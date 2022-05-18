using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    [SerializeField]
    private Transform _leader;
    [SerializeField]
    private int rotationSpeed = 50;

    private Vector3 _followOffset;

    private Vector3 targetPosition;
    [SerializeField]
    private int _ring;

    private int _direction;

    private Vector3 position;
    [SerializeField]
    private Vector3 _axis;
    [SerializeField]
    public bool sphere;

    void Start () 
    {
        if(gameObject.tag == "SpawnedShips") return;
        _followOffset = transform.position + new Vector3(0,0,_ring);
        targetPosition = _leader.position + _followOffset;
        transform.position += targetPosition;
    }

    void Update () 
    {
        if(gameObject.name == "orbit" || gameObject.name == "Name(Clone)"){
            StartCoroutine(Move());
        }else{;
            StartCoroutine(Rotate(_axis));
        }
        position = _leader.position;
    }

    IEnumerator Rotate(Vector3 axis){
        if(sphere){ //sphere
            var rotation = transform.rotation;
            transform.RotateAround(position, axis, rotationSpeed*_direction*Time.deltaTime);
            transform.rotation = rotation;
        }else{ //ring
            var rotation = transform.rotation;
            transform.RotateAround(position, Vector3.back, rotationSpeed*_direction*Time.deltaTime);
            transform.rotation = rotation;
        }
        yield return new WaitForFixedUpdate();
    }

    IEnumerator Move(){
        targetPosition = _leader.position + _followOffset;
        transform.position += (targetPosition - transform.position);
        yield return new WaitForFixedUpdate();
    }

    public Transform Leader
    {
        get { return _leader; }
        set { _leader = value; }
    }

    public Vector3 FollowOffset
    {
        get { return _followOffset; }
        set { _followOffset = value; }
    }

    public int Ring
    {
        get { return _ring; }
        set { _ring = value; }
    }

    public int Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public Vector3 Axis
    {
        get { return _axis; }
        set { _axis = value; } // I couldn't if I fried
    }
}
