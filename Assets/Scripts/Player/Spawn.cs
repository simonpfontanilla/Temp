using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float scale = 30.0f;
    public Rigidbody player;
    public float radius = 2.0f;
    private int count;
    private GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EditShip(GameObject ship, GameObject go){
        go.transform.localScale = new Vector3(scale, scale, scale);
        go.tag = "SpawnedShips";
        //go.AddComponent<Destroy>();
        go.AddComponent<Follow>();
        go.GetComponent<Follow>().leader = ship.GetComponent<Transform>();
        ship.GetComponent<Player>().children.Add(go);
        Destroy(go.GetComponent<Spawn>());
        var children = new List<GameObject>();
        foreach (Transform child in go.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    int UpdateCount(GameObject ship){
        return ship.GetComponent<Player>().children.Count;
    }

    void SpawnShip(int multiplier)
    {
        GameObject ship = GameObject.Find("FighterComplete");
        count = UpdateCount(ship);
        int rings;
        for(int i = 0; i < multiplier-1; i++){
            count = UpdateCount(ship)+1;
            rings = count/12;
            go = Instantiate(player, transform.position, transform.rotation).gameObject;
            //start coroutine
            //StartCoroutine("Test");
            go.transform.localPosition = new Vector3(Mathf.Cos(12*(i+count))*radius, Mathf.Sin(12*(i+count))*radius, 3*rings);

            EditShip(ship, go);
        }
        //Debug.Log(PlayerPrefs.GetInt("currency"));
    }

    // IEnumerator Test(){
    //     int rings = 1;
    //     for(int i = 0; i < 1000; i++){
    //         go.transform.localPosition = new Vector3((Mathf.Cos(12*(i+count))*radius)/1000, (Mathf.Sin(12*(i+count))*radius)/1000, 3*rings);
    //         yield return null;
    //     }
    // }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Respawn"){
            SpawnShip(12); //get text from gate for multiplier
        }
        else if (other.gameObject.tag == "Currency"){
            SpawnShip(1);
            Destroy(other.gameObject);
            GameObject.Find("FighterComplete").GetComponent<Player>().currency += 1;
        }
        else if (other.gameObject.tag == "EditorOnly"){
            transform.position = new Vector3(0,0,2);
            GameObject.Find("FighterComplete").GetComponent<Player>().currency = GameObject.Find("FighterComplete").GetComponent<Player>().currency + (GameObject.Find("FighterComplete").GetComponent<Player>().children.Count + 1) * 10;
        }
    }
}
