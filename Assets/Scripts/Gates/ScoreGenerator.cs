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
            str = "+ " + Random.Range(1, 13);
            // if (Random.Range(0, 2) == 0) str = "+ " + Random.Range(1, 13);
            // else
            // {
            //     // 1 - 5 and 10 for rare
            //     str = "x " + Random.Range(1, 6);
                
            //     if (new System.Random().Next(1, 100) < 10) str = "x 10";
            // }
        }
        else
        {
            if (Random.Range(0, 2) == 0) str = "- " + Random.Range(1, 15);
            else str = "÷ " + Random.Range(1, 10);
        }

        gameObject.GetComponent<TextMeshPro>().SetText(str);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
