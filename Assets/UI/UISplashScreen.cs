using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UISplashScreen : MonoBehaviour
{

    [SerializeField] private float FadeOutTime = 5.0f;
    public UIDocument uiDoc;
    private VisualElement uiContainer;
    private VisualElement uiLogo;
    private VisualElement uiLogoMerkle;
    void Start()
    {
        uiContainer = uiDoc.rootVisualElement.Q<VisualElement>("splashscreen");
        uiLogo = uiDoc.rootVisualElement.Q<VisualElement>("logo");
        uiLogoMerkle = uiDoc.rootVisualElement.Q<VisualElement>("logoMerkle");

        // Call DelayedReveal to start the animation after a delay
        StartCoroutine(DelayedReveal());
    }

    public float GetFadeOutTime()
    {
        return FadeOutTime;
    }

    // To reveal the element after a delay
    IEnumerator DelayedReveal()
    {
        yield return new WaitForSeconds(0.2f); // Delay for 0.2 second
        
        uiLogo.AddToClassList("fade-in-animation");
        uiLogoMerkle.AddToClassList("fade-in-animation");

        StartCoroutine(HideSplashScreen());
    }

    IEnumerator HideSplashScreen()
    {
        yield return new WaitForSeconds(FadeOutTime);

        uiContainer.AddToClassList("fade-out-animation");
    }

}
