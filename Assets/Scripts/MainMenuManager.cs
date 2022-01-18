using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public int shopIndex = 0;
    public TextMeshProUGUI shopPageLabel;
    public GameObject shopPage1;
    public GameObject shopPage2;
    public GameObject shopPage3;

    // Start is called before the first frame update
    void Start()
    {
        shopIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        checkPage();
    }

    public void choosePage(int Index)
    {
       shopIndex = Index;
        //return shopIndex;
    }

    public void checkPage()
    {
        switch (shopIndex)
        {
            case 1:
            shopPageLabel.text = "- Armor Skins"; 
            break;

            case 2:
            shopPageLabel.text = "- Ship Upgrades";
            break; 

            case 3:
            shopPageLabel.text = "- Power Ups";
            break; 

            
            default:
            shopPageLabel.text = "- Armor Skins";
            break;
        }
    }
}
