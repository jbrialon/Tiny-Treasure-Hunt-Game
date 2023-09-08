using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInstructionMenu : MonoBehaviour
{
    [SerializeField] string instructionStepOne = "Move around with your phone to detect the ground";
    [SerializeField] string instructionStepTwo = "Touch the Screen to Spawn your Ship, you have 30s to find as many Treasures Chests as you can!";
    private int internalStep = 1;
    public UIDocument uiDoc;
    void Start()
    {

    }
    public void updateTextToStepTwo() {
        if (internalStep == 1) {
            internalStep = 2;
            // uiInstruction.text = instructionStepTwo;
        }
    }

}
