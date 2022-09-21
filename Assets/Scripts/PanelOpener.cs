using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    [SerializeField]
    MeshRenderer carrierMesh;
    [SerializeField]
    GameObject supportUsPage;
    [SerializeField]
    GameObject storePanel;
    [SerializeField]
    GameObject startMenuActive;
    [SerializeField]
    GameObject carrier;

    public void OpenPanel(){
        if(supportUsPage != null){
            bool isActive = supportUsPage.activeSelf;
            carrierMesh.enabled = isActive;
            supportUsPage.SetActive(!isActive);
            storePanel.SetActive(isActive);
            startMenuActive.SetActive(isActive);
            // carrier.SetActive(!isActive);
        }
    }
}
