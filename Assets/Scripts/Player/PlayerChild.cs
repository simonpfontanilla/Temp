using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerChild : Player
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start(); //calls the start method in the parent class
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        GetComponent<BoxCollider>().isTrigger = true;
        
        List<GameObject> lst = GameObject.Find("PlayerShip_FBX").GetComponent<Player>().children;
        lst.Clear();
        foreach(GameObject fooObj in GameObject.FindGameObjectsWithTag("SpawnedShips"))
        {
            lst.Add(fooObj);
        }
    }

    // Update is called once per frame
    new void Update()
    {

    }

}
