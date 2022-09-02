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

    [SerializeField]
    private int _count;
    [SerializeField]
    private GameObject _ship;

    private Player _player;
    [SerializeField]
    private Transform _orbit;

    GameManager gM;
	
	[SerializeField]
    private GameObject canvas;

    private void Awake()
    {
        _transform = transform;
        _player = GetComponent<Player>();
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        Vibration.Init();
    }

    private void Start()
    {
        _position = _transform.position; // Balls
        _rotation = _transform.rotation;
    }

    public void Init()
    {
        int ships = PlayerPrefs.GetInt("shipLevel", 0);
        SpawnShip(ships + 1);
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
        go.transform.Rotate(new Vector3(-90, 90, 0), Space.Self);

        if (!isShip) return;

        go.tag = "SpawnedShips";
        var children = new List<GameObject>();
        foreach (Transform child in go.transform)
        children.Add(child.gameObject); // The child
        children.ForEach(child => Destroy(child)); // Anakin not the children
        Destroy(go.GetComponent<PlayerChild>());
        _player.Children.Add (go);
    }

    private int UpdateCount(GameObject _ship)
    {
        return _player.Children.Count;
    }
    
    void SpawnShip(int multiplier) // Hello there
    {
        // Vibration.Vibrate();
        // Vibration.VibratePop();
        // Vibration.VibratePeek();
        // Vibration.VibrateNope();
        // Vibration.VibrateCancel();
        // Vibration.Vibrate(500);
        // long [] pattern = { 0, 1000, 1000, 1000, 1000 };
        // Vibration.Vibrate ( pattern, -1 );  
        
        int rings;
        for (int i = 0; i < multiplier - 1; i++)
        {
            if(_count >= 100){
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
            temp.transform.position = new Vector3(Mathf.Cos(12*i) * _radius, Mathf.Sin(12*i) * _radius, 0); // 2 + 2 is 4 - 3 is 1 quick maths.
            GameObject go = Instantiate(spawnedShips, _position, _rotation).gameObject;
            EditShip(go, true, ring, direction);
            go.transform.position = _transform.position;

            temp.GetComponent<MeshRenderer>().enabled = false;
            go.GetComponent<Follow>().enabled = false;
            go.AddComponent<ShipAnimation>().Temp = temp;

            _count = UpdateCount(_ship); // General Kenobi
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
            Vibration.VibratePop();
            Debug.Log("Vibrate Good StarGate");

            string text = other.gameObject.GetComponentInChildren<TextMeshPro>().text;
            int amt = _count;
            
            if (text.Contains("+")) amt += Int32.Parse(text.Split(' ')[1]);
            else
            {
                if (amt == 0) amt = 1;
                amt *= Int32.Parse(text.Split(' ')[1]);
            }

            // actual_count = amt + 1 - _count;
            SpawnShip(amt + 1 - _count);
            // //other.gameObject.GetComponent<BoxCollider>().enabled = false;
            // SpawnShip(13);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            // SpawnShip(13);
            // other.gameObject.GetComponent<BoxCollider>().enabled = false;
            turnOff(other.gameObject);
        }
        else if (other.gameObject.tag == "Finish")
        {
            Vibration.Vibrate();
            Debug.Log("Vibrate Bad StarGate");
			
            canvas.SetActive(false);
			
            string text = other.gameObject.GetComponentInChildren<TextMeshPro>().text;
            int amt = _count;

            if (text.Contains("-")) amt -= Int32.Parse(text.Split(' ')[1]);
            else
            {
                if (amt == 0) amt = 1;
                amt /= Int32.Parse(text.Split(' ')[1]);
            }

            if (amt <= 0)
                gM.GameOver(true);

            if (amt > _player.Children.Count)
            {
                _count = amt;
                return;
            }

            while (_player.Children.Count != amt)
            {
                GameObject ship = _player.Children[_player.Children.Count - 1];
                _player.Children.RemoveAt(_player.Children.Count - 1);

                Destroy(ship);
            }

            turnOff(other.gameObject);

            _count = UpdateCount(_ship);
        }
        else if (other.gameObject.tag == "Asteroids")
        {
            Vibration.VibratePop();
            Debug.Log("Vibrate Asteroids");

            // Call gameover

            while (_player.Children.Count != 0)
            {
                GameObject ship = _player.Children[_player.Children.Count - 1];
                _player.Children.RemoveAt(_player.Children.Count - 1);

                Destroy(ship);
            }

            _count = UpdateCount(_ship);
            
            gM.GameOver(true);
        }
        else if (other.gameObject.tag == "Currency")
        {
            // Vibration.VibratePop();
            // Debug.Log("Vibrate Neutral Ship");

            SpawnShip(2);
            Destroy(other.gameObject);
            GameObject currency = Instantiate(curr, _transform.position + new Vector3(0, 1, 0), Quaternion.identity).gameObject;
            Move (currency);
            _player.Currency += 1;
        }
        else if (other.gameObject.tag == "CenterCarrier")
        {   
            gameObject.GetComponent<Movement>().moveCarrierToCenter();
        }
        else if (other.gameObject.tag == "EditorOnly")
        {
            foreach(Transform child in _orbit){
                Destroy(child.gameObject);
            }
            _transform.position = new Vector3(0, 0, 2);
            _player.Currency = _player.Currency + (_player.Children.Count + 1) * 10;
            _player.Children.Clear();
            _count = UpdateCount(_ship);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemyShip")
        {
            Vibration.VibratePop();
            Debug.Log("Vibrate Enemy");

            int amt = _count;
            amt -= 5;
            
            if (amt > _player.Children.Count)
            {
                _count = amt;
                return;
            }

            if (amt <= 0)
                gM.GameOver(true);
            
            while (_player.Children.Count != amt)
            {
                GameObject ship = _player.Children[_player.Children.Count - 1];
                _player.Children.RemoveAt(_player.Children.Count - 1);

                Destroy(ship);
            }

            _count = UpdateCount(_ship);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "CenterCarrier")
        {
            gameObject.GetComponent<Movement>().detachCamera();
        }
        else if (other.gameObject.tag == "EndGate")
        {
            // increase level
            gM.playerPrefsHolder.increaseLevel();
            Debug.Log(gM.playerPrefsHolder.getLevel());
            // Open game win ui
            // reset level
        }
    }

    private void Move(GameObject go) // I like the way you move
    {
        StartCoroutine(MoveCoroutine(go));
    }

    private void turnOff(GameObject other)
    {
        other.transform.GetChild(0).gameObject.SetActive(false);
        other.transform.GetChild(1).gameObject.SetActive(false);
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
        Destroy (go); // Don't go bacon my heart
    }

    public int Count
    {
        get { return _count; }
        set { _count = value; } // I couldn't if I fried
    }
}
