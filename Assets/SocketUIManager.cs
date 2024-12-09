using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketUIManager : MonoBehaviour
{
    public XRSocketInteractor socket; // Reference to the Socket Interactor
    public TextMeshProUGUI objectNameText; // Reference to the Text field in the UI

    void Start()
    {
        if (socket == null)
            socket = GetComponent<XRSocketInteractor>();

        // Subscribe to socket events
        socket.selectEntered.AddListener(OnObjectAttached);
        socket.selectExited.AddListener(OnObjectDetached);
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        socket.selectEntered.RemoveListener(OnObjectAttached);
        socket.selectExited.RemoveListener(OnObjectDetached);
    }

    private void OnObjectAttached(SelectEnterEventArgs args)
    {
        // Get the attached object's name and display it on the UI
        string attachedObjectName = args.interactableObject.transform.name;
        objectNameText.text = $"Attached: {attachedObjectName}";
    }

    private void OnObjectDetached(SelectExitEventArgs args)
    {
        // Clear the text when the object is detached
        objectNameText.text = string.Empty;
    }
}
