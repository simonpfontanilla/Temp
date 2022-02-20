using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateNumOfShips : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //List<GameObject> lst = GameObject.Find("Carrier").GetComponent<Player>().children;
        int count = GameObject.Find("Carrier").GetComponent<Spawn>()._count;
        gameObject.GetComponent<TextMeshProUGUI>().text = count.ToString();
    }
}
