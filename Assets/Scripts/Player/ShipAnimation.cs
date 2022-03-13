using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimation : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.1f;

    private Transform _target;

    private GameObject _temp;

    private Transform _transform;

    private GameObject _ship;

    private Transform _obj;

    private Transform _orbit;

    private void Awake() 
    {
        _transform = transform;
        _obj = GameObject.Find("orbit").transform;
        _orbit = GameObject.Find("orbit").transform;
    }

    void Start()
    {
        _ship = GameObject.Find("Carrier");
        _target = _temp.transform;
    }

    void FixedUpdate()
    {
        if (transform.position != _target.position)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, _speed);
        }
    }

    void Update()
    {
        bool distance = Vector3.Distance(transform.position, _ship.transform.position) >= 1.9f;
        if (!distance) return;
        SetPosition();
    }

    void SetPosition(){
        GetComponent<Follow>().enabled = true;
        this.transform.parent = _orbit;
        Destroy(this);
        Destroy (_temp);
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public Transform Target
    {
        get { return _target; }
        set { _target = value; }
    }

    public GameObject Temp
    {
        get { return _temp; }
        set { _temp = value; }
    }
}
