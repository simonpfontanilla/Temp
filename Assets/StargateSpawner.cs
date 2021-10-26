using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StargateSpawner : MonoBehaviour
{
    [SerializeField] GameObject [] gates;

    // Start is called before the first frame update
    void Start()
    {
        // SpawnGate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnGate()
    {
        GameObject gate = gates[Random.Range(0, 2)];

        Instantiate(gate, transform.position, gate.transform.rotation);
    }
}
