using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Spawn spawnScript;
    [SerializeField] private PlayerChild playerScript;
    public PlayerPrefsHolder playerPrefsHolder = new PlayerPrefsHolder();

    public int level = 0;

    // UI
    public GameObject UICamera, light1, light2,
        HUDCanvas, Carrier,
        Maploader, orbit, directionalLight,
        Carrier_MainUI, Store_Panel, StartMenu_Active,
        UpgradeHolder, GameOverUI, GameWinUI;
    
    [SerializeField] private TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        levelText = GameObject.Find("Level_Title").GetComponent<TextMeshProUGUI>();
        level = playerPrefsHolder.getLevel();
        levelText.text = "Level " + level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            level = 0;
        }
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
            Carrier.GetComponent<MeshRenderer>().enabled = false;
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
            Carrier.GetComponent<MeshRenderer>().enabled = true;
            Carrier_MainUI.SetActive(true);
            Store_Panel.SetActive(true);
            StartMenu_Active.SetActive(true);
            UpgradeHolder.SetActive(true);
            HUDCanvas.GetComponent<StartGame>().enabled = true;
        }
    }
}
