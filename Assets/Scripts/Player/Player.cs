using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _children;
    [SerializeField]
    private int _currency;

    public void Start()
    {

    }

    public void Update()
    {
        if (PlayerPrefs.GetInt("currency", 0) != _currency && PlayerPrefs.GetInt("currency", 0) != 0)
            _currency = PlayerPrefs.GetInt("currency", 0);
    }

    public void OnCollisionEnter(Collision other){
        // if (other.gameObject.tag == "Player"){
        //     Rigidbody rb = GetComponent<Rigidbody>();
        //     rb.velocity = new Vector3(0,0,0);
        // }
    }

    public int Currency
    {
        get { return _currency; }
        set { _currency = value; }
    }

    public List<GameObject> Children
    {
        get { return _children; }
        set { _children = value; }
    }
}
