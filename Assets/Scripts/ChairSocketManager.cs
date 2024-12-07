using UnityEngine;

public class ChairSocketManager : MonoBehaviour
{
    [SerializeField] private string requiredChairTag; // Tag for the correct chair
    private bool isChairCorrect = false;
    private bool isChairLocked = false; // Flag to track if the chair is locked in

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the correct tag
        if (other.CompareTag(requiredChairTag) && !isChairLocked) // Prevent score update if locked
        {
            isChairCorrect = true;
            SnapChairToSocket(other.gameObject);

            // Update the score once the chair is placed correctly
            UnifiedScoreManager.Instance.UpdateChairScore(requiredChairTag);
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

        // Mark the chair as locked in so that score doesn't update again
        isChairLocked = true;

        Debug.Log($"{chair.name} successfully placed in {gameObject.name}");
    }

    public void UnlockChair()
    {
        // Reset the locked flag when you release the chair (e.g., when Shift/Space is pressed)
        isChairLocked = false;

        // Optionally, reset the chair's Rigidbody to allow movement again
        // Rigidbody rb = chair.GetComponent<Rigidbody>();
        // if (rb != null)
        // {
        //     rb.isKinematic = false;
        // }
    }
}
