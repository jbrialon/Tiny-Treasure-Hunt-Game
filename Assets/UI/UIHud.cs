using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHud : MonoBehaviour
{
    [SerializeField] public float timer = 30f; // Start at 30 seconds
    public bool isTimerRunning = false;
    public UIDocument uiDoc;
    private VisualElement uiContainer;
    private Label uiTimerLabel;
    private Label uiScoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        uiContainer = uiDoc.rootVisualElement.Q<VisualElement>("hud");
        uiTimerLabel = uiDoc.rootVisualElement.Q<Label>("timerLabel");
        uiScoreLabel = uiDoc.rootVisualElement.Q<Label>("scoreLabel");
    }
    public void showHUD () {
        uiContainer.AddToClassList("show-transition");
    }
    public void startTimer () {
        Debug.Log("start timer");
        if (!isTimerRunning)
        {
            StartCoroutine(updateTimer());
        }
    }

    // Update the text field with the rounded timer value
    private void updateText()
    {
        if (uiTimerLabel != null)
        {
            int seconds = Mathf.FloorToInt(timer);
            int milliseconds = Mathf.RoundToInt((timer - seconds) * 1000); // Round milliseconds to the nearest whole number

            milliseconds.ToString("00");

            string formattedTimer = string.Format("{0}:{1:D2}s", seconds, milliseconds);
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

    private void StopFunction() {
        // playerScore = scoreBoard.score;
        // instructionsMenu.showScore(playerScore);
    }
}
