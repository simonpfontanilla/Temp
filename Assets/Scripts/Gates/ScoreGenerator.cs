using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreGenerator : MonoBehaviour
{
    [SerializeField] bool goodGate = false;

    // Start is called before the first frame update
    void Awake()
    {
        string str = "";

        if (goodGate)
        {
            if (Random.Range(0, 2) == 0) str = "+ " + Random.Range(1, 101);
            else str = "x " + Random.Range(1, 101);
        }
        else
        {
            if (Random.Range(0, 2) == 0) str = "- " + Random.Range(1, 101);
            else str = "÷ " + Random.Range(1, 101);
        }

        gameObject.GetComponent<TextMeshPro>().SetText(str);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
