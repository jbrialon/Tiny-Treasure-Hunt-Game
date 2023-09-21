using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class krakenAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
        animator.SetTrigger("One");
        animator.SetTrigger("Two");
        animator.SetTrigger("Three");
        animator.SetTrigger("Four");
        animator.SetTrigger("Five");
        animator.SetTrigger("Six");
        Debug.Log("KrakenAnimationController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
