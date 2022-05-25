using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateCurrency : MonoBehaviour
{
    private Player _ship;
    [SerializeField]
    private TextMeshProUGUI _text;

    void Start()
    {
        _ship = GameObject.Find("Carrier").GetComponent<Player>();
        _text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        int curr = _ship.Currency;
        _text.text = curr.ToString();
        PlayerPrefs.SetInt("currency", curr);
    }
}
