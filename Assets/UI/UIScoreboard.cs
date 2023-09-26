using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIScoreboard : MonoBehaviour
{
    public UIDocument uiDoc;
    private VisualElement uiContainer;
    private Label uiTitleLabel;
    private Label uiParagraphLabel;
    private bool isScoreboardHidden = true;
    
    void Start()
    {
        uiContainer = uiDoc.rootVisualElement.Q<VisualElement>("scoreboard");
        uiTitleLabel = uiDoc.rootVisualElement.Q<Label>("titleLabel");
        uiParagraphLabel = uiDoc.rootVisualElement.Q<Label>("paragraphLabel");
    }

    public void showScoreboard (int score) {
        if (isScoreboardHidden) {
            isScoreboardHidden = false;
            uiTitleLabel.text = $"Congratulations !";
            uiParagraphLabel.text = $"You found {score.ToString()} Treasure chests!";
            uiContainer.AddToClassList("container--success");
            uiContainer.AddToClassList("show-transition");
        }
    }

    public void showGameOver () {
        if (isScoreboardHidden) {
            isScoreboardHidden = false;
            uiTitleLabel.text = $"Unfortunately,";
            uiParagraphLabel.text = $"your pirate ship has fallen victim to the fearsome kraken!";
            uiContainer.AddToClassList("container--fail");
            uiContainer.AddToClassList("show-transition");
        }
    }
}
