using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private float scale = 30.0f;
    [SerializeField]
    private Rigidbody spawnedShips;
    [SerializeField]
    private float _radius = 2.0f;
    [SerializeField]
    private GameObject curr;

    private Transform _transform;

    private Vector3 _position;

    private Quaternion _rotation;

    private int _count;

    private GameObject _ship;

    private Player _player;

    private Transform _orbit;

    private void Awake()
    {
        _transform = transform;
        _player = GetComponent<Player>();
        _ship = GameObject.Find("Carrier");
        _orbit = GameObject.Find("orbit").transform;
    }

    private void Start()
    {
        _position = _transform.position;
        _rotation = _transform.rotation;
    }

    private void EditShip(GameObject go, bool isShip, int ring, int direction)
    {
        go.transform.localScale = new Vector3(scale, scale, scale);
        go.AddComponent<Follow>().Leader = _orbit;
        var f = go.GetComponent<Follow>();
        f.Ring = ring;
        f.Direction = direction;
        go.AddComponent<SphereCollider>().isTrigger = true;
        go.GetComponent<SphereCollider>().radius = 0.01f;

        if (!isShip) return;

        go.tag = "SpawnedShips";
        var children = new List<GameObject>();
        foreach (Transform child in go.transform)
        children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        Destroy(go.GetComponent<PlayerChild>());
        _player.Children.Add (go);
        Destroy(go.GetComponent<Spawn>());
    }

    private int UpdateCount(GameObject _ship)
    {
        return _player.Children.Count;
    }
    
    void SpawnShip(int multiplier)
    {
        int rings;
        for (int i = 0; i < multiplier - 1; i++)
        {
            if(_count >= 60){
                _count += 1;
                continue;
            }

            rings = _count / 12;
            int ring = -2 * rings;
            int index = GetIndex();
            int k = 1;
            for(int j = 0; j < index/12; j++){
                k *= -1;
            }
            int direction = k;
            
            GameObject temp = Instantiate(spawnedShips, _position, _rotation).gameObject;
            EditShip(temp, false, ring, direction);
            temp.transform.position = new Vector3(Mathf.Cos(12*i) * _radius, Mathf.Sin(12*i) * _radius, 0);
            GameObject go = Instantiate(spawnedShips, _position, _rotation).gameObject;
            EditShip(go, true, ring, direction);
            go.transform.position = _transform.position;

            temp.GetComponent<MeshRenderer>().enabled = false;
            go.GetComponent<Follow>().enabled = false;
            go.AddComponent<ShipAnimation>().Temp = temp;

            _count = UpdateCount(_ship);
        }
    }
    
    int GetIndex(){
       int length = _player.Children.Count + 1;
       int i = length/12;
        if(length%12 == 0){
            return 12*(i-1);
        }else{
            return 12*i;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            // string text = other.gameObject.GetComponentInChildren<TextMeshPro>().text;
            // int amt = _count;
            
            // if (text.Contains("+")) amt += Int32.Parse(text.Split(' ')[1]);
            // else
            // {
            //     if (amt == 0) amt = 1;
            //     amt *= Int32.Parse(text.Split(' ')[1]);
            // }

            // SpawnShip(amt + 1 - _count); //get text from gate for multiplier
            // //other.gameObject.GetComponent<BoxCollider>().enabled = false;
            SpawnShip(13);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.gameObject.tag == "Finish")
        {
            string text = other.gameObject.GetComponentInChildren<TextMeshPro>().text;
            int amt = _count;

            if (text.Contains("-")) amt -= Int32.Parse(text.Split(' ')[1]);
            else
            {
                if (amt == 0) amt = 1;
                amt /= Int32.Parse(text.Split(' ')[1]);
            }

            Debug.Log(amt);

            // if (amt == 0) call gameover

            while (_player.Children.Count != amt)
            {
                GameObject ship = _player.Children[_player.Children.Count - 1];
                _player.Children.RemoveAt(_player.Children.Count - 1);

                Destroy(ship);
            }

            _count = UpdateCount(_ship);
        }
        else if (other.gameObject.tag == "Currency")
        {
            SpawnShip(2);
            Destroy(other.gameObject);
            GameObject currency = Instantiate(curr, _transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
            Move (currency);
            _player.Currency += 1;
        }
        else if (other.gameObject.tag == "EditorOnly")
        {
            foreach(Transform child in _orbit){
                Destroy(child.gameObject);
            }
            _transform.position = new Vector3(0, 0, 2);
            _player.Currency = _player.Currency + (_player.Children.Count + 1) * 10;
        }
    }

    private void Move(GameObject go)
    {
        StartCoroutine(MoveCoroutine(go));
    }

    IEnumerator MoveCoroutine(GameObject go)
    {
        Color alpha = go.GetComponent<SpriteRenderer>().color;
        for (int i = 0; i <= 60; i++)
        {
            go.transform.position += new Vector3(0, 0.03f, 0);
            alpha.a -= 0.02f;
            go.GetComponent<SpriteRenderer>().color = alpha;
            go.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = alpha;
            yield return new WaitForFixedUpdate();
        }
        Destroy (go);
    }

    public int Count
    {
        get { return _count; }
        set { _count = value; }
    }
}
