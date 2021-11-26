using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform leader;
    private Vector3 _followOffset;

    void Start () 
    {
        _followOffset = gameObject.GetComponent<Spawn>().transform.position;
    }

    void LateUpdate () 
    {
        Vector3 targetPosition = leader.position + _followOffset;
        transform.position += (targetPosition - transform.position);
    }
}
