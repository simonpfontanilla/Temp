using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public List<GameObject> playerChildren;
    public Transform leader;
    private Vector3 _followOffset;
    public int radius = 2;

    void Start () 
    {
        playerChildren = GameObject.Find("PlayerShip_FBX").GetComponent<Player>().children;
        int rings = playerChildren.Count;
        Debug.Log(rings);
        if(rings < 12){
            _followOffset = new Vector3(Mathf.Cos(12*rings)*radius, Mathf.Sin(12*rings)*radius, 0);
        }else if(rings >= 12 && rings < 24){
            _followOffset = new Vector3(Mathf.Cos(12*rings)*radius, Mathf.Sin(12*rings)*radius, 3);
        }else if(rings >= 24 && rings < 36){
            _followOffset = new Vector3(Mathf.Cos(12*rings)*radius, Mathf.Sin(12*rings)*radius, 6);
        }else if(rings >= 36 && rings < 48){
            _followOffset = new Vector3(Mathf.Cos(12*rings)*radius, Mathf.Sin(12*rings)*radius, 9);
        }else if(rings >= 48 && rings < 60){
            _followOffset = new Vector3(Mathf.Cos(12*rings)*radius, Mathf.Sin(12*rings)*radius, 12);
        }

        //_followOffset = Random.insideUnitCircle.normalized*2;
    }

    void LateUpdate () 
    {
        Vector3 targetPosition = leader.position + _followOffset;
        transform.position += (targetPosition - transform.position);
    }
}
