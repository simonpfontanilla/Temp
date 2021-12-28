using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StargateSpawner : MonoBehaviour
{
    [SerializeField] GameObject [] gates;
    [SerializeField] public bool switchGate = false;
    public int whichGate = 0;


    // Start is called before the first frame update
    void Start()
    {
        // SpawnGate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnGate(int g)
    {
        GameObject gate = gates[whichGate = Random.Range(0, 2)];

        if (g != -1) gate = gates[whichGate = g];

        GameObject obj = Instantiate(gate, transform.position, gate.transform.rotation);
        obj.transform.parent = transform;

        return obj;
    }
}
