using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    [SerializeField]
    public Transform leader;
    [SerializeField]
    private int rotationSpeed = 30;
    public Vector3 _followOffset;
    private Vector3 targetPosition;
    public int ring;
    public int direction;
    //get ships to offset before rotation

    private void Awake() {

    }

    void Start () 
    {
        if(gameObject.tag == "SpawnedShips") return;
        _followOffset = transform.position + new Vector3(0,0,ring);
        targetPosition = leader.position + _followOffset;
        transform.position += targetPosition;
    }

    void Update () 
    {
        if(gameObject.name == "orbit" || gameObject.name == "Name(Clone)"){
            StartCoroutine(Move());
        }else{
            StartCoroutine(Rotate());
        }
        // StartCoroutine(Move());
    }

    IEnumerator Rotate(){
        transform.RotateAround(leader.position, Vector3.back, rotationSpeed*direction*Time.deltaTime);
        yield return new WaitForFixedUpdate();
    }

    IEnumerator Move(){
        targetPosition = leader.position + _followOffset;
        transform.position += (targetPosition - transform.position);
        yield return new WaitForFixedUpdate();
    }
}
