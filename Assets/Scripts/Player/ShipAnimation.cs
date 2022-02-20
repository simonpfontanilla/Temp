using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimation : MonoBehaviour
{
    public float t = 0.1f;

    public Transform _target;

    public GameObject temp;

    private Transform _transform;

    private GameObject _ship;

    private Transform _obj;

    public int ring;

    private void Awake() 
    {
        _transform = transform;
        _obj = GameObject.Find("orbit").transform;
    }

    void Start()
    {
        _ship = GameObject.Find("Carrier");
        _target = temp.transform;
    }

    void FixedUpdate()
    {
        if (transform.position != _target.position)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, t);
        }
    }

    void Update()
    {
        bool distance = Vector3.Distance(transform.position, _ship.transform.position) >= 1.9f;
        if (!distance) return;
        SetPosition();
    }

    void SetPosition(){
        GetComponent<Follow>()._followOffset += new Vector3(0,0,ring);
        GetComponent<Follow>().enabled = true;
        this.transform.parent = GameObject.Find("orbit").transform;
        Destroy(this);
        Destroy (temp);
    }
}
