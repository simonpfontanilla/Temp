using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearChildren : MonoBehaviour
{
    [SerializeField] private Transform orbit;
    void Start()
    {

    }
    // deletes ships at the beginning of each level
    public void Clear(){
        foreach(Transform child in orbit){
            Destroy(child.gameObject);
        }
    }
}
