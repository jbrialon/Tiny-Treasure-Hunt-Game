using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoard : MonoBehaviour
{
    public int score;
    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "0";
    }

    public void IncreaseScore(int amountToIncrease) {
        score += amountToIncrease;
        scoreText.text = score.ToString();
    }
}
