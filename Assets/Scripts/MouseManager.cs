using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private StartGame startGame;

    public void Start(){
        startGame = GameObject.Find("HUD_Canvas").GetComponent<StartGame>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        startGame.enabled = false;
    }


    public void OnPointerExit(PointerEventData pointerEventData)
    {
        startGame.enabled = true;
    }
}