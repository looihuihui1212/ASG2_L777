using UnityEngine;

public class VRInstructionPanel : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the VR camera or player's head
    public float distanceFromCamera = 2f; // Distance of the panel from the camera
    public float heightOffset = 0.5f; // Adjust height if needed

    void Update()
    {
        // Position the panel in front of the camera
        Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        targetPosition.y += heightOffset;
        transform.position = targetPosition;

        // Rotate the panel to face the camera
        transform.LookAt(cameraTransform);
        transform.Rotate(0, 180, 0); // Flip the panel to face the user directly
    }

    public void ShowPanel(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

}
