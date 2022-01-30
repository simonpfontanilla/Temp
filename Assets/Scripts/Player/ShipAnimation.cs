using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimation : MonoBehaviour
{
    public float t = 0.3f;

    public Transform _target;

    public GameObject temp;

    public int ring;

    private Transform _transform;

    private GameObject _ship;

    private void Awake() 
    {
        _transform = transform;
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
        GetComponent<Follow>()._followOffset = temp.GetComponent<Follow>()._followOffset + new Vector3(0, 0, ring);
        GetComponent<Follow>().enabled = true;
        this.transform.parent = GameObject.Find("orbit").transform;
        Destroy(this);
        Destroy (temp);
    }
}
