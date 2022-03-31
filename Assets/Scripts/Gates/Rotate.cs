using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private RectTransform _text;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "Currency") return;
        _text = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Finish"){
            transform.Rotate(Vector3.back * 20f * Time.deltaTime);
        }else{
            transform.Rotate(Vector3.forward * 20f * Time.deltaTime);
        }
        if(gameObject.tag == "Currency") return;
        _text.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -transform.rotation.z);
    }
}
