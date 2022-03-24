using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpacing : MonoBehaviour
{
    private Transform _obj;

    private Transform _ship;

    private List<GameObject> shipList;

    private float theta;

    private float phi;

    private float radius;

    private int prevCount;

    void Start()
    {
        _obj = transform;
        _ship = GameObject.Find("Carrier").transform;
        shipList = GameObject.Find("Carrier").GetComponent<Player>().Children;
        prevCount = shipList.Count;
        radius  = 2.0f;
    }

    void Update()
    {
        if(shipList.Count == prevCount) return;

        int index = GetIndex(shipList.Count);
        int count = shipList.Count;
        //check if ring is completed
        int multiple = (shipList.Count/12);
        if(prevCount < 12*multiple){
            if(prevCount == 0){
                index = 0;
            }else{
                index -= 12;
            }
            count = 12*multiple;
        }
        MoveShips(index, count, 0);
        prevCount = shipList.Count;
    }

    void MoveShips(int index, int count, int offset){
        Debug.Log("new loop" + " index: " + index + " count: " + count);
        for(int i = index; i < count; i++){
            Debug.Log(i);
            float theta = (2 * Mathf.PI / (count-index)) * i;
            int ring = shipList[i].GetComponent<Follow>().Ring;

            float x = Mathf.Cos(theta) + (_ship.position.x/2);
            float y = Mathf.Sin(theta) + (_ship.position.y/2);
            shipList[i].GetComponent<Follow>().FollowOffset = new Vector3(radius*x,radius*y,_ship.position.z + ring);
            shipList[i].transform.position = new Vector3(radius*x,radius*y,_ship.position.z + ring);

            if((i+1)%12 == 0){
                Debug.Log("new ring");
                int multiple = 12*(shipList.Count/12);
                MoveShips(multiple, shipList.Count, multiple);
                break;
            }
        }
    }

    // void MoveShips(int index, int shipList.Count, float radius, int count){
    //     Debug.Log("new loop");
    //     for(int i = index; i < shipList.Count; i++){
    //         float theta = (2 * Mathf.PI / count) * i;
    //         int ring = _obj.GetChild(i).GetComponent<Follow>().Ring;

    //         //works for spawn number that is divisible by 12
    //         //fix when number of ships spawned flows into the next ring
    //         Debug.Log(i+1);
    //         if((i+1)%12 == 0){
    //             Debug.Log("new ring");
    //             int c = GetCount(count - 11);
    //             MoveShips(i, (shipList.Count - i + 1), radius, c);
    //         }

    //         // cone shape
    //         // float test = radius/6;
    //         // int k = ring/-2;
    //         // float x = Mathf.Cos(theta) + (_ship.position.x/(radius-(test*k)));
    //         // float y = Mathf.Sin(theta) + (_ship.position.y/(radius-(test*k)));
    //         // _obj.GetChild(i).GetComponent<Follow>()._followOffset = new Vector3((radius-(test*k))*x,(radius-(test*k))*y,_ship.position.z + ring);
    //         // _obj.GetChild(i).transform.position = new Vector3((radius-(test*k))*x,(radius-(test*k))*y,_ship.position.z + ring);

    //         // tunnel
    //         float x = Mathf.Cos(theta) + (_ship.position.x/2);
    //         float y = Mathf.Sin(theta) + (_ship.position.y/2);
    //         _obj.GetChild(i).GetComponent<Follow>().FollowOffset = new Vector3(radius*x,radius*y,_ship.position.z + ring);
    //         _obj.GetChild(i).transform.position = new Vector3(radius*x,radius*y,_ship.position.z + ring);

    //         // sphere
    //         // theta = (2 * Mathf.PI / GetCount(numObjects)) * i; //0 to 2pi
    //         // phi = Mathf.PI/2;
    //         // int temp = GetCount(numObjects)/12;
    //         // Debug.Log(temp);
    //         // phi = (Mathf.PI*(3+temp))/6;
    //         // float x = radius*Mathf.Cos(theta)*Mathf.Sin(phi)+_ship.position.x;
    //         // float y = radius*Mathf.Sin(theta)*Mathf.Sin(phi)+_ship.position.y;
    //         // float z = radius*Mathf.Cos(phi);
    //         // _obj.GetChild(i).GetComponent<Follow>().FollowOffset = new Vector3(x,y,z+_ship.position.z);
    //         // _obj.GetChild(i).transform.position = new Vector3(x,y,z+_ship.position.z);
    //         // Debug.Log("test");

    //     }
    // }

    int GetIndex(int length){
        return shipList.Count <= 12 ? 0 : 12*(length/12);
    }
}
