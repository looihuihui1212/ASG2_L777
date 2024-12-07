using UnityEngine;
using TMPro;

public class UnifiedScoreManager : MonoBehaviour
{
    public static UnifiedScoreManager Instance;

    // UI References
    public TextMeshProUGUI plasticBottleScoreText;
    public TextMeshProUGUI metalCanScoreText;
    public TextMeshProUGUI paperScoreText;
    public TextMeshProUGUI chairScoreText;
    public TextMeshProUGUI laptopScoreText;

    // Chair Scores
    private int squareChairCount = 0;
    private int circleChairCount = 0;
    private const int totalSquareChairs = 2;
    private const int totalCircleChairs = 2;

    // Bin Scores
    private int plasticBottleScore = 0;
    private int metalCanScore = 0;
    private int paperScore = 0;
    private const int maxPlasticBottles = 2;
    private const int maxMetalCans = 2;
    private const int maxPapers = 2;

    // Laptop Score
    private int laptopCount = 0;
    private const int totalLaptops = 2;

    // Timer Reference
    public Timer timer; // Drag the Timer script object in the Inspector

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Methods for Chair Scores
    public void UpdateChairScore(string chairName)
    {
        if (chairName.Contains("CircleChair") && circleChairCount < totalCircleChairs)
        {
            circleChairCount++;
        }
        else if (chairName.Contains("SquareChair") && squareChairCount < totalSquareChairs)
        {
            squareChairCount++;
        }

        // Update the UI for chairs
        chairScoreText.text = $"Square Chairs: {squareChairCount}/{totalSquareChairs}\n" +
                              $"Circle Chairs: {circleChairCount}/{totalCircleChairs}";

        CheckIfAllConditionsMet();
    }

    // Methods for Bin Scores
    public void AddBinScore(string binType)
    {
        switch (binType)
        {
            case "PlasticBottle":
                if (plasticBottleScore < maxPlasticBottles)
                {
                    plasticBottleScore++;
                }
                break;

            case "MetalCan":
                if (metalCanScore < maxMetalCans)
                {
                    metalCanScore++;
                }
                break;

            case "Paper":
                if (paperScore < maxPapers)
                {
                    paperScore++;
                }
                break;
        }

        plasticBottleScoreText.text = $"Plastic Bottles: {plasticBottleScore}/{maxPlasticBottles}";
        metalCanScoreText.text = $"Metal Cans: {metalCanScore}/{maxMetalCans}";
        paperScoreText.text = $"Paper: {paperScore}/{maxPapers}";

        CheckIfAllConditionsMet();
    }

    // Method for Laptop Score
    public void UpdateLaptopScore()
    {
        if (laptopCount < totalLaptops)
        {
            laptopCount++;
        }

        laptopScoreText.text = $"Remove laptops: {laptopCount}/{totalLaptops} done";
        CheckIfAllConditionsMet();
    }

    private void CheckIfAllConditionsMet()
    {
        if (squareChairCount == totalSquareChairs &&
            circleChairCount == totalCircleChairs &&
            plasticBottleScore == maxPlasticBottles &&
            metalCanScore == maxMetalCans &&
            paperScore == maxPapers &&
            laptopCount == totalLaptops)
        {
            StopTimer();
        }
    }

    private void StopTimer()
    {
        if (timer != null)
        {
            timer.StopTimer(); // Call the method to stop the timer
        }
    }
}
