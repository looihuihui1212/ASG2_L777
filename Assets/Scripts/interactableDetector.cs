using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class InteractableDetector : MonoBehaviour
{
    public TextMeshProUGUI interactableNameText;  // Reference to the text component
    public Transform socketTransform;             // Reference to the socket's transform
    public Vector3 textOffset = new Vector3(0, 0.2f, 0); // Offset to position the text above the socket

    private XRSocketInteractor socketInteractor;
    private Camera mainCamera;

    private void Awake()
    {
        // Get the XR Socket Interactor component attached to this GameObject
        socketInteractor = GetComponent<XRSocketInteractor>();
        mainCamera = Camera.main; // Get the main camera in the scene

        // Subscribe to the Select Enter event (when an object is placed in the socket)
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnObjectPlacedInSocket);
        }
    }

    private void OnObjectPlacedInSocket(SelectEnterEventArgs args)
    {
        // Check if the interactable object is valid
        if (args.interactableObject == null) return;

        // Get the interactable object
        var interactable = args.interactableObject.transform.gameObject;

        // Update the text and position it above the socket
        interactableNameText.text = "Correct Placement!";
        interactableNameText.transform.position = socketTransform.position + textOffset;
        interactableNameText.gameObject.SetActive(true);

        // Make the text face the camera
        FaceCamera();

        // Optional: Hide the text after a delay
        Invoke(nameof(HideText), 2f);
    }

    private void FaceCamera()
    {
        if (mainCamera != null)
        {
            // Calculate the direction to the camera
            Vector3 directionToCamera = mainCamera.transform.position - interactableNameText.transform.position;

            // Set the y-component to 0 to prevent vertical flipping
            directionToCamera.y = 0;

            // Get the rotation that looks at the camera
            Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);

            // Apply the rotation, but we also need to flip the text to prevent mirroring
            interactableNameText.transform.rotation = lookRotation * Quaternion.Euler(0, 180, 0); // Flip 180 degrees around Y-axis
        }
    }


    private void HideText()
    {
        interactableNameText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnObjectPlacedInSocket);
        }
    }
}
