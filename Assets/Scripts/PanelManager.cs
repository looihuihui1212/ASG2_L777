using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;

    [Header("Instruction Canvas")]
    public GameObject instructionCanvas;

    [Header("Transition Settings")]
    public float transitionDuration = 0.5f; // Duration of the fade in/out effect
    public CanvasGroup panel1CanvasGroup; // Add a CanvasGroup to each panel for fade effects
    public CanvasGroup panel2CanvasGroup;
    public CanvasGroup panel3CanvasGroup;
    public CanvasGroup panel4CanvasGroup;

    /// <summary>
    /// Activates Panel 2 and deactivates Panel 1 with transition.
    /// </summary>
    public void ShowPanel2()
    {
        StartCoroutine(TransitionToPanel(panel1CanvasGroup, panel2CanvasGroup));
    }

    /// <summary>
    /// Activates Panel 3 and deactivates Panel 2 with transition.
    /// </summary>
    public void ShowPanel3()
    {
        StartCoroutine(TransitionToPanel(panel2CanvasGroup, panel3CanvasGroup));
    }

    /// <summary>
    /// Activates Panel 4 and deactivates Panel 3 with transition.
    /// </summary>
    public void ShowPanel4()
    {
        StartCoroutine(TransitionToPanel(panel3CanvasGroup, panel4CanvasGroup));
    }

    /// <summary>
    /// Hides the instruction canvas.
    /// </summary>
    public void CloseInstructionCanvas()
    {
        instructionCanvas.SetActive(false);
    }

    /// <summary>
    /// Handles the transition between panels using fade in/out effects.
    /// </summary>
    private System.Collections.IEnumerator TransitionToPanel(CanvasGroup currentPanel, CanvasGroup nextPanel)
    {
        // Fade out the current panel
        yield return StartCoroutine(FadeOut(currentPanel));

        // Set the current panel inactive
        currentPanel.gameObject.SetActive(false);

        // Activate and fade in the next panel
        nextPanel.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn(nextPanel));
    }

    /// <summary>
    /// Fades out a panel over time.
    /// </summary>
    private System.Collections.IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }

    /// <summary>
    /// Fades in a panel over time.
    /// </summary>
    private System.Collections.IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    // Optional: Initialize default state
    private void Start()
    {
        // Set the initial active panel with proper alpha values
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);

        panel1CanvasGroup.alpha = 1f;
        panel2CanvasGroup.alpha = 0f;
        panel3CanvasGroup.alpha = 0f;
        panel4CanvasGroup.alpha = 0f;

        instructionCanvas.SetActive(true); // Ensure the instruction canvas is visible
    }
}
