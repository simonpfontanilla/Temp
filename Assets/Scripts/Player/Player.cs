using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public List<GameObject> children;
    public int currency;
    // Start is called before the first frame update
    public void Start()
    {

    }

    public void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other){
        // if (other.gameObject.tag == "Player"){
        //     Rigidbody rb = GetComponent<Rigidbody>();
        //     rb.velocity = new Vector3(0,0,0);
        // }
    }
}
