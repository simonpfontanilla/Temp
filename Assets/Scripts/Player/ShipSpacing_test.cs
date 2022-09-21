using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpacing_test : MonoBehaviour
{
    private Transform _obj;

    private Transform _ship;

    private List<GameObject> shipList;

    private float theta;

    private float phi;

    private float radius;

    private int prevCount;
    [SerializeField]
    private bool sphere;
    [SerializeField]
    private float sphereSize = 4.0f;

    void Start()
    {
        _obj = transform;
        _ship = GameObject.Find("Carrier").transform;
        shipList = GameObject.Find("Carrier").GetComponent<Player>().Children;
        prevCount = shipList.Count;
        radius  = 3.0f;
    }

    void Update()
    {
        if(shipList.Count == prevCount) return;
        int count = shipList.Count;
        //Debug.Log("count: " + count);
        int index = GetIndex(count);
        //check if ring is completed
        int multiple = (shipList.Count/12);
        //Debug.Log("multiple: " + multiple);
        if(prevCount < 12*multiple){
            int desiredIndex = 12*(prevCount/12);
            index = FixIndex(index, desiredIndex);
            // Debug.Log("index: " + index);
            count = 12*multiple;
        }
        MoveShips(index, count);
        prevCount = shipList.Count;
    }

    void MoveShips(int index, int count){
        // Debug.Log("new loop" + " index: " + index + " count: " + count + " prevCount: " + prevCount);
        for(int i = index; i < count; i++){
            if(sphere){ //sphere
                Vector3 newPos = Random.onUnitSphere * sphereSize;
                shipList[i].GetComponent<Follow>().FollowOffset = new Vector3(_ship.position.x + newPos.x, _ship.position.y + newPos.y, _ship.position.z + newPos.z);
                shipList[i].transform.position = new Vector3(_ship.position.x + newPos.x, _ship.position.y + newPos.y, _ship.position.z + newPos.z);
                // shipList[i].GetComponent<Follow>().Axis = Random.onUnitSphere * sphereSize;
                // shipList[i].GetComponent<Follow>().Axis = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) * _ship.position;
                shipList[i].GetComponent<Follow>().Axis = _ship.position;
                shipList[i].GetComponent<Follow>().sphere = true;
            }else{ //ring
                int temp = count - index;
                if(temp > 12) temp = 12;
                float theta = (2 * Mathf.PI / temp) * i;
                int ring = shipList[i].GetComponent<Follow>().Ring;
                float x = Mathf.Cos(theta) + (_ship.position.x/2);
                float y = Mathf.Sin(theta) + (_ship.position.y/2);
                shipList[i].GetComponent<Follow>().FollowOffset = new Vector3(radius*x,radius*y,_ship.position.z + ring);
                shipList[i].transform.position = new Vector3(radius*x,radius*y,_ship.position.z + ring);
                shipList[i].transform.Rotate(new Vector3(0, 0, 90), Space.Self);
            }

            if((i+1)%12 == 0){
                // Debug.Log("new ring");
                prevCount = i;
                int newIndex = 12*((prevCount/12)+1);
                // Debug.Log("newindex: " + newIndex);
                MoveShips(newIndex, shipList.Count);
                break;
            }
        }
    }

    int GetIndex(int length){
        return shipList.Count <= 12 ? 0 : 12*(length/12);
    }

    int FixIndex(int index, int desiredIndex){
        while(index != desiredIndex){    
            index -= 12;
        }
        return index;
    }
}
