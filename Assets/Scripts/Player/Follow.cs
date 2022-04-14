using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    [SerializeField]
    private Transform _leader;
    [SerializeField]
    private int rotationSpeed = 30;

    private Vector3 _followOffset;

    private Vector3 targetPosition;
    [SerializeField]
    private int _ring;

    private int _direction;

    private Vector3 position;

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
        }else{
            StartCoroutine(Rotate());
        }
        position = _leader.position;
    }

    IEnumerator Rotate(){
        //stationary rotation
        // var rotation = transform.rotation;
        // transform.RotateAround(position, Vector3.back, rotationSpeed*_direction*Time.deltaTime);
        // transform.rotation = rotation;

        //facing carrier
        transform.RotateAround(position, Vector3.back, rotationSpeed*_direction*Time.deltaTime);
        transform.LookAt(_leader, Vector3.up);
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 90, transform.rotation.z));
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
}
