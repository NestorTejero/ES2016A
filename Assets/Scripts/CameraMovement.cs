using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    public float moveSpeed = 50f;       // movement through keyboard arrows
    public float scrollSpeed = 250f;    // movement through mouse wheel

    // Camera boundaries. User cannot place camera outside the space defined by this coordinates
    public Vector3 minimumBoundary = new Vector3(10f, 20f, 10f);
    public Vector3 maximumBoundary = new Vector3(190f, 80f, 120f);
    // X,Z boundaries should be related to the size of the terrain. Altitude optimal boundary (Y) has to be tested.

    public string targetTag = "home";
    public bool zoomEnabled = true;   // zoom (scroll) enabled

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag(targetTag);
        if (go != null)
            centerCamera(go);
    }

    // Called once per frame
    void Update()
    {
        // Check for keyboard arrows
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveCamera(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveCamera(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveCamera(new Vector3(0, 0, -moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveCamera(new Vector3(0, 0, moveSpeed * Time.deltaTime));
        }
        if (!zoomEnabled)    // end if zoom is not enabled
            return;

        // Zoom: check for mouse wheel 
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            // Inversed Y axis
            MoveCamera(new Vector3(0, -scroll * scrollSpeed * Time.deltaTime, 0));
        }
    }

    // Manages camera movement on the XYZ world axis.
    private void MoveCamera(Vector3 movement)
    {
        Vector3 position = gameObject.transform.position; // current position
        Vector3 targetPosition = position + movement;     // position after movement

        // Place target postion inside boundaries.
        targetPosition = new Vector3(Mathf.Min(maximumBoundary.x, targetPosition.x),
            Mathf.Min(maximumBoundary.y, targetPosition.y), Mathf.Min(maximumBoundary.z, targetPosition.z));
        targetPosition = new Vector3(Mathf.Max(minimumBoundary.x, targetPosition.x),
            Mathf.Max(minimumBoundary.y, targetPosition.y), Mathf.Max(minimumBoundary.z, targetPosition.z));

        // Using world coordinates allows the camera to maintain its original orientation.
        transform.Translate(targetPosition - position, Space.World);
    }

    // Places camera so it points to given game object.
    public void centerCamera(GameObject go)
    {
        centerCamera(go.transform.position);
    }

    // Places camera so it points to given position.
    public void centerCamera(Vector3 target)
    {
        // Camera rotation in the X-axis.
        float angle = transform.rotation.eulerAngles.x;
        // Camera height respecting imposed boundaries.
        float height = Mathf.Max(minimumBoundary.y, Mathf.Min(maximumBoundary.y, transform.position.y));

        /*
         * Center camera:
         *      - set camera in the same X position as target
         *      - maintain the same y posityion
         *      - set z position as target's z minus height * tan(angle)
         * */
        transform.position = new Vector3(target.x, height,
            target.z - height * Mathf.Tan(angle * Mathf.Deg2Rad));
    }
}
