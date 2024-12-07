using UnityEngine;

public class LaptopInteraction : MonoBehaviour
{
    public Animator studentAnimator; // Assign the student's Animator in the Inspector
    public Transform cameraTransform; // Assign the camera's Transform in the Inspector
    private bool isOnTable = true;

    void OnCollisionExit(Collision collision)
    {
        // Check if the laptop has stopped touching the table
        if (collision.gameObject.CompareTag("Table"))
        {
            isOnTable = false;
            TriggerAngryAnimation();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the laptop is placed back on the table
        if (collision.gameObject.CompareTag("Table"))
        {
            isOnTable = true;
            ResetAnimation(); // Optional, reset to a default animation if needed
        }
    }

    private void TriggerAngryAnimation()
    {
        if (studentAnimator != null)
        {
            studentAnimator.SetTrigger("Angry"); // Use a trigger to switch to the angry animation
        }
    }

    private void ResetAnimation()
    {
        if (studentAnimator != null)
        {
            studentAnimator.ResetTrigger("Angry");
        }
    }

    void Update()
    {
        if (!isOnTable && cameraTransform != null)
        {
            FollowCamera(); // Continuously follow the camera when angry
        }
    }

    private void FollowCamera()
    {
        // Make the student face the camera
        Vector3 direction = cameraTransform.position - studentAnimator.transform.position;
        direction.y = 0; // Ensure they stay upright (don't tilt up or down)
        studentAnimator.transform.rotation = Quaternion.LookRotation(direction);
    }
}
