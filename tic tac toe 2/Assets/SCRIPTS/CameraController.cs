using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Target to look at
    public Vector3 offset; // Offset from the target object
    public float panSpeed = 1.0f; // Speed of the camera movement
    public float zoomSpeed = 10.0f; // Speed of zooming
    public float minZoomDistance = 5.0f; // Minimum distance for zoom
    public float maxZoomDistance = 20.0f; // Maximum distance for zoom

    private Vector3 initialOffset;
    private Camera myCamera;

    void Start()
    {
        initialOffset = transform.position - target.position;
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        // Pan the camera horizontally
        if (Input.GetMouseButton(0))
        {
            float horizontalInput = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            transform.Translate(new Vector3(horizontalInput, 0, 0), Space.World);
        }

        // Pan the camera vertically
        if (Input.GetMouseButton(1))
        {
            float verticalInput = Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0, verticalInput, 0), Space.World);
        }

        // Zoom in/out
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        Vector3 direction = transform.position - target.position;
        Vector3 newPosition = transform.position - direction * (scrollData * zoomSpeed * Time.deltaTime);
        if (Vector3.Distance(target.position, newPosition) > minZoomDistance && Vector3.Distance(target.position, newPosition) < maxZoomDistance)
        {
            transform.position = newPosition;
        }
    }
}
