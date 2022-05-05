using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)){
            UICamera.SetActive(false);
            light1.SetActive(false);
            light2.SetActive(false);
            backgroundCanvas.SetActive(false);
            HUDCanvas.SetActive(false);
            Carrier.SetActive(true);
            Maploader.SetActive(true);
            orbit.SetActive(true);
            directionalLight.SetActive(true);
        }
    }
}
