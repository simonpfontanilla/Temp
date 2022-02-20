using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    // [SerializeField]
    // public bool canSpawn;
    public int canSpawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if ring is full, shift it back
    }


    private void OnTriggerEnter(Collider other){
        canSpawn += 1;
    }

    private void OnTriggerExit(Collider other) {
        canSpawn -= 1;
    }
}
