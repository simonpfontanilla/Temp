using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropdownMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] buttonPrefab;


    public GameObject button;
    public Animator settingsAnim;

    [SerializeField] Transform settingsPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void swipeOut()
    {

        Debug.Log("Game Started, swipe initiated");
        settingsAnim.SetBool("SlideOut", true);
    }

    public void openDropdown()
    {
        Debug.Log("The length of array is " + buttonPrefab.Length);
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

        buttonPrefab[0].SetActive(true);
        buttonPrefab[1].SetActive(true);
        buttonPrefab[2].SetActive(true);
        // yield return new WaitForSeconds(5);
        settingsAnim.SetTrigger("OpenSettings");
        Debug.Log("Animation start!");
        settingsAnim.SetBool("Open", true);

        yield return null;
        // yield return new WaitForSeconds(5);

    }

    public void collapseDropdown()
    {
        // StartCoroutine(collapse());
        settingsAnim.SetTrigger("CloseSettings");
        settingsAnim.SetBool("Open", false);

        if (settingsAnim.GetBool("Open") == false && settingsAnim.GetCurrentAnimatorStateInfo(0).IsName("IdleDrop"))
        {
            Debug.Log("IdleDrop State reached");
            buttonPrefab[0].SetActive(false);
            buttonPrefab[1].SetActive(false);
            buttonPrefab[2].SetActive(false);
        }

    }

    public IEnumerator collapse()
    {
        foreach (GameObject button in buttonPrefab)
        {
            // Destroy(button, 0.5f);
            button.SetActive(false);
            Debug.Log("Button has been toggled inactive");
            yield return new WaitForSeconds(5);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
