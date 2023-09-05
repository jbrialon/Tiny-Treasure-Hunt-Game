using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionsMenu : MonoBehaviour
{  
    [SerializeField] string instructionStepOne = "Move around with your phone to detect the ground";
    [SerializeField] string instructionStepTwo = "Touch the Screen to Spawn your Ship, you have 30s to find as many Treasures Chests as you can!";
    private TMP_Text instructionsText;
    private int internalStep = 1;
    void Start()
    {
        instructionsText = GetComponent<TMP_Text>();
        instructionsText.gameObject.SetActive(true);
        instructionsText.text = instructionStepOne;
    }

    public void updateTextToStepTwo() {
        if (internalStep == 1) {
            internalStep = 2;
            instructionsText.text = instructionStepTwo;
        }
    }

    public void hideInstructions () {
        instructionsText.gameObject.SetActive(false);
    }

    public void showScore (int score) {
        internalStep = 3;
        instructionsText.gameObject.SetActive(true);
        instructionsText.text = $"Congratulation, you found {score} Treasure Chests!";
    }
}
