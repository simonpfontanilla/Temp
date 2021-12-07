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
        List<GameObject> lst = GameObject.Find("FighterComplete").GetComponent<Player>().children;
        gameObject.GetComponent<TextMeshProUGUI>().text = lst.Count.ToString();
    }
}
