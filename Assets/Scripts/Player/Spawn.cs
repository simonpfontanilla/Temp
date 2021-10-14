using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Rigidbody player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBall()
    {
        GameObject go = Instantiate(player).gameObject;
        go.AddComponent<Destroy>();

        go.AddComponent<Follow>();
        go.GetComponent<Follow>().leader = GameObject.Find("PlayerShip_FBX").GetComponent<Transform>();
        
        Destroy(go.GetComponent<Spawn>());

        var children = new List<GameObject>();
        foreach (Transform child in go.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Respawn"){
            SpawnBall();
        }
        else if (other.gameObject.tag == "EditorOnly"){
            transform.position = new Vector3(0,0,2);
        }
    }
}
