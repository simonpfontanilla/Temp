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

    public GameObject levelCompleteScreen;
    public GameObject gameOverScreen;
    public GameObject startScreen;
    public GameObject mainGameScreen;

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

    //Call this function to trigger GameOver
    public void gameOverTrigger()
    {
        //Display GameOver text, vignette in the back, retry and quit buttons
        gameOverScreen.SetActive(true);

    }

    //Call this function to trigger LevelComplete
    public void levelCompleteTrigger()
    {
        //Display Level Complete Screen
        levelCompleteScreen.SetActive(true);
    }

    public void startGame()
    {
        startScreen.SetActive(false);
        mainGameScreen.SetActive(true);
        //Do something.....

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
