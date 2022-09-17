using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DropdownMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] buttonPrefab;


    public GameObject button;
    public Animator settingsAnim;
    public bool dropdownState;

    [SerializeField] Transform settingsPanel;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void swipeOut()
    {

        // Debug.log("Game Started, swipe initiated");
        settingsAnim.SetBool("SlideOut", true);
    }

    //Will be an alternator that will apply to one button, check to see the state then decide what animation to perform
    public void toggleDropdown()
    {
        // Debug.log("The state of the settings toggle: "+dropdownState);
        if(dropdownState == false)
        {
            openDropdown();
        }

        else
        {
            collapseDropdown();

        }
        
    }

    public void openDropdown()
    {
        // Debug.log("The length of array is " + buttonPrefab.Length);
        /*for(int i =0; i<buttonPrefab.Length;i++)
       {
         GameObject button =  (GameObject)Instantiate(buttonPrefab[i],Vector3.zero,Quaternion.identity);
         button.GetComponent<RectTransform>().SetParent(settingsPanel,false);
       } */
        // buttonPrefab[0].SetActive(true);
        //buttonPrefab[1].SetActive(true);
        //buttonPrefab[2].SetActive(true);
        //settingsAnim.SetTrigger("OpenSettings");
        //settingsAnim.SetBool("Open", true);
        StartCoroutine(open());

    }

    public IEnumerator open()
    {

        // buttonPrefab[0].SetActive(true);
        // buttonPrefab[1].SetActive(true);
        // buttonPrefab[2].SetActive(true);
        
        buttonPrefab[0].GetComponent<Image>().enabled = true;
        buttonPrefab[1].GetComponent<Image>().enabled = true;
        buttonPrefab[2].GetComponent<Image>().enabled = true;
        foreach (GameObject button in buttonPrefab)
        {
            // Destroy(button, 0.5f);
            button.SetActive(true);
            // Debug.log("Button has been toggled ACTIVE");
        

        }
        // yield return new WaitForSeconds(5);
        settingsAnim.SetTrigger("OpenSettings");
        // Debug.log("Animation start!");
        settingsAnim.SetBool("Open", true);
        yield return null;
        // yield return new WaitForSeconds(5);

    }

    public void collapseDropdown()
    {
        // StartCoroutine(collapse());
        settingsAnim.SetTrigger("CloseSettings");
        settingsAnim.SetBool("Open", false);
        StartCoroutine(collapse());
        //buttonPrefab[0].GetComponent<Image>().enabled = false;
        //buttonPrefab[1].GetComponent<Image>().enabled = false;
        //buttonPrefab[2].GetComponent<Image>().enabled = false;
       // Debug.Log("The current animation state is "+settingsAnim.GetCurrentAnimatorStateInfo(0));
        if (settingsAnim.GetCurrentAnimatorStateInfo(0).IsName("IdleDrop"))
        {
            // Debug.log("IdleDrop State reached");
        buttonPrefab[0].GetComponent<Image>().enabled = false;
        buttonPrefab[1].GetComponent<Image>().enabled = false;
        buttonPrefab[2].GetComponent<Image>().enabled = false;
            // buttonPrefab[0].SetActive(false);
            // buttonPrefab[1].SetActive(false);
            // buttonPrefab[2].SetActive(false);
        }

        else
        {
            // Debug.log("IdleDrop has not been reached");
        }
    }

    public IEnumerator collapse()
    {
        foreach (GameObject button in buttonPrefab)
        {
            // Destroy(button, 0.5f);
            button.SetActive(false);
            // Debug.log("Button has been toggled inactive");
            yield return new WaitForSeconds(0.5f);

        }
    }

    // Update is called once per frame
    void Update()
    {
         dropdownState = settingsAnim.GetBool("Open");
    }
}
