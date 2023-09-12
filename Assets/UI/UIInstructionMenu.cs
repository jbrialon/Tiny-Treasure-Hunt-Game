using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInstructionMenu : MonoBehaviour
{
    [SerializeField] private string instructionStepOne = "Move around with your phone to detect the sea.";
    [SerializeField] private string instructionStepTwo = "Touch the screen to spawn your ship. \n\nYou have 30s to find as many Treasures Chests as you can!";
    private int internalStep = 1;
    public UIDocument uiDoc;
    private VisualElement uiContainer;
    private VisualElement uiArrows;
    private VisualElement uiHandphone;
    private VisualElement uiChest;
    private Label uiLabel;
    private UISplashScreen SplashScreen;

    void Start()
    {
        uiContainer = uiDoc.rootVisualElement.Q<VisualElement>("instructions");
        uiArrows = uiDoc.rootVisualElement.Q<VisualElement>("arrows");
        uiHandphone = uiDoc.rootVisualElement.Q<VisualElement>("handphone");
        uiChest = uiDoc.rootVisualElement.Q<VisualElement>("chest");
        uiLabel = uiDoc.rootVisualElement.Q<Label>("textLabel");

        SplashScreen = FindObjectOfType<UISplashScreen>();
        uiLabel.text = instructionStepOne;

        StartCoroutine(DelayedReveal());
    }

    public void updateTextToStepTwo() {
        if (internalStep == 1) {
            internalStep = 2;
            uiLabel.text = instructionStepTwo;

            // hide arrows and HandPhone
            uiArrows.AddToClassList("hide-transition");
            uiHandphone.AddToClassList("hide-transition");
            uiChest.AddToClassList("show-transition");
        }
    }

    public void hideInstructions () {
        uiContainer.RemoveFromClassList("show-transition");
    }

    // To reveal the element after a delay
    IEnumerator DelayedReveal()
    {
        float delay = SplashScreen ? SplashScreen.GetFadeOutTime() + 1f : 4.0f;
        yield return new WaitForSeconds(delay);
        
        uiContainer.AddToClassList("show-transition");
        StartCoroutine(RotateVisual(1.3f));
    }

    // Rotate hand/hook animation
    IEnumerator RotateVisual(float delay)
    {
        yield return new WaitForSeconds(delay);

        uiHandphone.AddToClassList("rotate-transition");
        StartCoroutine(RotateVisualBack());
    }

    // Rotate hand/hook animation back
    IEnumerator RotateVisualBack()
    {
        yield return new WaitForSeconds(0.3f);

        uiHandphone.RemoveFromClassList("rotate-transition");

        // we loop this animation every 3s if we are on the step 1
        if (internalStep == 1) {
            StartCoroutine(RotateVisual(3.0f));
        }
    }
}
