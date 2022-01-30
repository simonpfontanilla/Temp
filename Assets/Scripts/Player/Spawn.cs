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
    private float radius = 2.0f;
    [SerializeField]
    private GameObject curr;

    private Transform _transform;

    private Vector3 _position;

    private Quaternion _rotation;

    private int _count;

    private GameObject _ship;

    private Player _player;

    private void Awake()
    {
        _transform = transform;
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _ship = GameObject.Find("Carrier");
        _position = _transform.position;
        _rotation = _transform.rotation;
    }

    private void EditShip(GameObject go, bool t)
    {
        go.transform.localScale = new Vector3(scale, scale, scale);
        //go.AddComponent<Follow>().leader = _transform;
        go.AddComponent<Follow>().leader = GameObject.Find("orbit").transform;

        if (!t) return;

        go.tag = "SpawnedShips";
        var children = new List<GameObject>();
        foreach (Transform child in go.transform)
        children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        Destroy(go.GetComponent<PlayerChild>());
        _player.children.Add (go);
        Destroy(go.GetComponent<Spawn>());
    }

    private int UpdateCount(GameObject _ship)
    {
        return _player.children.Count;
    }

    private void SpawnShip(int multiplier)
    {
        _count = UpdateCount(_ship);
        int rings;
        for (int i = 0; i < multiplier - 1; i++)
        {
            _count = UpdateCount(_ship) + 1;
            rings = _count / 12;

            //create ships
            GameObject go = Instantiate(spawnedShips, _position, _rotation).gameObject;
            EditShip(go, true);
            GameObject temp = Instantiate(spawnedShips, _position, _rotation).gameObject;
            EditShip(temp, false);

            //generate position of ship
            int ring = -2 * rings;
            temp.transform.localPosition = new Vector3(Mathf.Cos(12 * (i + _count)) * radius, Mathf.Sin(12 * (i + _count)) * radius, 0);
            go.transform.localPosition = _transform.position;
            //go.transform.localPosition = new Vector3(0,0,0);

            var m = temp.GetComponent<MeshRenderer>().enabled = false;
            var f = go.GetComponent<Follow>().enabled = false;
            go.AddComponent<ShipAnimation>().temp = temp;
            go.GetComponent<ShipAnimation>().ring = ring;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            string text = other.gameObject.GetComponentInChildren<TextMeshPro>().text;
            int amt = _count;
            
            if (text.Contains("+")) amt += Int32.Parse(text.Split(' ')[1]);
            else
            {
                if (amt == 0) amt = 1;
                amt *= Int32.Parse(text.Split(' ')[1]);
            }

            SpawnShip(amt + 1 - _count); //get text from gate for multiplier
            //other.gameObject.GetComponent<BoxCollider>().enabled = false;
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

            while (_player.children.Count != amt)
            {
                GameObject ship = _player.children[_player.children.Count - 1];
                _player.children.RemoveAt(_player.children.Count - 1);

                Destroy(ship);
            }
        }
        else if (other.gameObject.tag == "Currency")
        {
            SpawnShip(2);
            Destroy(other.gameObject);
            GameObject currency = Instantiate(curr, _transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
            Move (currency);
            _player.currency += 1;
        }
        else if (other.gameObject.tag == "EditorOnly")
        {
            _transform.position = new Vector3(0, 0, 2);
            _player.currency = _player.currency + (_player.children.Count + 1) * 10;
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
}
