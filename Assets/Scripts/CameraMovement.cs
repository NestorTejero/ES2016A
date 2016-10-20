using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float moveSpeed = 250f;      // movement through keyboard arrows
    public float scrollSpeed = 250f;    // movement through mouse wheel

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

        // Check for mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            // Inversed Y axis
            MoveCamera(new Vector3(0, -scroll * scrollSpeed * 100 * Time.deltaTime, 0));
        }
    }

    // Manages camera movement on the XYZ world axis.
    private void MoveCamera(Vector3 position)
    {
        // Using world coordinates allows the camera to maintain its original orientation.
        transform.Translate(position, Space.World);
    }
}
