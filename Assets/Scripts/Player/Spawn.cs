using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float scale = 30.0f;
    public Rigidbody player;
    public float radius = 2.0f;
    public GameObject curr;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LastUpdate()
    {
        // foreach(var item in dict){
        //     item.Key.transform.position = item.Value;
        //     Debug.Log(item.Key.transform.position);
        // }
        // Debug.Log(GameObject.Find("FighterComplete(Clone)").GetComponent<Transform>().position);
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
            GameObject go = Instantiate(player, transform.position, transform.rotation).gameObject;
            go.transform.localPosition = new Vector3(Mathf.Cos(12*(i+count))*radius, Mathf.Sin(12*(i+count))*radius, -2*rings); //original
            EditShip(ship, go);
        }
        //Debug.Log(PlayerPrefs.GetInt("currency"));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Respawn"){
            SpawnShip(12); //get text from gate for multiplier
        }
        else if (other.gameObject.tag == "Currency"){
            SpawnShip(2);
            Destroy(other.gameObject);
            GameObject currency = Instantiate(curr, transform.position + new Vector3(0,1,0), Quaternion.identity).gameObject;
            StartCoroutine(Wait(currency, 1));
            GameObject.Find("FighterComplete").GetComponent<Player>().currency += 1;
        }
        else if (other.gameObject.tag == "EditorOnly"){
            transform.position = new Vector3(0,0,2);
            GameObject.Find("FighterComplete").GetComponent<Player>().currency = GameObject.Find("FighterComplete").GetComponent<Player>().currency + (GameObject.Find("FighterComplete").GetComponent<Player>().children.Count + 1) * 10;
        }
    }

    IEnumerator Wait(GameObject currency, int seconds){
        for(int i = 0;i <= seconds; i++){
            if(i == seconds){
                Destroy(currency);
                yield break;
            }
            yield return StartCoroutine(Move(currency));
        }
    }

    IEnumerator Move(GameObject currency){
        Color alpha = currency.GetComponent<SpriteRenderer>().color;
        for(int i = 0;i <= 60; i++){
            currency.transform.position += new Vector3(0, 0.03f, 0);
            alpha.a -= 0.02f;
            currency.GetComponent<SpriteRenderer>().color = alpha;
            yield return new WaitForFixedUpdate();
        }
    }
}
