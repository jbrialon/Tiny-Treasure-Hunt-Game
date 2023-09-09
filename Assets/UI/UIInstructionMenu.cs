using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInstructionMenu : MonoBehaviour
{
    [SerializeField] string instructionStepOne = "Move around with your phone to detect the sea.";
    [SerializeField] string instructionStepTwo = "Touch the Screen to Spawn your Ship, you have 30s to find as many Treasures Chests as you can!";
    private int internalStep = 1;
    public UIDocument uiDoc;
    private VisualElement uiContainer;
    private VisualElement uiArrows;
    private VisualElement uiHandphone;
    private Label uiLabel;
    void Start()
    {
        uiContainer = uiDoc.rootVisualElement.Q<VisualElement>("instructions");
        uiArrows = uiDoc.rootVisualElement.Q<VisualElement>("arrows");
        uiHandphone = uiDoc.rootVisualElement.Q<VisualElement>("handphone");
        uiLabel = uiDoc.rootVisualElement.Q<Label>("textLabel");

        uiLabel.text = instructionStepOne;

        StartCoroutine(DelayedReveal());
    }
    public void updateTextToStepTwo() {
        if (internalStep == 1) {
            internalStep = 2;
            uiLabel.text = instructionStepTwo;
        }
    }

    // To reveal the element after a delay
    IEnumerator DelayedReveal()
    {
        yield return new WaitForSeconds(4.0f); // Delay for 1 second
        
        uiContainer.AddToClassList("reveal-animation");
        StartCoroutine(RotateVisual());
    }

    IEnumerator RotateVisual()
    {
        yield return new WaitForSeconds(0.9f);

        uiHandphone.AddToClassList("rotate-animation");
        StartCoroutine(RotateVisualBack());
    }

    IEnumerator RotateVisualBack()
    {
        yield return new WaitForSeconds(0.3f);

        uiHandphone.RemoveFromClassList("rotate-animation");
    }
}
