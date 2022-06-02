using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateCurrency : MonoBehaviour
{
    [SerializeField]
    private Player _ship;
    [SerializeField]
    private TextMeshProUGUI _text;

    void Start()
    {
        // _ship = GameObject.Find("Carrier").GetComponent<Player>();
        _text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        int curr = _ship.Currency;
        // int curr = 999999;
        // int curr = PlayerPrefs.GetInt("currency");
        _text.text = curr.ToString();
        PlayerPrefs.SetInt("currency", curr);
    }
}
