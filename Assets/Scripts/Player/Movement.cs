using UnityEngine;

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
        transform.position = new Vector3(0f,4.07819986f,-15f);
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

    public void detachCamera()
    {
        transform.GetChild(0).parent = null;
    }
}