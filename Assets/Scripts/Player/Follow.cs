using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    [SerializeField]
    public Transform leader;
    [SerializeField]
    private int rotationSpeed = 15;
    public Vector3 _followOffset;
    private Vector3 targetPosition;
    private float desiredDistance;
    //get ships to offset before rotation

    private void Awake() {

    }

    void Start () 
    {
        if(gameObject.tag == "SpawnedShips") return;
        _followOffset = transform.position;
        targetPosition = leader.position + _followOffset;
        transform.position += (targetPosition - transform.position);
        Debug.Log(transform.position);
        desiredDistance = Vector3.Distance(leader.position, transform.position);
    }

    void Update () 
    {
        // if(gameObject.name == "orbit"){
        //     StartCoroutine(Move());
        // }else{
        //     StartCoroutine(Rotate());
        // }
        StartCoroutine(Move());
    }

    IEnumerator Rotate(){
        transform.RotateAround(leader.position, Vector3.back, rotationSpeed*Time.deltaTime);
        yield return new WaitForFixedUpdate();
    }

    IEnumerator Move(){
        targetPosition = leader.position + _followOffset;
        transform.position += (targetPosition - transform.position);
        yield return new WaitForFixedUpdate();
    }
}
