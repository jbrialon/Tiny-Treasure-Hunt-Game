using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInteractionScript : MonoBehaviour
{
    [SerializeField] string instructionStepOne = "Move around with your phone to detect the ground";
    [SerializeField] string instructionStepTwo = "Touch the Screen to Spawn your Ship, you have 30s to find as many Treasures Chests as you can!";
    private int internalStep = 1;
    public UIDocument uiDoc;
    private Button uiButton;
    private Label uiTitle;
    private Label uiInstruction;
    void Start()
    {
        uiButton = uiDoc.rootVisualElement.Q<Button>("testButton");
        uiTitle = uiDoc.rootVisualElement.Q<Label>("title");
        uiInstruction = uiDoc.rootVisualElement.Q<Label>("instruction");
        uiInstruction.text = instructionStepOne;
        
        uiButton.RegisterCallback<ClickEvent>(RevealTextAnimation);
        // Call DelayedReveal to start the animation after a delay
        StartCoroutine(DelayedReveal());
    }
    private void RevealTextAnimation(ClickEvent clickEvent) {
        uiTitle.AddToClassList("reveal-animation");
        uiInstruction.AddToClassList("reveal-animation");
    }
    public void HideTextAnimation () {
        uiTitle.RemoveFromClassList("reveal-animation");
        uiInstruction.RemoveFromClassList("reveal-animation");
    }
    public void updateTextToStepTwo() {
        if (internalStep == 1) {
            internalStep = 2;
            uiInstruction.text = instructionStepTwo;
        }
    }
    // To reveal the element after a delay
    IEnumerator DelayedReveal()
    {
        yield return new WaitForSeconds(1.0f); // Delay for 1 second
        uiTitle.AddToClassList("reveal-animation");
        uiInstruction.AddToClassList("reveal-animation");
    }
}
