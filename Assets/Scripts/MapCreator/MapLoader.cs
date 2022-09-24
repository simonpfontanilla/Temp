using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private double [] difficultyLevel = {0.2, 0.4, 0.6, 0.8, 1.0};
    
    [SerializeField] private GameObject gateSpawner;
    [SerializeField] private GameObject [] asteroids;
    [SerializeField] private GameObject collectableShip;
    [SerializeField] private GameObject bigStarGate;
    [SerializeField] private GameObject centerCarrier;
    [SerializeField] private GameObject enemy;
    public float centerToPosition, toCenterZPos, bigGateEndingZPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void create()
    {
        GameManager gM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        createMap(LoadResourceTextfile(), gM.playerPrefsHolder.getLevel() - 1);
    }

    private void createMap(List<MapLevelClass> mapLevel, int level)
    {
        for (int i = 0; i < mapLevel[level].gatePositons.Count(); i++)
        {
            GameObject gateSpawn = Instantiate(gateSpawner, strToVec(mapLevel[level].gatePositons[i]), gateSpawner.transform.rotation);
            GameObject gate = gateSpawn.GetComponent<StargateSpawner>().SpawnGate(mapLevel[level].whichGates[i]);
            gate.transform.GetChild(0)
                .gameObject.GetComponentInChildren<TextMeshPro>().SetText(mapLevel[level].gateTexts[i]);
        }

        // One or two asteroids between spaces any where between that
        // or collectable
        // Asyeroid = 0, collectable = 1
        float zPos = 15f + 42.0f;
        for (int i = 0; i < mapLevel[level].numGatesSpawners - 1; i++)
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

        // should asteroid and collectables positions be random

        zPos = strToVec(mapLevel[level].gatePositons[mapLevel[level].gatePositons.Count() - 1]).z;

        // center carrier
        zPos += 7;
        toCenterZPos = zPos;
        centerToPosition = zPos + 7;
        GameObject centerCarrierObject = Instantiate(centerCarrier, new Vector3(0, 4, zPos), centerCarrier.transform.rotation);

        // enemy
        int enemyCount = (int)(mapLevel[level].maxShips * difficultyLevel[UnityEngine.Random.Range(0, 5)]);
        // Debug.Log(mapLevel[level].maxShips);

        zPos += 22;
        List<GameObject> enemies = new List<GameObject>();
        // make object with z position
        for (int i = 0; i < enemyCount; i+=5, zPos+=7)
        {
            GameObject g;
            // g = Instantiate(enemy, new Vector3(-2, 4, zPos), enemy.transform.rotation);
            // enemies.Add(g);
            // g = Instantiate(enemy, new Vector3(-1, 4, zPos), enemy.transform.rotation);
            // enemies.Add(g);
            g = Instantiate(enemy, new Vector3(0, 4, zPos), enemy.transform.rotation);
            // enemies.Add(g);
            // g = Instantiate(enemy, new Vector3(1, 4, zPos), enemy.transform.rotation);
            // enemies.Add(g);
            // g = Instantiate(enemy, new Vector3(2, 4, zPos), enemy.transform.rotation);
            // enemies.Add(g);
        }

        // remove extra enemies
        // for (int i = enemies.Count() - 1; i >= enemyCount; i--)
        //     DestroyImmediate(enemies[i].gameObject);
        
        // int reminder = enemyCount % 5;
        // if (reminder > 0)
        // {
        //     for (int i = enemyCount - 1; i >= (enemyCount - reminder); i--)
        //     {
        //         float add = 0f;
        //         if (reminder == 1) add = 2f;
        //         else if (reminder == 2) add = 1.5f;
        //         else if (reminder == 3) add = 1f;
        //         else add = 0.5f;

        //         float x = enemies[i].gameObject.transform.position.x + add;

        //         enemies[i].gameObject.transform.position = new Vector3(x, 4, zPos-7);
        //     }
        // }

        centerCarrierObject.GetComponent<BoxCollider>().size = new Vector3(14f, 14f, (zPos + 5f) - (centerToPosition - 7f));
        centerCarrierObject.GetComponent<BoxCollider>().center = new Vector3(0f, 0f, ((zPos + 5f) - (centerToPosition - 7f)) / 2.019348984f);
        
        // Add big stargate
        zPos+=21;
        Instantiate(bigStarGate, new Vector3(0, 4, zPos), bigStarGate.transform.rotation);
        bigGateEndingZPos = zPos + 10f;
    }

    public void destoryMap()
    {
        List<GameObject> groupGateSpawner = GameObject.FindGameObjectsWithTag("GateSpawner").ToList();
        foreach (GameObject g in groupGateSpawner)
        {
            Destroy(g.gameObject);
        }

        List<GameObject> enemyShips = GameObject.FindGameObjectsWithTag("EnemyShip").ToList();
        foreach (GameObject g in enemyShips)
        {
            Destroy(g.gameObject);
        }

        List<GameObject> spawnedShips = GameObject.FindGameObjectsWithTag("SpawnedShips").ToList();
        foreach (GameObject g in spawnedShips)
        {
            Destroy(g.gameObject);
        }

        List<GameObject> asteroid = GameObject.FindGameObjectsWithTag("Asteroids").ToList();
        foreach (GameObject g in asteroid)
        {
            Destroy(g.gameObject);
        }

        List<GameObject> currency = GameObject.FindGameObjectsWithTag("Currency").ToList();
        foreach (GameObject g in currency)
        {
            Destroy(g.gameObject);
        }

        Destroy(GameObject.Find("CenterCarrier(Clone)"));
        Destroy(GameObject.Find("bigStarGate(Clone)"));
    }

    private static List<MapLevelClass> LoadResourceTextfile()
    {
        string filePath = "MapLevelData";

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return MyJsonConverter.Deserialize<List<MapLevelClass>>(targetFile.ToString());
    }

    private Vector3 strToVec(string sVector)
    {
        // string to vector3
        // Remove the parentheses
        if (sVector.StartsWith ("(") && sVector.EndsWith (")")) {
            sVector = sVector.Substring(1, sVector.Length-2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]) + 15f
        );

        return result;
    }
}
