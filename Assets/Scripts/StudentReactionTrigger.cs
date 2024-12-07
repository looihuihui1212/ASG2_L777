using UnityEngine;

public class StudentReactionTrigger : MonoBehaviour
{
    public Animator studentAnimator; // Drag and drop the Animator component here
    public AnimationClip typingAnimation; // Drag the Typing animation clip here
    public AnimationClip angryAnimation;  // Drag the Angry animation clip here

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered: " + other.name);
        if (other.CompareTag("Laptop"))
        {
            Debug.Log("Laptop entered!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Something exited: " + other.name);
        if (other.CompareTag("Laptop"))
        {
            Debug.Log("Laptop exited!");
        }
    }


    private void PlayAnimation(AnimationClip clip)
    {
        if (studentAnimator != null && clip != null)
        {
            studentAnimator.Play(clip.name); // Play the animation by name
        }
        else
        {
            Debug.LogWarning("Animator or AnimationClip is missing!");
        }
    }
}
