using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    public int shopIndex = 0;
    public TextMeshProUGUI shopPageLabel;
    public GameObject titleHolder;
    public GameObject shopPage1;
    public GameObject shopPage2;
    public GameObject shopPage3;
    public GameObject Store_Panel;

    //Player Models
    public GameObject mainShipModel;
    public GameObject shipSkinModel;
    public GameObject carrierSkinModel;
    public GameObject shipTrailModel;

    public GameObject levelCompleteScreen;
    public GameObject gameOverScreen;
    public GameObject startScreen;
    public GameObject mainGameScreen;

    public Transform panel1;
    public Transform panel2;
    public Transform panel3;

    // Initial Shop Tab
    //public GameObject initialShopTab;
    public Button initialShopTab;
    //Icon Array
    public Sprite[] skinsIcons;

    //Initial Values
    public int totalCurrency = 0;
    public TextMeshProUGUI currencyAmount;

    // Start is called before the first frame update
    void Start()
    {
        shopIndex = 1;
        totalCurrency = PlayerPrefs.GetInt("currency", 0);
        currencyAmount.text = totalCurrency.ToString();
        //Add button-pn-click events to shop Buttons
        InitShop();
    }

    void tempAddCurrency()
    {
        totalCurrency = totalCurrency + 50;
    }

    // Update is called once per frame
    void Update()
    {
        // checkPage();
        currencyAmount.text = totalCurrency.ToString();
        if (Input.GetKeyDown("space"))
        {
            startGame();
            //Testing Currency Usage in Shop
            //tempAddCurrency();
        }
    }

    public void setInitialTab()
    {
        initialShopTab.Select();
        Debug.Log("Ship skin selected by default");
    }

    private async void InitShop()
    {
        //Button x = initialShopTab.GetComponent<Button>();
        //x.Select();

        //Assign the references
        if (panel1 == null || panel2 == null || panel3 == null)
        {
            Debug.Log("At least one of the panels is not assigned");
        }
        //For every children transform under panel1, find button and add onClick method
        int i = 0;
        foreach (Transform t in panel1)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.GetComponent<Image>().sprite = skinsIcons[i];
            b.onClick.AddListener(() => OnPanel1Select(currentIndex));

            i++;

        }

        //Reset index;
        i = 0;
        //Do the same for Panel 2
        foreach (Transform t in panel2)
        {
            int currentIndex2 = i;
            Button b2 = t.GetComponent<Button>();
            b2.onClick.AddListener(() => OnPanel2Select(currentIndex2));

            i++;

        }

        //Reset index;
        i = 0;
        //Do the same for Panel 2
        foreach (Transform t in panel3)
        {
            int currentIndex3 = i;
            Button b3 = t.GetComponent<Button>();
            b3.onClick.AddListener(() => OnPanel3Select(currentIndex3));

            i++;

        }

    }

    //Purchase from Page 1, adjust category for cost
    private void purchaseItemPanel1(int currentIndex)
    {
        //Debug.Log("")

        if (currentIndex < 3)
        {
            totalCurrency = totalCurrency - 50;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);
        }
        else if (currentIndex >= 3 && currentIndex < 6)
        {
            totalCurrency = totalCurrency - 100;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);
        }
        else if (currentIndex >= 6 && currentIndex < 9)
        {
            totalCurrency = totalCurrency - 150;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);
        }

    }

    //Purchase from Page 2, adjust category for cost
    private void purchaseItemPanel2(int currentIndex)
    {
        //Debug.Log("")

        if (currentIndex < 3)
        {
            totalCurrency = totalCurrency - 50;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);
        }
        else if (currentIndex >= 3 && currentIndex < 6)
        {
            totalCurrency = totalCurrency - 100;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);
        }
        else if (currentIndex >= 6 && currentIndex < 9)
        {
            totalCurrency = totalCurrency - 150;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);
        }

    }

    //Purchase from Page 3, adjust category for cost
    private void purchaseItemPanel3(int currentIndex)
    {
        //Debug.Log("")

        if (currentIndex < 3)
        {
            totalCurrency = totalCurrency - 50;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);
        }
        else if (currentIndex >= 3 && currentIndex < 6)
        {
            totalCurrency = totalCurrency - 100;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);
        }
        else if (currentIndex >= 6 && currentIndex < 9)
        {
            totalCurrency = totalCurrency - 150;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);
        }

    }

    //This function will be used to add functionality to each button press within Panel
    private void OnPanel1Select(int currentIndex)
    {
        //throw new NotImplementedException();

        Debug.Log("Selecting item from Panel 1: Item #" + currentIndex);
        purchaseItemPanel1(currentIndex);
        //Simulate purchase

    }

    private void OnPanel2Select(int currentIndex)
    {
        // throw new NotImplementedException();
        Debug.Log("Selecting item from Panel 2: Item #" + currentIndex);
        purchaseItemPanel2(currentIndex);
    }

    private void OnPanel3Select(int currentIndex)
    {
        // throw new NotImplementedException();
        Debug.Log("Selecting item from Panel 3: Item #" + currentIndex);
        purchaseItemPanel3(currentIndex);
    }

    public void OnPanel1BuySet()
    {
        Debug.Log("Buy/Set Panel1");
    }

    public void OnPanel2BuySet()
    {
        Debug.Log("Buy/Set Panel2");
    }


    //Call this function to trigger GameOver
    public void gameOverTrigger()
    {
        //Display GameOver text, vignette in the back, retry and quit buttons
        gameOverScreen.SetActive(true);
        //mainGameScreen.SetActive(false);

    }

    //Call this function to trigger LevelComplete
    public void levelCompleteTrigger()
    {
        //Display Level Complete Screen
        levelCompleteScreen.SetActive(true);
        //mainGameScreen.SetActive(false);
    }

    public void startGame()
    {
        startScreen.SetActive(false);
        Store_Panel.SetActive(false);
        mainGameScreen.SetActive(true);
        //Do something.....

    }

    public void choosePage(int Index)
    {
        shopIndex = Index;
        checkPage();
        //return shopIndex;
    }

    public void returnToMain()
    {
        mainShipModel.SetActive(true);
        shipSkinModel.SetActive(false);
        carrierSkinModel.SetActive(false);
        shipTrailModel.SetActive(false);
        titleHolder.SetActive(true);
    }

    public void checkPage()
    {
        switch (shopIndex)
        {
            case 1:
                shopPageLabel.text = "- Ships";
                shopPage1.SetActive(true);
                shopPage2.SetActive(false);
                shopPage3.SetActive(false);
                shipSkinModel.SetActive(true);
                carrierSkinModel.SetActive(false);
                shipTrailModel.SetActive(false);

                break;

            case 2:
                shopPageLabel.text = "- Carrier";
                shopPage2.SetActive(true);
                shopPage1.SetActive(false);
                shopPage3.SetActive(false);
                shipSkinModel.SetActive(false);
                carrierSkinModel.SetActive(true);
                shipTrailModel.SetActive(false);
                break;

            case 3:
                shopPageLabel.text = "- Trails";
                shopPage3.SetActive(true);
                shopPage1.SetActive(false);
                shopPage2.SetActive(false);
                shipSkinModel.SetActive(false);
                carrierSkinModel.SetActive(false);
                shipTrailModel.SetActive(true);
                break;


            default:
                shopPageLabel.text = "- Ships";
                shopPage1.SetActive(true);
                shopPage1.SetActive(false);
                shopPage1.SetActive(false);
                shipSkinModel.SetActive(true);
                carrierSkinModel.SetActive(false);
                shipTrailModel.SetActive(false);
                break;
        }
    }
}
