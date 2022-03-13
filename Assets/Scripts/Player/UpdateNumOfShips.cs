using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateNumOfShips : MonoBehaviour
{
    private Spawn _spawn;

    void Start()
    {
        _spawn = GameObject.Find("Carrier").GetComponent<Spawn>();
    }

    void Update()
    {
        int count = _spawn.Count;
        gameObject.GetComponent<TextMeshProUGUI>().text = count.ToString();
    }
}
