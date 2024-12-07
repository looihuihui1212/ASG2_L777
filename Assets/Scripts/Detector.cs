using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class UpdateTextOnSelect : MonoBehaviour
{
    public TextMeshProUGUI interactableNameText; // Reference to the text component

    public void UpdateText(SelectEnterEventArgs args)
    {
        // Update text with the name of the object
        var interactable = args.interactableObject.transform.gameObject;
        interactableNameText.text = interactable.name + " is attached!";
    }
}
