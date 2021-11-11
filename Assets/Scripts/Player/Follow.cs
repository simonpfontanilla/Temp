using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public List<GameObject> playerChildren;
    public Transform leader;
    private Vector3 _followOffset;
    public float radius = 2.0f;

    void Start () 
    {
        playerChildren = GameObject.Find("PlayerShip_FBX").GetComponent<Player>().children;
        int position = playerChildren.Count;
        int rings = position/12;
        //Debug.Log(position/12);
        _followOffset = new Vector3(Mathf.Cos(12*position)*radius, Mathf.Sin(12*position)*radius, 3*rings);
    }

    void LateUpdate () 
    {
        Vector3 targetPosition = leader.position + _followOffset;
        transform.position += (targetPosition - transform.position);
    }
}
