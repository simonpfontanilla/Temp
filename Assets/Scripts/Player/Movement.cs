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
    private Transform _player;

    private Vector3 _position;

    private Rigidbody selectedRigidBody;

    private int bounds;

    private Vector3 lastPos;

    private Vector3 delta;

    [SerializeField] private Vector3 centerPosition;
    [SerializeField] private bool moveToCenter = false;

    [SerializeField] public float speed = 10;

    void Start() {
        _player = GameObject.Find("Carrier").transform;
        _position = _player.position;
        bounds = 7;
    }

    void Update()
    {
        if (moveToCenter)
        {
            if ((int)Vector3.Distance(transform.parent.transform.position, centerPosition) > 0)
                transform.parent.transform.position = Vector3.Lerp(transform.parent.transform.position, centerPosition, Time.deltaTime);
            else
                moveToCenter = false;

                // Vector3(0.181989998,0,0.137689993)
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                MoveShip(touch);
            }else if(Input.GetMouseButtonDown(0)){
                lastPos = Input.mousePosition;
            }else if(Input.GetMouseButton(0)){
                delta = Input.mousePosition - lastPos;
                MoveShipMouse(delta);
                lastPos = Input.mousePosition;
            }
        }
    }

    void MoveShip(Touch touch){
        if(_player.position.x > bounds){
            _player.position += new Vector3(-0.01f,0,0.1f);
        }else if(_player.position.x < -bounds){
            _player.position += new Vector3(0.01f,0,0.1f);
        }else{
            _player.position += new Vector3(touch.deltaPosition.x/100,0,0.1f);
        }
    }

    void MoveShipMouse(Vector3 delta){
        if(_player.position.x > bounds){
            _player.position += new Vector3(-0.01f,0,0.1f);
        }else if(_player.position.x < -bounds){
            _player.position += new Vector3(0.01f,0,0.1f);
        }else{
            _player.position += new Vector3(delta.x/100,0,speed * Time.deltaTime);
        }
    }

    public void moveCarrierToCenter()
    {
        float z = GameObject.Find("MapLoader").GetComponent<MapLoader>().centerToPosition;

        centerPosition = new Vector3(0, 4, z);

        moveToCenter = true;
    }
}