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
    //private GameObject go;
    private bool temp = false;
    Dictionary<GameObject, Vector3> dict = new Dictionary<GameObject, Vector3>();
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
            // if(i%2 == 0){
            //     go.transform.position = new Vector3(1,1,1);
            // }else{
            //     go.transform.position = new Vector3(2,2,2);
            // }
            //start coroutine
            //StartCoroutine(MoveFromTo(go.transform, go.transform.position, new Vector3(Mathf.Cos(12*(i+count))*radius, Mathf.Sin(12*(i+count))*radius, 3*rings), 1));
            //dict.Add(go,go.transform.position);
            //Debug.Log(GameObject.Find("FighterComplete(Clone)").GetComponent<Transform>().localPosition);
            //StartCoroutine(Test(go, i, rings, count));
            go.transform.localPosition = new Vector3(Mathf.Cos(12*(i+count))*radius, Mathf.Sin(12*(i+count))*radius, -2*rings); //original
            EditShip(ship, go);
        }
        //Debug.Log(PlayerPrefs.GetInt("currency"));
    }

    // IEnumerator MoveFromTo(GameObject objectToMove, Vector3 a, Vector3 b, float speed) {
    //     //float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime*100;
    //     float step = 0.1f;
    //     float t = 0;
    //     while (t <= 1.0f) {
    //         t += step; // Goes from 0 to 1, incrementing by step each time
    //         objectToMove.transform.localPosition = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
    //         Debug.Log(objectToMove.transform.position);
    //         //yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
    //         yield return new WaitForSeconds(1);
    //     }
    //     objectToMove.transform.localPosition = b;
    //     Debug.Log(objectToMove.transform.position);
    // }

    // IEnumerator Test(GameObject go, int i, int rings, int count){
    //     Vector3 destination = new Vector3(10,10,10);
    //     Vector3 position = dict[go];
    //     for(int j = 1; j < 50; j++){
    //         float step = 1.0f * Time.deltaTime;
    //         Vector3 newPosition = Vector3.Lerp(position, destination, step);
    //         position = newPosition;
    //         dict[go] = newPosition;
    //         //Debug.Log(dict[go]);
    //         // if(i%2 == 0){
    //         //     if(temp){
    //         //         //go.SetActive(true);
    //         //         Debug.Log("TEMP");
    //         //         for (int k = 0; k < 50; k++)
    //         //         {
    //         //             go.transform.localPosition = Vector3.MoveTowards(go.transform.localPosition, a, step);
    //         //             Debug.Log("testing");
    //         //             Debug.Log(go.transform.position);
    //         //         }
    //         //         temp = false;
    //         //     }else{
    //         //         //go.SetActive(false);
    //         //         //go.transform.localPosition = new Vector3(0,0,0);
    //         //         temp = true;
    //         //     }
    //         //     Debug.Log("even");
    //         //     Debug.Log(go.transform.localPosition);
    //         // }else{
    //         //     if(temp){
    //         //         //go.SetActive(true);
    //         //         go.transform.localPosition = Vector3.MoveTowards(go.transform.localPosition, b, step);
    //         //         temp = false;
    //         //     }else{
    //         //         //go.SetActive(false);
    //         //         //go.transform.localPosition = new Vector3(0,0,0);
    //         //         temp = true;
    //         //     }
    //         //     Debug.Log("odd");
    //         //     Debug.Log(go.transform.localPosition);
    //         // }
    //         //yield return new WaitForFixedUpdate();
    //         yield return new WaitForEndOfFrame();
    //     }
    // }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Respawn"){
            SpawnShip(12); //get text from gate for multiplier
        }
        else if (other.gameObject.tag == "Currency"){
            SpawnShip(2);
            Destroy(other.gameObject);
            GameObject currency = Instantiate(curr, transform.position + new Vector3(0,2,2), Quaternion.identity).gameObject;
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
            yield return new WaitForSeconds(1);
        }
    }
}
