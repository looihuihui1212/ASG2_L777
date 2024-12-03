using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class InteractableHoverDetector : MonoBehaviour
{
    public TextMeshProUGUI interactablePromptText; // Reference to the text UI
    public Vector3 textOffset = new Vector3(0, 0.5f, 0); // Offset for the text position
    private Camera mainCamera;
    private bool isPlacedInSocket = false; // Track if the object is placed in the socket

    private void Start()
    {
        if (interactablePromptText != null)
        {
            Debug.Log("TextMeshProUGUI is assigned correctly.");
        }
        else
        {
            Debug.LogError("TextMeshProUGUI is not assigned! Please assign it in the Inspector.");
        }
    }

    private void Awake()
    {
        mainCamera = Camera.main; // Assign the main camera
    }

    private void Update()
    {
        if (interactablePromptText != null && interactablePromptText.gameObject.activeSelf)
        {
            FaceCamera(); // Continuously make the text face the camera
        }
    }

    private void OnEnable()
    {
        // Register hover events for XRBaseInteractable
        var interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnHoverEntered);
            interactable.hoverExited.AddListener(OnHoverExited);
        }

        // Register socket placement event
        var socketInteractor = GetComponent<XRSocketInteractor>();
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnPlacedInSocket);
        }
    }

    private void OnDisable()
    {
        // Unregister hover events
        var interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.RemoveListener(OnHoverEntered);
            interactable.hoverExited.RemoveListener(OnHoverExited);
        }

        // Unregister socket placement event
        var socketInteractor = GetComponent<XRSocketInteractor>();
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnPlacedInSocket);
        }
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (isPlacedInSocket) return; // Do not show text if placed in socket

        Debug.Log("Hover Entered"); // Confirms the method is triggered

        if (interactablePromptText != null)
        {
            interactablePromptText.text = "Press the Grip Button to Grab";
            interactablePromptText.gameObject.SetActive(true);
            Debug.Log("Text Activated"); // Confirms text activation logic is reached
        }
        else
        {
            Debug.LogError("InteractablePromptText is null! Please assign it in the Inspector.");
        }
    }

    public void OnHoverExited(HoverExitEventArgs args)
    {
        if (interactablePromptText == null || isPlacedInSocket) return;

        // Hide the text
        interactablePromptText.gameObject.SetActive(false);
    }

    private void OnPlacedInSocket(SelectEnterEventArgs args)
    {
        Debug.Log("Object placed in socket"); // Confirm socket placement
        isPlacedInSocket = true;

        // Hide the text
        if (interactablePromptText != null)
        {
            interactablePromptText.gameObject.SetActive(false);
        }
    }

    private void FaceCamera()
    {
        if (mainCamera == null || interactablePromptText == null) return;

        Vector3 directionToCamera = mainCamera.transform.position - interactablePromptText.transform.position;
        directionToCamera.y = 0; // Prevent vertical flipping
        interactablePromptText.transform.rotation = Quaternion.LookRotation(-directionToCamera);
    }
}
