using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    public InputActionReference gripAction;

    public InputActionReference triggerAction;

    public Hand hand;
    
    void Start()
    {
        if (!gripAction.action.enabled) {
            gripAction.action.Enable();
        }

        if (!triggerAction.action.enabled) {
            triggerAction.action.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        hand.SetGrip(gripAction.action.ReadValue<float>());
        hand.SetTrigger(triggerAction.action.ReadValue<float>());
    }
}
