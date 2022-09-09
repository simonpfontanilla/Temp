using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    // [SerializeField]
    // GameObject carrierMain;
    [SerializeField]
    GameObject storePanel;
    [SerializeField]
    GameObject startMenuActive;
    [SerializeField]
    GameObject upgradeHolder;

    public void OpenPanel(){
        if(panel != null){
            bool isActive = panel.activeSelf;
            // carrierMain.SetActive(isActive);
            storePanel.SetActive(isActive);
            startMenuActive.SetActive(isActive);
            // upgradeHolder.SetActive(isActive);
            panel.SetActive(!isActive);
        }
    }
}
