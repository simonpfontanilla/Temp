using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private GameObject UICamera;
    [SerializeField]
    private GameObject light1;
    [SerializeField]
    private GameObject light2;
    [SerializeField]
    private GameObject backgroundCanvas;
    [SerializeField]
    private GameObject HUDCanvas;
    [SerializeField]
    private GameObject Carrier;
    [SerializeField]
    private GameObject Maploader;
    [SerializeField]
    private GameObject orbit;
    [SerializeField]
    private GameObject directionalLight;
    // [SerializeField]
    private GameObject shopPanel;
    // [SerializeField]
    private GameObject shipTabShip;
    // [SerializeField]
    private GameObject carrierTabShip;
    // [SerializeField]
    private GameObject trailTabShip;
    // [SerializeField]
    private GameObject backButton;
    // [SerializeField]
    private GameObject dropDownMenu;
    // [SerializeField]
    private GameObject directionalArrow;
    // [SerializeField]
    private GameObject titleHolder;
    [SerializeField]
    private GameObject mainShipModel;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        Vibration.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            foreach (Touch touch in Input.touches)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    //ui
                    UICamera.SetActive(true);
                    light1.SetActive(true);
                    light2.SetActive(true);
                    HUDCanvas.SetActive(true);
                    Carrier.SetActive(false);
                    Maploader.SetActive(false);
                    orbit.SetActive(false);
                    directionalLight.SetActive(false);
                    canvas.SetActive(false);
                    // if(Input.GetKeyUp(KeyCode.Escape)){
                    //     shopPanel.SetActive(false);
                    //     backButton.SetActive(false);
                    //     dropDownMenu.SetActive(true);
                    //     directionalArrow.SetActive(true);
                    //     shipTabShip.SetActive(false);
                    //     carrierTabShip.SetActive(false);
                    //     trailTabShip.SetActive(false);
                    //     mainShipModel.SetActive(true);
                    //     titleHolder.SetActive(true);
                    // }
                }else{
                    //start game
                    // UICamera.SetActive(false);
                    // light1.SetActive(false);
                    // light2.SetActive(false);
                    // HUDCanvas.SetActive(false);
                    // Maploader.SetActive(true);
                    // Maploader.GetComponent<MapLoader>().create();
                    // Carrier.SetActive(true);
                    // Carrier.GetComponent<Spawn>().Init();
                    // orbit.SetActive(true);
                    // directionalLight.SetActive(true);
                    // canvas.SetActive(true);
                    // Vibration.VibratePop();
                    // Debug.Log("Vibrate Start");
                    animator.SetTrigger("Trigger");
                }
            }
        }
        if(SwipeManager.IsSwipingRight())
        {
            foreach(Touch touch in Input.touches){
                if(touch.phase == TouchPhase.Ended){
                    shopPanel.SetActive(false);
                    backButton.SetActive(false);
                    dropDownMenu.SetActive(true);
                    directionalArrow.SetActive(true);
                    shipTabShip.SetActive(false);
                    carrierTabShip.SetActive(false);
                    trailTabShip.SetActive(false);
                    mainShipModel.SetActive(true);
                    titleHolder.SetActive(true);
                    canvas.SetActive(false);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            //ui
            // UICamera.SetActive(false);
            // light1.SetActive(false);
            // light2.SetActive(false);
            // HUDCanvas.SetActive(false);
            // Maploader.SetActive(true);
            // Maploader.GetComponent<MapLoader>().create();
            // Carrier.SetActive(true);
            // orbit.SetActive(true);
            // directionalLight.SetActive(true);
            // canvas.SetActive(true);
            // Carrier.GetComponent<Spawn>().Init();
            // Debug.Log("Test");
            animator.SetTrigger("Trigger");
            // animator.SetBool("Reset", true);
        }
    }

    public void DisableUI(){
        HUDCanvas.SetActive(false);
        UICamera.SetActive(false);
        light1.SetActive(false);
        light2.SetActive(false);
        Maploader.SetActive(true);
        Maploader.GetComponent<MapLoader>().create();
        Carrier.SetActive(true);
        orbit.SetActive(true);
        directionalLight.SetActive(true);
        Carrier.GetComponent<Movement>().enabled = true;
        Carrier.GetComponent<Movement>().autoMove = true;
        Carrier.GetComponent<Spawn>().Init();
        canvas.SetActive(true);
        Vibration.VibratePop();
        // Debug.log("Vibrate Start");
    }

    public void EnableUI(){
        UICamera.SetActive(true);
        light1.SetActive(true);
        light2.SetActive(true);
        HUDCanvas.SetActive(true);
        Carrier.SetActive(false);
        Maploader.SetActive(false);
        orbit.SetActive(false);
        directionalLight.SetActive(false);
        canvas.SetActive(false);
    }
}

