using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Rigidbody ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBall()
    {
        GameObject go = Instantiate(ball).gameObject;
        go.AddComponent<Destroy>();

        go.AddComponent<Follow>();
        go.GetComponent<Follow>().leader = GameObject.Find("PlayerShip_FBX").GetComponent<Transform>();
        
        Destroy(go.GetComponent<Spawn>());
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Respawn"){
            SpawnBall();
        }
    }
}
