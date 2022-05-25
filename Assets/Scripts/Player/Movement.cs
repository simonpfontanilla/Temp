using UnityEngine;

// public class Movement : MonoBehaviour
// {
//     [SerializeField]
//     private float forceAmount = 250;
//     [SerializeField]
//     private float bounds = 5;
//     [SerializeField]
//     private GameObject player;

//     private Vector3 offset;

//     private Rigidbody selectedRigidbody;

//     private Camera targetCamera;

//     private Vector3 originalScreenTargetPosition;

//     private Vector3 originalRigidbodyPos;

//     private float selectionDistance;


//     void Start()
//     {
//         targetCamera = GetComponent<Camera>();
//         offset = transform.position - player.transform.position;
//     }

//     void Update()
//     {
//         if (!targetCamera)
//             return;

//         if (Input.GetMouseButtonDown(0))
//         {
//             //Check if we are hovering over Rigidbody, if so, select it
//             selectedRigidbody = GetRigidbodyFromMouseClick();
//         }
//         if (Input.GetMouseButtonUp(0) && selectedRigidbody)
//         {
//             //Release selected Rigidbody if there any
//             selectedRigidbody.velocity = new Vector3(0,0,0);
//             selectedRigidbody = null;
//         }
//     }

//     void LastUpdate(){
//         transform.position = player.transform.position + offset;
//     }

//     void FixedUpdate()
//     {
//         if (selectedRigidbody)
//         {
//             Vector3 mousePositionOffset = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition;
//             if (selectedRigidbody.transform.position.x > bounds){
//                 selectedRigidbody.transform.position = new Vector3(bounds, selectedRigidbody.transform.position.y, selectedRigidbody.transform.position.z);
//             }else if (selectedRigidbody.transform.position.x < -bounds){
//                 selectedRigidbody.transform.position = new Vector3(-bounds, selectedRigidbody.transform.position.y, selectedRigidbody.transform.position.z);
//             }
//             selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position + transform.forward) * forceAmount * Time.deltaTime;
//         }
//     }

//     Rigidbody GetRigidbodyFromMouseClick()
//     {
//         RaycastHit hitInfo = new RaycastHit();
//         Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
//         bool hit = Physics.Raycast(ray, out hitInfo);
//         if (hit)
//         {
//             if (hitInfo.collider.gameObject.GetComponent<Rigidbody>())
//             {
//                 selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
//                 originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
//                 originalRigidbodyPos = hitInfo.collider.transform.position;
//                 return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
//             }
//         }

//         return null;
//     }
// }


public class Movement : MonoBehaviour
{
    [SerializeField]
    // private Transform _player;

    private Vector3 _position;

    private Rigidbody selectedRigidBody;

    private int bounds;

    private Vector3 lastPos;

    private Vector3 delta;

    [SerializeField] private Vector3 centerPosition, bigGateEndingPos, toCenterPos;
    [SerializeField] private bool moveToCenter = false, autoMove = false;

    [SerializeField] public float speed = 10, autoMoveMulti = 1;
    [SerializeField] public float strafeSpeed = 2f;    //some sideways movement amount

    void Start() {
        // _player = GameObject.Find("Carrier").transform;
        // _position = _player.position;
        bounds = 7;

        float z = GameObject.Find("MapLoader").GetComponent<MapLoader>().toCenterZPos;
        toCenterPos = new Vector3(0, 4, z);
    }

    void Update()
    {
        if (moveToCenter)
        {
            if (Vector3.Distance(transform.position, centerPosition) > 0.1)
            {
                transform.position = Vector3.Lerp(transform.position, centerPosition, 1.5f * Time.deltaTime);
                // camera position
                transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, new Vector3(0.2f,0f,0.14f), Time.deltaTime);
            }
            else
            {
                moveToCenter = false;
                autoMove = true;
            }
        }
        else if (autoMove)
        {
            if ((int)Vector3.Distance(transform.position, bigGateEndingPos) > 0)
                transform.position = Vector3.Lerp(transform.position, bigGateEndingPos, Time.deltaTime);
        }
        else
        {
            if (transform.position.x > bounds)
            {
                transform.position += new Vector3(-0.01f,0,speed * Time.deltaTime);
            }
            else if (transform.position.x < -bounds)
            {
                transform.position += new Vector3(0.01f,0,speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Input.GetAxis("Horizontal") * strafeSpeed * Time.deltaTime, 0f, speed * Time.deltaTime, Space.World);
            }

            // if (Input.touchCount > 0)
            // {
            //     Touch touch = Input.GetTouch(0);
            //     MoveShip(touch);
            // }else if(Input.GetMouseButtonDown(0)){
            //     lastPos = Input.mousePosition;
            // }else if(Input.GetMouseButton(0)){
            //     delta = Input.mousePosition - lastPos;
            //     MoveShipMouse(delta);
            //     lastPos = Input.mousePosition;
            // }
        }
    }

    void MoveShip(Touch touch){
        if(transform.position.x > bounds){
            transform.position += new Vector3(-0.01f,0,0.1f);
        }else if(transform.position.x < -bounds){
            transform.position += new Vector3(0.01f,0,0.1f);
        }else{
            transform.position += new Vector3(touch.deltaPosition.x/100,0,0.1f);
        }
    }

    void MoveShipMouse(Vector3 delta){
        if(transform.position.x > bounds){
            transform.position += new Vector3(-0.01f,0,0.1f);
        }else if(transform.position.x < -bounds){
            transform.position += new Vector3(0.01f,0,0.1f);
        }else{
            transform.position += new Vector3(delta.x/100,0, speed * Time.deltaTime);
        }
    }

    public void moveCarrierToCenter()
    {
        float z = GameObject.Find("MapLoader").GetComponent<MapLoader>().centerToPosition;

        centerPosition = new Vector3(0, 4, z);

        z = GameObject.Find("MapLoader").GetComponent<MapLoader>().bigGateEndingZPos;

        bigGateEndingPos = new Vector3(0, 4, z);

        moveToCenter = true;
    }
}