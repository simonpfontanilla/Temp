using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    public int shopIndex = 0;
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

    //The Transform (source) for the 3 shop pages/tabs
    public Transform panel1;
    public Transform panel2;
    public Transform panel3;

    //Scroll Rects for each Shop Panel
    public ScrollRect panel1SR;
    public ScrollRect panel2SR;
    public ScrollRect panel3SR;

    // Upgrade Text Lables for Ship+ and Income+
    public TextMeshProUGUI shipUpLevel;
    public TextMeshProUGUI shipUpCost;
    public TextMeshProUGUI incomeUpLevel;
    public TextMeshProUGUI incomeUpCost;

    //int values for Ship+ and Income+
    public int shipLevel = 0;
    public int shipCost = 0;
    public int incomeLevel = 0;
    public int incomeCost = 0;


    // Initial Shop Tab
    //public GameObject initialShopTab;
    public Button initialShopTab;
    public Button shipUpgrade;
    public Button incomeUpgrade;

    //Icon Array
    public Sprite[] shipIcons;
    public Sprite[] carrierIcons;
    public Sprite[] trailIcons;

    //Skins Arrays (Ship, Carrier, Trail)
    public Material[] shipMat;
    public Material[] carrierMat;
    public Material[] trailMat;

    //Initial Values
    public int totalCurrency = 0;
    public int currentShipSkinValue;
    public int currentCarrierSkinValue;
    public int currentTrailSkinValue;
    public TextMeshProUGUI currencyAmount;

    //Dropdown Object Controller
    public DropdownMenu settingsDropdown;
    //HUD animator
    public Animator hudAnim;

    // Start is called before the first frame update
    void Start()
    {
        shopIndex = 1;
        totalCurrency = PlayerPrefs.GetInt("currency", 0);
        currencyAmount.text = totalCurrency.ToString();

        shipLevel = PlayerPrefs.GetInt("shipLevel", 0);
        shipUpLevel.text = shipLevel.ToString();
        shipCost = PlayerPrefs.GetInt("shipCost", 20);
        shipUpCost.text = shipCost.ToString();

        incomeLevel = PlayerPrefs.GetInt("incomeLevel", 0);
        incomeUpLevel.text = incomeLevel.ToString();
        incomeCost = PlayerPrefs.GetInt("incomeCost", 20);
        incomeUpCost.text = incomeCost.ToString();

        currentShipSkinValue = PlayerPrefs.GetInt("ShipSkinVal", 0);
        currentCarrierSkinValue = PlayerPrefs.GetInt("CarrierSkinVal", 0);
        currentTrailSkinValue = PlayerPrefs.GetInt("TrailSkinVal", 0);


        //Add button-on-click events to shop Buttons
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

        //Check if player has enough currency before allowing interactable
        checkUpgradeAccess(); 

        //Update ship level and income level in real-time
        shipLevel = PlayerPrefs.GetInt("shipLevel", 0);
        shipUpLevel.text = shipLevel.ToString();
        shipCost = PlayerPrefs.GetInt("shipCost", 0);
        shipUpCost.text = shipCost.ToString();

        incomeLevel = PlayerPrefs.GetInt("incomeLevel", 0);
        incomeUpLevel.text = incomeLevel.ToString();
        incomeCost = PlayerPrefs.GetInt("incomeCost", 0);
        incomeUpCost.text = incomeCost.ToString();
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            //Initiate SwipeOut transition
            //screenSlideOut();

            //startGame();



            //Testing Currency Usage in Shop
            tempAddCurrency();
        }
    }

    public void resetUpgrades()
    {
        shipLevel = 0;
        PlayerPrefs.SetInt("shipLevel", shipLevel);

        shipCost = 0;
        PlayerPrefs.SetInt("shipCost", shipCost);

        incomeLevel = 0;
        PlayerPrefs.SetInt("incomeLevel", incomeLevel);

        incomeCost = 0;
        PlayerPrefs.SetInt("incomeCost", incomeCost);
    }

    public void shipUp()
    {
         shipLevel = shipLevel + 1;
        PlayerPrefs.SetInt("shipLevel", shipLevel);
        Debug.Log("Ship level is "+shipLevel);

        totalCurrency = totalCurrency - shipCost;
        PlayerPrefs.SetInt("currency", totalCurrency);
        Debug.Log("Total currency is: " + totalCurrency);

        shipCost = shipCost + 20;
        PlayerPrefs.SetInt("shipCost", shipCost);
        Debug.Log("Ship cost is "+shipCost);
    }

    public void incomeUp()
    {
        incomeLevel = incomeLevel +1;
        PlayerPrefs.SetInt("incomeLevel", incomeLevel);
        Debug.Log("Income level is "+incomeLevel);

        totalCurrency = totalCurrency - incomeCost;
        PlayerPrefs.SetInt("currency", totalCurrency);
        Debug.Log("Total currency is: " + totalCurrency);

        incomeCost = incomeCost + 20;
        PlayerPrefs.SetInt("incomeCost", incomeCost);
        Debug.Log("Income cost is "+incomeCost);
    }

    public void screenSlideOut()
    {

        settingsDropdown.swipeOut();
        hudSlideOut();
    }

    public void hudSlideOut()
    {
        hudAnim.SetBool("GameStart", true);
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

        //TODO: Create a larger loop that checks each GameObject(Price Category) within a Shop Panel
        //Then applies the foreach mapping function

        //foreach (Transform/GameObject G in Panel )


        foreach (Transform t in panel1)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.GetComponent<Image>().sprite = shipIcons[i];
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
            b2.GetComponent<Image>().sprite = carrierIcons[i];
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
            //b3.GetComponent<Image>().sprite = trailIcons[i];
            b3.onClick.AddListener(() => OnPanel3Select(currentIndex3));

            i++;

        }

    }

    //Check Currency For Ship Upgrade
    public void checkShipCurrencyUpgrade()
    {
      if(totalCurrency < shipCost)
      {
         // Debug.Log("Total Currency is less than ship upgrade cost!");
          shipUpgrade.interactable = false;
      } 

      else 
      {
         //Debug.Log("Total Currency is MORE than or equal ship upgrade cost!");
          shipUpgrade.interactable = true;
      }
    }

     //Check Currency For Income Upgrade
    public void checkIncomeCurrencyUpgrade()
    {
        if(totalCurrency < incomeCost)
      {
         // Debug.Log("Total Currency is less than income upgrade cost!");
          incomeUpgrade.interactable = false;
      } 

      else
      {
        // Debug.Log("Total Currency is MORE than or equal income upgrade cost!");
         incomeUpgrade.interactable = true;
    
      }
    }

    public void checkUpgradeAccess()
    {
        checkShipCurrencyUpgrade();
        checkIncomeCurrencyUpgrade();
    }

    //Purchase from Page 1, adjust category for cost
    private void purchaseItemPanel1(int currentIndex)
    {
        //Debug.Log("")

        if (currentIndex < 11)
        {
            totalCurrency = totalCurrency - 50;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            //Apply skin/material change here
            currentShipSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentShipSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentShipSkinValue);
            //ShipGameObject.Material = ShipMat[currentShipSkinValue];
        }
        else if (currentIndex >= 11 && currentIndex < 19)
        {
            totalCurrency = totalCurrency - 100;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentShipSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentShipSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentShipSkinValue);
        }
        else if (currentIndex >= 19 && currentIndex < 23)
        {
            totalCurrency = totalCurrency - 150;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentShipSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentShipSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentShipSkinValue);
        }
        else if (currentIndex >= 23 && currentIndex < 29)
        {
            totalCurrency = totalCurrency - 2-0;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentShipSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentShipSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentShipSkinValue);
        }
         else if (currentIndex >= 29 && currentIndex < 34)
        {
            totalCurrency = totalCurrency - 250;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentShipSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentShipSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentShipSkinValue);
        }

    }

    //Purchase from Page 2, adjust category for cost
    private void purchaseItemPanel2(int currentIndex)
    {
        //Debug.Log("")

        if (currentIndex < 11)
        {
            totalCurrency = totalCurrency - 50;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentCarrierSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentCarrierSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentCarrierSkinValue);
            //CarrierGameObject.Material = CarrierMat[currentCarrierSkinValue];
        }
        else if (currentIndex >= 11 && currentIndex < 19)
        {
            totalCurrency = totalCurrency - 100;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentCarrierSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentCarrierSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentCarrierSkinValue);
        }
        else if (currentIndex >= 19 && currentIndex < 23)
        {
            totalCurrency = totalCurrency - 150;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentCarrierSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentCarrierSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentCarrierSkinValue);
        }
        else if (currentIndex >= 23 && currentIndex < 29)
        {
            totalCurrency = totalCurrency - 2-0;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentCarrierSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentCarrierSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentCarrierSkinValue);
        }
         else if (currentIndex >= 29 && currentIndex < 34)
        {
            totalCurrency = totalCurrency - 250;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentCarrierSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentCarrierSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentCarrierSkinValue);
        }

    }

    //Purchase from Page 3, adjust category for cost
    private void purchaseItemPanel3(int currentIndex)
    {
        //Debug.Log("")

        if (currentIndex < 11)
        {
            totalCurrency = totalCurrency - 50;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentTrailSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentTrailSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentTrailSkinValue);
            //TrailGameObject.Material = TrailMat[currentTrailSkinValue];
        }
        else if (currentIndex >= 11 && currentIndex < 19)
        {
            totalCurrency = totalCurrency - 100;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentTrailSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentTrailSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentTrailSkinValue);
        }
        else if (currentIndex >= 19 && currentIndex < 23)
        {
            totalCurrency = totalCurrency - 150;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentTrailSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentTrailSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentTrailSkinValue);
        }
        else if (currentIndex >= 23 && currentIndex < 29)
        {
            totalCurrency = totalCurrency - 2-0;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentTrailSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentTrailSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentTrailSkinValue);
        }
         else if (currentIndex >= 29 && currentIndex < 34)
        {
            totalCurrency = totalCurrency - 250;
            PlayerPrefs.SetInt("currency", totalCurrency);
            Debug.Log("Total currency is: " + totalCurrency);

            currentTrailSkinValue = currentIndex;
            PlayerPrefs.SetInt("ShipSkinVal", currentTrailSkinValue);
            Debug.Log("Current Ship Skin Index is: "+ currentTrailSkinValue);
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
        resetScroll();
        //return shopIndex;
    }

    public void resetScroll()
    {
         panel1SR.verticalNormalizedPosition = 0f;
         panel2SR.verticalNormalizedPosition = 0f;
         panel3SR.verticalNormalizedPosition = 0f;
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
               
                shopPage1.SetActive(true);
                shopPage2.SetActive(false);
                shopPage3.SetActive(false);
                shipSkinModel.SetActive(true);
                carrierSkinModel.SetActive(false);
                shipTrailModel.SetActive(false);

                break;

            case 2:
                
                shopPage2.SetActive(true);
                shopPage1.SetActive(false);
                shopPage3.SetActive(false);
                shipSkinModel.SetActive(false);
                carrierSkinModel.SetActive(true);
                shipTrailModel.SetActive(false);
                break;

            case 3:
                
                shopPage3.SetActive(true);
                shopPage1.SetActive(false);
                shopPage2.SetActive(false);
                shipSkinModel.SetActive(false);
                carrierSkinModel.SetActive(false);
                shipTrailModel.SetActive(true);
                break;


            default:
                
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
