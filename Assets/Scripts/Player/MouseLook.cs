using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // this script goes in the camera that's attached to the player
    //-------------------------------------------------------------
    
    // sets mouse sensitivity
    [SerializeField] private float mouseSensitivity = 100f;
    // sets value for x axis rotation
    [SerializeField] private float xRotation = 0f;
    // gets the transform of the player
    [SerializeField] private Transform playerBody;
    
    void Start()
    {
        // locks the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        // linking mouseX and mouseY to the mouse sensitivity
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // mouse y makes the camera rotate in the x axis
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // mouse x makes the camera rotate in the y axis
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
