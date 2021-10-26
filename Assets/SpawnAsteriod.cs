using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteriod : MonoBehaviour
{
    public GameObject [] asteriods;
    [SerializeField] private int minX, maxX, minZ, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, 2);

        Vector3 vec = new Vector3(Random.Range(minX, maxX), 4.17f, Random.Range(minZ, maxZ));

        Instantiate(asteriods[rand], vec, asteriods[rand].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
