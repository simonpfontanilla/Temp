using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // [SerializeField] private GameObject [] badStarGates;
    [SerializeField] private GameObject [] goodStarGates;
    [SerializeField] private double [] difficultyLevel = {0.2, 0.4, 0.6, 0.8, 1.0};

    [SerializeField] private int maxShips = 1, enemyCount, currentLevel;
    
    [SerializeField] private GameObject gateSpawners;

    // Start is called before the first frame update
    void Start()
    {
        // One or two asteroids between spaces any where between that

        int rand = UnityEngine.Random.Range(4, 11);
        float zPos = 31.0f;
        for (int i = 0; i < rand; i++)
        {
            Instantiate(gateSpawners, new Vector3(0f, 4.17f, zPos), gateSpawners.transform.rotation);
            zPos += 22;
        }

        List<GameObject> gateSpawner = GameObject.FindGameObjectsWithTag("GateSpawner").ToList();

        foreach (GameObject item in gateSpawner) item.GetComponent<StargateSpawner>().SpawnGate(-1);

        List<GameObject> g1 = gateSpawner.FindAll(i => i.GetComponent<StargateSpawner>().switchGate == false);
        List<GameObject> g2 = gateSpawner.FindAll(i => i.GetComponent<StargateSpawner>().switchGate == true);

        foreach (GameObject g in g2)
        {
            // Find pair
            GameObject which = g1.Find(i => i.transform.position.z == g.transform.position.z);
            
            if (which.GetComponent<StargateSpawner>().whichGate == g.GetComponent<StargateSpawner>().whichGate)
            {
                DestroyImmediate(g.transform.GetChild(0).gameObject);

                g.GetComponent<StargateSpawner>().SpawnGate(which.GetComponent<StargateSpawner>().whichGate == 0 ? 1 : 0);
            }
        }

        List<GameObject> groupGateSpawner = GameObject.FindGameObjectsWithTag("GroupGateSpawner").ToList();

        foreach(GameObject gr in groupGateSpawner)
        {
            int x = UnityEngine.Random.Range(-1, 3);

            if (x == -1) continue;

            DestroyImmediate(gr.transform.GetChild(x).gameObject);
        }

        calculateMaxShips();

        // 20% 40% 60% 80% 100% enemy
        enemyCount = (int)(maxShips * difficultyLevel[currentLevel]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void calculateMaxShips()
    {
        goodStarGates = GameObject.FindGameObjectsWithTag("Respawn");

        foreach (GameObject item in goodStarGates)
        {
            string text = item.GetComponentInChildren<TextMeshPro>().text;

            if (text.Contains("+")) maxShips += Int32.Parse(text.Split(' ')[1]);
            else maxShips *= Int32.Parse(text.Split(' ')[1]);
        }
    }
}
