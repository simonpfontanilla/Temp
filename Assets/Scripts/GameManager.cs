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
    public GameObject light1, light2,
        HUDCanvas, Carrier,
        Maploader, orbit, directionalLight,
        Store_Panel, StartMenu_Active,
        UpgradeHolder, GameOverUI, GameWinUI;
    
    [SerializeField] private TextMeshProUGUI levelText, GameTitle;

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

        if (playerPrefsHolder.getLevel() != level)
        {
            level = playerPrefsHolder.getLevel();
            levelText.text = "Level " + level.ToString();
        }
    }

    public void Game(bool win)
    {
        light1.SetActive(true);
        light2.SetActive(true);
        HUDCanvas.SetActive(true);
        HUDCanvas.GetComponent<StartGame>().enabled = false;

        Store_Panel.SetActive(false);
        StartMenu_Active.SetActive(false);
        UpgradeHolder.SetActive(false);

        if (win)
            GameWinUI.SetActive(true);
        else
            GameOverUI.SetActive(true);

        Carrier.GetComponent<MeshRenderer>().enabled = false;
        Carrier.GetComponent<Movement>().reset();
        Carrier.GetComponent<Movement>().autoMove = false;
        orbit.SetActive(false);
        directionalLight.SetActive(false);

        if (GameTitle.enabled)
            GameTitle.enabled = false;
    }

    public void resetUI()
    {
        Carrier.transform.position = new Vector3(0,4.07819986f,10.37f);
        // rest map
        Maploader.GetComponent<MapLoader>().destoryMap();
        Maploader.SetActive(false);
        GameOverUI.SetActive(false);
        GameWinUI.SetActive(false);
        Carrier.GetComponent<MeshRenderer>().enabled = true;
        Store_Panel.SetActive(true);
        StartMenu_Active.SetActive(true);
        UpgradeHolder.SetActive(true);
        HUDCanvas.GetComponent<StartGame>().enabled = true;
    }
}
