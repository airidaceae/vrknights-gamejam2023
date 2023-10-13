using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Using the input system so that we are able to reference the inputs from the controller
using UnityEngine.InputSystem;


public class AnimateHand : MonoBehaviour
{
    //The input action properties are components downloaded as samples from earlier. We will be looking for pinching and gripping. 
    //It is public, so we can drag this information into the code from the inspector 
    public InputActionProperty PinchAnimatinAction;
    public InputActionProperty gripAnimationAction;
    //the hand animator is the component we are accessing to change it's value- in the code here. 
    public Animator handAnimator;



    // Update is called once per frame, so every frame we are checking for the value of the input 
    //and putting it into the value that changes the animation trigger and grip 
    void Update()
    {
        float triggerValue = PinchAnimatinAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripvalue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripvalue);
    }
}
