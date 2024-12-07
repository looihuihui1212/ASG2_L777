using UnityEngine;
using TMPro;

public class BinScoreManager : MonoBehaviour
{
    public TextMeshProUGUI feedbackText; // Reference to the UI Text element for feedback
    public float feedbackDuration = 2f; // Duration to show the feedback message

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        string binName = gameObject.name;

        // Check if the litter matches the bin
        if ((binName == "PlasticBottleCollider" && tag == "PlasticBottle") ||
            (binName == "MetalCanCollider" && tag == "MetalCan") ||
            (binName == "PaperCollider" && tag == "Paper"))
        {
            // If the litter matches the bin, add the score and destroy the litter
            UnifiedScoreManager.Instance.AddBinScore(tag);
            Destroy(collision.gameObject);
        }
        else
        {
            // If the litter does not match the bin, show the "Incorrect bin!" message
            ShowFeedback("Incorrect bin!");
        }
    }

    // Method to display feedback
    private void ShowFeedback(string message)
    {
        feedbackText.text = message; // Set the feedback text
        Invoke("HideFeedback", feedbackDuration); // Hide the feedback after a set duration
    }

    // Method to hide the feedback message
    private void HideFeedback()
    {
        feedbackText.text = ""; // Clear the feedback message
    }
}
