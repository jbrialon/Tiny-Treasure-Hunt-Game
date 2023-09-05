using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] public float timer = 60f; // Start at 60 seconds
    public bool isTimerRunning = false;
    private TMP_Text timerText;
    private InstructionsMenu instructionsMenu;
    private ScoreBoard scoreBoard;

    private int playerScore = 0;
    void Start()
    {
        instructionsMenu = FindObjectOfType<InstructionsMenu>();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        timerText = GetComponent<TMP_Text>();
        timerText.gameObject.SetActive(false);
        timerText.text = timer.ToString();
    }

    // Start the timer
    public void StartTimer()
    {   
        timerText.gameObject.SetActive(true);
        if (!isTimerRunning)
        {
            StartCoroutine(UpdateTimer());
        }
    }

    // Coroutine to update the timer
    private IEnumerator UpdateTimer()
    {
        isTimerRunning = true;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            UpdateText();

            yield return null; // Wait for the next frame
        }

        timer = 0f; // Ensure timer reaches 0
        isTimerRunning = false;

        // Trigger your stop function here
        StopFunction();
    }

    // Update the text field with the rounded timer value
    private void UpdateText()
    {
        if (timerText != null)
        {
            int roundedTimer = Mathf.RoundToInt(timer);
            timerText.text = $"{roundedTimer.ToString()}s";
        }
    }

    private void StopFunction() {
        playerScore = scoreBoard.score;
        instructionsMenu.showScore(playerScore);
    }
}
