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
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        fc = 0.5f;
        //alternate
        // if((_transform.position.z-200)/7%2 == 0){
        //     phi = 0.0f;
        // }else{
        //     phi = 180.0f;
        // }
        //sinusoidal
        phi = ((_transform.position.z-200)/7)*5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float y = 2*Mathf.Cos((2*Mathf.PI*fc*Time.fixedTime)+phi)+4;
        _transform.position = new Vector3(_transform.position.x, y, _transform.position.z);
    }
}
