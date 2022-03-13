using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerChild : Player
{

    new void Start()
    {
        base.Start(); //calls the start method in the parent class
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        GetComponent<BoxCollider>().isTrigger = true;
    }


    new void Update()
    {

    }

}
