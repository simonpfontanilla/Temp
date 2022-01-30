using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateCurrency : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int curr = GameObject.Find("Carrier").GetComponent<Player>().currency;
        gameObject.GetComponent<TextMeshProUGUI>().text = "Currency: " + curr.ToString();
        PlayerPrefs.SetInt("currency", curr);
    }
}
