using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "FighterComplete_Shop") transform.Rotate(Vector3.back * 20f * Time.deltaTime);
        else if(gameObject.name == "Carrier_Shop") transform.Rotate(Vector3.forward * 20f * Time.deltaTime);
    }
}
