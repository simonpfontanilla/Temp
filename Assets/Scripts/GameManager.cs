using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject [] badStarGates;
    [SerializeField] private GameObject [] goodStarGates;

    [SerializeField] private int maxShips = 0, enemyCount;


    // Start is called before the first frame update
    void Start()
    {
        GameObject [] gateSpawner = GameObject.FindGameObjectsWithTag("GateSpawner");

        foreach (GameObject item in gateSpawner)
        {
            item.GetComponent<StargateSpawner>().SpawnGate();
        }

        badStarGates = GameObject.FindGameObjectsWithTag("Finish");
        goodStarGates = GameObject.FindGameObjectsWithTag("Respawn");
        

        calculateMaxShips();

        // 20% 40% 60% 80% 100% enemy
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void calculateMaxShips()
    {
        foreach (GameObject item in goodStarGates)
        {
            string text = item.GetComponentInChildren<TextMeshPro>().text;

            if (text.Contains("+")) maxShips += Int32.Parse(text.Split(' ')[1]);
            else maxShips *= Int32.Parse(text.Split(' ')[1]);
        }
    }
}
