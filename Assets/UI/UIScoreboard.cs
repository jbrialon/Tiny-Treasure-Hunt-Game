using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIScoreboard : MonoBehaviour
{
    public UIDocument uiDoc;
    private VisualElement uiContainer;
    private Label uiParagraphLabel;
    // Start is called before the first frame update
    void Start()
    {
        uiContainer = uiDoc.rootVisualElement.Q<VisualElement>("scoreboard");
        uiParagraphLabel = uiDoc.rootVisualElement.Q<Label>("paragraphLabel");
    }

    public void showScoreboard (int score) {
        uiParagraphLabel.text = $"You found {score.ToString()} Treasure chests!";
        uiContainer.AddToClassList("show-transition");
    }

}
