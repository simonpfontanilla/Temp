using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateNumOfShips : MonoBehaviour
{
    [SerializeField]
    private Spawn _spawn;

    private TextMeshProUGUI _text;

    void Start()
    {
        // _spawn = GameObject.Find("Carrier").GetComponent<Spawn>();
        _text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        int count = _spawn.Count;
        _text.text = count.ToString();
    }
}
