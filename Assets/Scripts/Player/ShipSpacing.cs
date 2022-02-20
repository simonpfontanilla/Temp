using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpacing : MonoBehaviour
{
    public int numObjects;
    public float x;
    public float y;
    private Transform _obj;
    private Transform _ship;
    private Vector3 position;
    private int newCount;
    // Start is called before the first frame update
    void Start()
    {
        _obj = GameObject.Find("orbit").transform;
        _ship = GameObject.Find("Carrier").transform;
        newCount = GameObject.Find("Carrier").GetComponent<PlayerChild>().children.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(newCount ==_obj.childCount) return;

        numObjects = _obj.childCount;
        newCount = numObjects;
        int i = 0;
        Vector3 newPos = new Vector3(0,0,0);i++;

        int index = GetIndex(newCount);
        for(i = index; i < newCount; i++){
            float theta = (2 * Mathf.PI / GetCount(numObjects)) * i;
            x = Mathf.Cos(theta) + (_ship.position.x/2);
            y = Mathf.Sin(theta) + (_ship.position.y/2);
            int ring = _obj.GetChild(i).GetComponent<Follow>().ring;
            _obj.GetChild(i).GetComponent<Follow>()._followOffset = new Vector3(2*x,2*y,_ship.position.z + ring);
            _obj.GetChild(i).transform.position = new Vector3(2*x,2*y,_ship.position.z + ring);
        }
    }

    int GetIndex(int length){
        int i = length/12;
        if(length%12 == 0){
            return 12*(i-1);
        }else{
            return 12*i;
        }
    }

    int GetCount(int numObjects){
        int temp = numObjects%12;
        if(temp == 0){
            return 12;
        }else{
            return numObjects%12;
        }
    }
}
