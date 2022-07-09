using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ScrollingLimit : MonoBehaviour
{
    // Listing all 3 Scroll Rects
    // Create Checks using verticalNormalizedPosition, find value first, 
    // Then see at what value is the "end" of the content

    //Y content limit -> 276 or 278, use that as maximum y-displacement of scrolling


    //public void resetScroll()
    //{
     //    panel1SR.verticalNormalizedPosition = 0f;
       //  panel2SR.verticalNormalizedPosition = 0f;
        // panel3SR.verticalNormalizedPosition = 0f;
    //}

    //Define Scrolling Panel and limit of scrolling
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] float upperScrollLimit = 0.9f;
    [SerializeField] float lowerScrollLimit = 0.5f;
    public GameObject panel1;

      public void LimitScroll(){
        // when hovering over ScrollRect and dragging/using mouse wheel for Scrolling
        if (scrollRect.verticalNormalizedPosition > upperScrollLimit)
        {
            Debug.Log("The vertical Normalized position is above upperscroll limit");
            scrollRect.verticalNormalizedPosition = Mathf.Clamp(scrollRect.verticalNormalizedPosition, 0, upperScrollLimit);
        }

         else if ( panel1.GetComponent<RectTransform>().anchoredPosition.y < 238)
        {
            Debug.Log("You've reached the end of the panel, can't scroll further");
            //Returns Scrolling Ability (temporary solution)
            scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

           
        }
        
        //If scrolling passed a certain y-value for the Content Panel, prevent infinite scrolling
        else if ( panel1.GetComponent<RectTransform>().anchoredPosition.y > 238)
        {
            Debug.Log("You've reached the end of the panel, can't scroll further");
            
            //TODO: Find a way to restrict movement... possibly make it temporarily elastic
            //scrollRect.vertical = false;

            scrollRect.movementType = ScrollRect.MovementType.Elastic;

           // scrollRect.verticalNormalizedPosition = 1.0f;
          // scrollRect.verticalNormalizedPosition = Mathf.Clamp(scrollRect.verticalNormalizedPosition, lowerScrollLimit, 0); 
        }



        //else
       // {
         //   Debug.Log("Vertical Normalized position is below the scroll limit, continue scrolling!");
       // }
      /*  // when scrolling via Scrollbar attached to ScrollRect
        if (scrollbar.value > upperScrollLimit)
        {
            scrollbar.value = Mathf.Clamp(scrollbar.value, 0, upperScrollLimit);
       
            // while holding scrollbar handle, even if you clamp 'scrollbar.value', the Scrollbar will still continue to move
            // if you hold the mouse button down because it handles Drag events when the mouse is over the scrollbar
            // this will stop that from happening
            scrollbar.interactable = false;
        }
        */
    }

    public void setNorm1()
    {
        scrollRect.verticalNormalizedPosition = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log("Panel rectTransform :" + parentObject.GetComponent<RectTransform>().anchoredPosition);
       
    }

    // Update is called once per frame
   /* void Update()
    {
        
    }
    */
    void LateUpdate(){
       // setNorm1();
       // LimitScroll();
       Debug.Log("Panel rectTransform :" + panel1.GetComponent<RectTransform>().anchoredPosition);
        //Debug.Log("The vertical scroll value is:  " +scrollRect.verticalNormalizedPosition );
    }
}
