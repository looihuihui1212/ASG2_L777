using UnityEngine;

public class VRInstructionPanel : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the VR camera or player's head
    public float distanceFromCamera = 2f; // Distance of the panel from the camera
    public float heightOffset = -0.5f; // Adjust height if needed
    public float widthOffset = -0.5f; // Adjust width for left/right positioning

    void Update()
    {
        // Calculate the target position in front of the camera
        Vector3 forwardPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;

        // Apply height and width offsets
        Vector3 offsetPosition = forwardPosition +
                                 (cameraTransform.right * widthOffset) +
                                 (Vector3.up * heightOffset);

        // Set the panel's position
        transform.position = offsetPosition;

        // Rotate the panel to face the camera
        transform.LookAt(cameraTransform);
        transform.Rotate(0, 180, 0); // Flip the panel to face the user directly
    }

    public void ShowPanel(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}
