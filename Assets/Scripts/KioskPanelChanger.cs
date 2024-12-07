using System.Collections;
using UnityEngine;

public class KioskSceneChanger : MonoBehaviour
{
    [Header("Fade Settings")]
    public float fadeDuration = 1.0f;  // Duration of the fade effect

    [Header("Current Canvas")]
    public CanvasGroup currentCanvas;  // The currently active canvas

    // Call this method and drag the next canvas dynamically in the Inspector
    public void ChangeToCanvas(CanvasGroup nextCanvas)
    {
        StartCoroutine(TransitionCanvases(currentCanvas, nextCanvas));
    }

    private IEnumerator TransitionCanvases(CanvasGroup current, CanvasGroup next)
    {
        // Fade out the current canvas
        yield return StartCoroutine(FadeOut(current));

        // Deactivate the current canvas
        current.gameObject.SetActive(false);

        // Activate the next canvas
        next.gameObject.SetActive(true);

        // Ensure the next canvas starts fully transparent
        next.alpha = 0f;

        // Fade in the next canvas
        yield return StartCoroutine(FadeIn(next));

        // Update the current canvas reference
        currentCanvas = next;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = 1 - Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
}
