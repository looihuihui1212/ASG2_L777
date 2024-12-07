using UnityEngine;

public class ChairSocketManager : MonoBehaviour
{
    [SerializeField] private string requiredChairTag; // Tag for the correct chair
    private bool isChairCorrect = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the correct tag
        if (other.CompareTag(requiredChairTag))
        {
            isChairCorrect = true;
            SnapChairToSocket(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset if the object leaves the socket
        if (other.CompareTag(requiredChairTag))
        {
            isChairCorrect = false;
        }
    }

    private void SnapChairToSocket(GameObject chair)
    {
        // Snap the chair to the socket's position and rotation
        chair.transform.position = transform.position;
        chair.transform.rotation = transform.rotation;

        // Optional: Disable chair movement if it's VR interactable
        Rigidbody rb = chair.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Prevent further movement
        }

        Debug.Log($"{chair.name} successfully placed in {gameObject.name}");
    }
}
