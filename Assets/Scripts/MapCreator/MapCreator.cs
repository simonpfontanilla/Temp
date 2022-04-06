using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Json;

public class MapCreator : MonoBehaviour
{
    // [SerializeField] private GameObject [] badStarGates;
    [SerializeField] private GameObject [] goodStarGates;
    [SerializeField] private double [] difficultyLevel = {0.2, 0.4, 0.6, 0.8, 1.0};

    [SerializeField] private int maxShips = 1, enemyCount;
    private int numGatesSpawners = 0;
    
    [SerializeField] private GameObject gateSpawners;
    [SerializeField] private GameObject [] asteroids;
    [SerializeField] private GameObject collectableShip;
    [SerializeField] private GameObject bigStarGate;
    [SerializeField] private GameObject centerCarrier;
    [SerializeField] private GameObject enemy;
    private List<MapLevelClass> levels = new List<MapLevelClass>();

    public bool testing = false;

    // Start is called before the first frame update
    void Start()
    {
        // createLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (testing)
        {
            for (int i = 0; i < 100; i++)
            {
                createLevel();
                makeLevelClass(i + 1);
                destoryMap();
            }

            FileHandler.SaveToJSON<MapLevelClass> (levels, "testData.json");
            testing = false;
        }
    }

    void createLevel()
    {
        numGatesSpawners = UnityEngine.Random.Range(4, 11);
        float zPos = 31.0f;
        for (int i = 0; i < numGatesSpawners; i++)
        {
            Instantiate(gateSpawners, new Vector3(0f, 4f, zPos), gateSpawners.transform.rotation);
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

        bool skip = true;

        foreach(GameObject gr in groupGateSpawner)
        {
            if (skip)
            {
                skip = false;
                continue;
            }

            int x = UnityEngine.Random.Range(-1, 3);

            if (x == -1) continue;

            DestroyImmediate(gr.transform.GetChild(x).gameObject);
        }

        // one in the beginning, middle, and end (rare) gets a multiplication
        changeToMult(groupGateSpawner, 0);
        changeToMult(groupGateSpawner, (int)(groupGateSpawner.Count / 2));
        changeToMult(groupGateSpawner, groupGateSpawner.Count - 1);

        // first gate change division to sub
        changeToSub(groupGateSpawner, 0);

        calculateMaxShips();

        // 20% 40% 60% 80% 100% enemy
        enemyCount = (int)(maxShips * difficultyLevel[UnityEngine.Random.Range(0, 5)]);

        // One or two asteroids between spaces any where between that
        // or collectable
        // Asyeroid = 0, collectable = 1
        zPos = 42.0f;
        for (int i = 0; i < numGatesSpawners - 1; i++)
        {
            int astOrCol = UnityEngine.Random.Range(0, 2);
            int subAddZ = UnityEngine.Random.Range(-7, 8);
            Vector3 vec = new Vector3(UnityEngine.Random.Range(-7, 7), 4f, zPos + subAddZ);

            if (astOrCol == 0)
            {
                int randAst = UnityEngine.Random.Range(0, 3);
                for (int j = 0; j < randAst; j++)
                {
                    subAddZ = UnityEngine.Random.Range(-7, 8);
                    vec = new Vector3(UnityEngine.Random.Range(-7, 7), 4f, zPos + subAddZ);
                    int whichAst = UnityEngine.Random.Range(0, 2);
                    Instantiate(asteroids[whichAst], vec, asteroids[whichAst].transform.rotation);
                }
            }
            else if (astOrCol == 1)
            {
                Instantiate(collectableShip, vec, collectableShip.transform.rotation);
            }

            zPos += 22;
        }

        Instantiate(centerCarrier, new Vector3(0, 4, zPos), centerCarrier.transform.rotation);

        zPos += 14;
        int reminder = enemyCount % 3;
        List<GameObject> enemies = new List<GameObject>();
        for (int i = 0; i < enemyCount; i+=3, zPos+=14)
        {
            GameObject g;
            g = Instantiate(enemy, new Vector3(-2, 4, zPos), enemy.transform.rotation);
            enemies.Add(g);
            g = Instantiate(enemy, new Vector3(0, 4, zPos), enemy.transform.rotation);
            enemies.Add(g);
            g = Instantiate(enemy, new Vector3(2, 4, zPos), enemy.transform.rotation);
            enemies.Add(g);
        }

        // remove extra enemies
        for (int i = enemies.Count() - 1; i >= enemyCount; i--)
        {
            DestroyImmediate(enemies[i].gameObject);
        }
        
        // Add big stargate
        zPos+=14;
        Instantiate(bigStarGate, new Vector3(0, 4, zPos), bigStarGate.transform.rotation);
        
        // for simulation change the x so ten maps can be add to the scene for testing.
    }

    void makeLevelClass(int level)
    {
        MapLevelClass mapLevel = new MapLevelClass();
        mapLevel.level = level;
        mapLevel.maxShips = maxShips;
        mapLevel.numGatesSpawners = numGatesSpawners;
        // mapLevel.centerCarrier = GameObject.Find("CenterCarrier(Clone)").GetComponent<Transform>().position.ToString();
        // mapLevel.bigStartGate = GameObject.Find("bigStarGate(Clone)").GetComponent<Transform>().position.ToString();

        // gates
        mapLevel.gatePositons = new List<string>();
        mapLevel.whichGates = new List<int>();
        mapLevel.gateTexts = new List<string>();
        List<GameObject> groupGateSpawner = GameObject.FindGameObjectsWithTag("GroupGateSpawner").ToList();
        for (int i = 0; i < groupGateSpawner.Count(); i++)
        {
            for (int j = 0; j < groupGateSpawner[i].transform.childCount; j++)
            {
                Vector3 vec = new Vector3(
                    groupGateSpawner[i].transform.GetChild(j).transform.position.x,
                    groupGateSpawner[i].transform.GetChild(j).transform.position.y,
                    groupGateSpawner[i].transform.position.z
                );
                
                mapLevel.gatePositons.Add(vec.ToString());

                mapLevel.whichGates.Add(
                    groupGateSpawner[i].transform.GetChild(j).GetComponent<StargateSpawner>().whichGate
                );

                mapLevel.gateTexts.Add(
                    groupGateSpawner[i].transform.GetChild(j).transform.GetChild(0)
                        .gameObject.GetComponentInChildren<TextMeshPro>().text
                );
            }
        }

        // asteroids
        List<GameObject> asteroid = GameObject.FindGameObjectsWithTag("Asteroids").ToList();
        mapLevel.asteroidPositons = new List<string>();
        foreach (GameObject g in asteroid)
            mapLevel.asteroidPositons.Add(g.transform.position.ToString());

        // currency (collectable)
        List<GameObject> currency = GameObject.FindGameObjectsWithTag("Currency").ToList();
        mapLevel.collectablePositons = new List<string>();
        foreach (GameObject g in currency)
            mapLevel.collectablePositons.Add(g.transform.position.ToString());

        levels.Add(mapLevel);
        
        // FileHandler.SaveToJSON<MapLevelClass> (levels, "testData.json");

        // Debug.Log(json);

        // testing = false;
    }

    void destoryMap()
    {
        List<GameObject> groupGateSpawner = GameObject.FindGameObjectsWithTag("GroupGateSpawner").ToList();
        foreach (GameObject g in groupGateSpawner)
        {
            DestroyImmediate(g.gameObject);
        }

        List<GameObject> enemyShips = GameObject.FindGameObjectsWithTag("EnemyShip").ToList();
        foreach (GameObject g in enemyShips)
        {
            DestroyImmediate(g.gameObject);
        }

        List<GameObject> asteroid = GameObject.FindGameObjectsWithTag("Asteroids").ToList();
        foreach (GameObject g in asteroid)
        {
            DestroyImmediate(g.gameObject);
        }

        List<GameObject> currency = GameObject.FindGameObjectsWithTag("Currency").ToList();
        foreach (GameObject g in currency)
        {
            DestroyImmediate(g.gameObject);
        }

        DestroyImmediate(GameObject.Find("CenterCarrier(Clone)"));
        DestroyImmediate(GameObject.Find("bigStarGate(Clone)"));

        maxShips = 1;

        testing = false;
    }

    private void changeToMult(List<GameObject> groupGateSpawner, int group)
    {
        for (int i = 0; i < groupGateSpawner[group].transform.childCount; i++)
        {
            if (groupGateSpawner[group].transform.GetChild(i).transform.GetChild(0).gameObject.CompareTag("Respawn"))
            {
                groupGateSpawner[group].transform.GetChild(i).transform.GetChild(0)
                    .gameObject.GetComponentInChildren<TextMeshPro>().SetText("x " + UnityEngine.Random.Range(1, 6));
                break;
            }
        }
    }

    private void changeToSub(List<GameObject> groupGateSpawner, int group)
    {
        for (int i = 0; i < groupGateSpawner[group].transform.childCount; i++)
        {
            if (groupGateSpawner[group].transform.GetChild(i).transform.GetChild(0).gameObject.CompareTag("Finish") &&
                groupGateSpawner[group].transform.GetChild(i).transform.GetChild(0)
                    .gameObject.GetComponentInChildren<TextMeshPro>().text.Contains("÷")
            )
            {
                string text = groupGateSpawner[group].transform.GetChild(i).transform.GetChild(0)
                    .gameObject.GetComponentInChildren<TextMeshPro>().text;
                
                groupGateSpawner[group].transform.GetChild(i).transform.GetChild(0)
                    .gameObject.GetComponentInChildren<TextMeshPro>().SetText("- " + text.Split(' ')[1]);
                break;
            }
        }
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

//  string to vector3
//  Remove the parentheses
//  if (sVector.StartsWith ("(") && sVector.EndsWith (")")) {
//      sVector = sVector.Substring(1, sVector.Length-2);
//  }

//  split the items
//  string[] sArray = sVector.Split(',');

//  store as a Vector3
//  Vector3 result = new Vector3(
//      float.Parse(sArray[0]),
//      float.Parse(sArray[1]),
//      float.Parse(sArray[2]));
