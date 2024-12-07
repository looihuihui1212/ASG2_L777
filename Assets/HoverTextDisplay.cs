using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HoverTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI hoverText;  // Reference to the TextMeshPro UI text component
    public string message = "Pick up litter";  // Message to display when hovering

    private XRBaseInteractable interactableObject;  // Reference to the interactable object

    private void OnEnable()
    {
        // Ensure text starts hidden when the script is enabled
        if (hoverText != null)
        {
            hoverText.gameObject.SetActive(false); // Text is initially hidden
        }

        // Get the XR interactable component
        interactableObject = GetComponent<XRBaseInteractable>();

        // Subscribe to the new hover events
        if (interactableObject != null)
        {
            interactableObject.hoverEntered.AddListener(OnHoverEntered);
            interactableObject.hoverExited.AddListener(OnHoverExited);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the new hover events when the script is disabled
        if (interactableObject != null)
        {
            interactableObject.hoverEntered.RemoveListener(OnHoverEntered);
            interactableObject.hoverExited.RemoveListener(OnHoverExited);
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        // Show the text when the controller hovers over the object
        hoverText.text = message;
        hoverText.gameObject.SetActive(true); // Make the text visible
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        // Hide the text when the controller stops hovering over the object
        hoverText.gameObject.SetActive(false); // Hide the text
    }
}
