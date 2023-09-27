using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHud : MonoBehaviour
{
    [SerializeField] public float timer = 30f; // Start at 30 seconds

    public bool isTimerRunning = false;
    public int score;
    public UIDocument uiDoc;

    private int scorePerHit = 1;
    private int internalStep = 1;

    private VisualElement uiContainer;
    private Label uiTimerLabel;
    private Label uiScoreLabel;
    private UIScoreboard Scoreboard;

    void Start()
    {
        uiContainer = uiDoc.rootVisualElement.Q<VisualElement>("hud");
        uiTimerLabel = uiDoc.rootVisualElement.Q<Label>("timerLabel");
        uiScoreLabel = uiDoc.rootVisualElement.Q<Label>("scoreLabel");
        Scoreboard = FindObjectOfType<UIScoreboard>();
    }

    public void ShowHUD () {
        if (internalStep == 1) {
            Debug.Log("Show HUD");
            internalStep = 2;
            uiContainer.AddToClassList("show-transition");
        }
    }
    
    public void HideHUD () {
        if (internalStep == 2) {
            Debug.Log("Hide HUD");
            uiContainer.RemoveFromClassList("show-transition");
        }
    }

    public void IncreaseScore() {
        score += scorePerHit;
        uiScoreLabel.text = score.ToString();
    }

    public void startTimer () {
        Debug.Log("Start Timer");
        if (!isTimerRunning)
        {
            StartCoroutine(updateTimer());
        }
    }

    // Update the text field with the formatted timer value
    private void updateText()
    {
        if (uiTimerLabel != null)
        {
            // Separate seconds and milliseconds without rounding
            int seconds = Mathf.FloorToInt(timer);
            int milliseconds = Mathf.FloorToInt((timer - Mathf.Floor(timer)) * 1000);

            // Ensure seconds and milliseconds are not negative
            seconds = Mathf.Max(0, seconds);
            milliseconds = Mathf.Max(0, milliseconds);

            // Format the timer as "seconds:milliseconds" with 2 digits for milliseconds
            string formattedTimer = $"{seconds}:{milliseconds.ToString("D2")}s"; // Using string interpolation
            uiTimerLabel.text = formattedTimer;
        }
    }

    // Coroutine to update the timer
    private IEnumerator updateTimer()
    {
        isTimerRunning = true;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            updateText();

            yield return null; // Wait for the next frame
        }

        timer = 0f; // Ensure timer reaches 0
        isTimerRunning = false;

        // Trigger your stop function here
        StopFunction();
    }

    // When the timer reach 0 we show the score
    private void StopFunction() {
        Debug.Log("Stop Timer");
        uiTimerLabel.text = "0:00s";
        Scoreboard.showScoreboard(score);
    }
}
