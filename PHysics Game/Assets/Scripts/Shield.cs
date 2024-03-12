using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shield : MonoBehaviour
{
    public InputActionReference inputActionReference;
    public InputActionReference inputActionReferenceSecond;
    public GameObject hand;

    void Start()
    {
        inputActionReference.action.Enable();
        inputActionReferenceSecond.action.Enable();
    }

    void Update()
    {
        if (inputActionReference.action.IsPressed() && inputActionReference.action.IsPressed()) {
            transform.position = hand.transform.position + new Vector3(0f, 0f, 0.5f);
        } else {
            transform.position = new Vector3(0, -10, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="projectile") {
            t.GameObject().GetComponent<Projectile>().InvertDirection(transform.forward + transform.right + transform.up);
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
