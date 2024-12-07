using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ScoreOnGrabWithGoal : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign the TextMeshPro text in Inspector
    public int goal = 1; // The total number of laptops to remove
    private static int currentScore = 0; // Tracks how many laptops have been removed
    public GameObject doorGameObject; // Assign the door GameObject in Inspector
    public float detectionRadius = 1.0f; // Radius for detecting if the laptop is near the door

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private bool isThrown = false; // Track whether the laptop has been thrown

    void Start()
    {
        // Get the XRGrabInteractable and Rigidbody components
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        if (grabInteractable != null)
        {
            // Subscribe to the SelectExited event to detect when the object is thrown
            grabInteractable.selectExited.AddListener(OnThrow);
        }

        // Update the initial score display
        UpdateScoreUI();
    }

    private void OnThrow(SelectExitEventArgs args)
    {
        isThrown = true; // Mark the laptop as thrown
    }

    void Update()
    {
        if (isThrown && doorGameObject != null)
        {
            // Check if the laptop is near the door
            float distanceToDoor = Vector3.Distance(transform.position, doorGameObject.transform.position);
            if (distanceToDoor <= detectionRadius)
            {
                // Increment the score
                currentScore++;

                // Update the score on the UI
                UpdateScoreUI();

                // Destroy the laptop game object
                Destroy(gameObject);

                // Update the unified score manager
                UnifiedScoreManager.Instance?.UpdateLaptopScore();

                isThrown = false; // Prevent further checks
            }
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            // Update the UI with the format "Remove laptops: x/goal done"
            scoreText.text = $"Remove laptops: {currentScore}/{goal} done";
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnThrow);
        }
    }
}
