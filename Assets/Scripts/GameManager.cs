using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Spawn spawnScript;
    [SerializeField] private PlayerChild playerScript;

    public int level = 0;

    // UI
    public GameObject UICamera, light1, light2,
        HUDCanvas, Carrier,
        Maploader, orbit, directionalLight,
        Carrier_MainUI, Store_Panel, StartMenu_Active,
        UpgradeHolder, GameOverUI, GameWinUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(bool check)
    {
        if (check)
        {            
            UICamera.SetActive(true);
            light1.SetActive(true);
            light2.SetActive(true);
            HUDCanvas.SetActive(true);
            HUDCanvas.GetComponent<StartGame>().enabled = false;

            Carrier_MainUI.SetActive(false);
            Store_Panel.SetActive(false);
            StartMenu_Active.SetActive(false);
            UpgradeHolder.SetActive(false);
            GameOverUI.SetActive(true);
            Carrier.SetActive(false);
            Carrier.transform.position = new Vector3(0,4.07819986f,-15);
            Carrier.GetComponent<Movement>().reset();

            Maploader.GetComponent<MapLoader>().destoryMap();
            Maploader.SetActive(false);
            
            orbit.SetActive(false);
            directionalLight.SetActive(false);

            // rest map
        }
        else
        {
            GameOverUI.SetActive(false);
            Carrier_MainUI.SetActive(true);
            Store_Panel.SetActive(true);
            StartMenu_Active.SetActive(true);
            UpgradeHolder.SetActive(true);
            HUDCanvas.GetComponent<StartGame>().enabled = true;
        }
    }
}
