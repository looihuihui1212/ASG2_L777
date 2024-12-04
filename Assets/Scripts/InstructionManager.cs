using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public GameObject[] panels; // Array to hold all instruction panels
    private int currentPanelIndex = 0; // Tracks the current panel

    // Show the current panel and hide all others
    void ShowCurrentPanel()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == currentPanelIndex);
        }
    }

    // Move to the next panel
    public void NextPanel()
    {
        if (currentPanelIndex < panels.Length - 1)
        {
            currentPanelIndex++;
            ShowCurrentPanel();
        }
    }

    // Close the instruction canvas
    public void CloseInstructions()
    {
        gameObject.SetActive(false);
    }

    // Initialize the first panel
    void Start()
    {
        ShowCurrentPanel();
    }
}
