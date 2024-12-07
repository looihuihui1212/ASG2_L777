using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeElapsed = 0f; // Time that has passed
    public TMP_Text timerText; // Reference to the TMP_Text to display the timer
    private bool isRunning = true; // Tracks if the timer is running

    void Update()
    {
        if (isRunning)
        {
            // Increase the timeElapsed by the time passed since the last frame
            timeElapsed += Time.deltaTime;

            // Calculate minutes and seconds from the timeElapsed
            int minutes = Mathf.FloorToInt(timeElapsed / 60);
            int seconds = Mathf.FloorToInt(timeElapsed % 60);

            // Update the displayed timer text in the format "Minutes:Seconds"
            timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds); // D2 formats numbers with leading zeros if needed
        }
    }

    public void StopTimer()
    {
        isRunning = false; // Stop the timer
    }
}
