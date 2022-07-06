using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _transform;
    [SerializeField]
    private float fc;
    [SerializeField]
    private float phi;
    [SerializeField]
    private float amplitude;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        fc = 0.5f;
        amplitude = 2.0f;

        //alternate
        // if((_transform.position.z-200)/7%2 == 0){
        //     phi = 0.0f;
        // }else{
        //     phi = Mathf.PI;
        // }

        //sinusoidal
        //phi = ((_transform.position.z-200)/7)*5.0f;

        // alternate in row
        if(Mathf.Abs(_transform.position.x)%2 == 0){
            phi = 0.0f;
        }else{
            phi = Mathf.PI;
        }

        // sinusoidal row
        // if(_transform.position.x == -2) phi = 0;
        // else if(_transform.position.x == -1) phi = (Mathf.PI/5);
        // else if(_transform.position.x == 0) phi = (Mathf.PI/5)*2;
        // else if(_transform.position.x == 1) phi = (Mathf.PI/5)*3;
        // else if(_transform.position.x == 2) phi = (Mathf.PI/5)*4;

        // sinusoidal row (alternate)
        // if((_transform.position.z-200)/7%2 == 0){
        //     if(_transform.position.x == -2) phi = 0;
        //     else if(_transform.position.x == -1) phi = (Mathf.PI/5);
        //     else if(_transform.position.x == 0) phi = (Mathf.PI/5)*2;
        //     else if(_transform.position.x == 1) phi = (Mathf.PI/5)*3;
        //     else if(_transform.position.x == 2) phi = (Mathf.PI/5)*4;
        // }else{
        //     if(_transform.position.x == -2) phi = (Mathf.PI/5)*4;
        //     else if(_transform.position.x == -1) phi = (Mathf.PI/5)*3;
        //     else if(_transform.position.x == 0) phi = (Mathf.PI/5)*2;
        //     else if(_transform.position.x == 1) phi = (Mathf.PI/5);
        //     else if(_transform.position.x == 2) phi = 0;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "FighterComplete_Shop_Trail"){
            float y = 0.5f*Mathf.Cos((2*Mathf.PI*fc*Time.fixedTime)+phi) + 1876;
            _transform.position = new Vector3(_transform.position.x, y , _transform.position.z);
        }else{
            float y = amplitude*Mathf.Cos((2*Mathf.PI*fc*Time.fixedTime)+phi)+4;
            _transform.position = new Vector3(_transform.position.x, y, _transform.position.z);
        }
    }
}
