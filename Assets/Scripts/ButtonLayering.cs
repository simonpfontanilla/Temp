using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLayering : MonoBehaviour
{
    [SerializeField]
    private Button button;

    private Transform ship;

    private Transform carrier;

    private Transform trail;

    private Transform buttonHolder;

    private Transform shipPanel;

    private Transform carrierPanel;

    private Transform trailsPanel;

    private Transform shopPanel;

    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
        buttonHolder = GameObject.Find("Shop_Button_Holder").transform;
        ship = GameObject.Find("Ship Tab").transform;
        carrier = GameObject.Find("Carrier Tab").transform;
        trail = GameObject.Find("Trail Tab").transform;
        shopPanel = GameObject.Find("Shop_Panel").transform;
    }

    public void TaskOnClick(){
        Transform prevButton = GameObject.Find("Shop_Button_Holder").transform.GetChild(0);
        int index = transform.GetSiblingIndex();
        prevButton.SetSiblingIndex(index);
        transform.SetSiblingIndex(2);
        if(buttonHolder.GetChild(1).gameObject.name == "Ship Tab"){
            //prevent buttons from changing state unless another tab is selected
            carrier.gameObject.GetComponent<Button>().interactable = true;
            trail.gameObject.GetComponent<Button>().interactable = true;
            ship.gameObject.GetComponent<Button>().interactable = false;
            //change the priority of the tabs in the heirarchy
            carrier.parent = buttonHolder;
            trail.parent = buttonHolder;
            ship.parent = shopPanel;
            //change the position of the tabs within each group
            carrier.SetSiblingIndex(1);
            trail.SetSiblingIndex(0);
            ship.SetSiblingIndex(shipPanel.GetSiblingIndex());
        }else if(buttonHolder.GetChild(1).gameObject.name == "Trail Tab"){
            //prevent buttons from changing state unless another tab is selected
            ship.gameObject.GetComponent<Button>().interactable = true;
            carrier.gameObject.GetComponent<Button>().interactable = true;
            trail.gameObject.GetComponent<Button>().interactable = false;
            //change the priority of the tabs in the heirarchy
            ship.parent = buttonHolder;
            carrier.parent = buttonHolder;
            trail.parent = shopPanel;
            //change the position of the tabs within each group
            ship.SetSiblingIndex(0);
            carrier.SetSiblingIndex(1);
            trail.SetSiblingIndex(shipPanel.GetSiblingIndex());
        }else if(buttonHolder.GetChild(1).gameObject.name == "Carrier Tab"){
            //prevent buttons from changing state unless another tab is selected
            ship.gameObject.GetComponent<Button>().interactable = true;
            trail.gameObject.GetComponent<Button>().interactable = true;
            carrier.gameObject.GetComponent<Button>().interactable = false;
            //change the priority of the tabs in the heirarchy
            ship.parent = buttonHolder;
            trail.parent = buttonHolder;
            carrier.parent = shopPanel;
            //change the position of the tabs within each group
            carrier.SetSiblingIndex(shipPanel.GetSiblingIndex());
        }
    }
}
