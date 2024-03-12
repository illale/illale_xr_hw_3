using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetBallPosition : MonoBehaviour
{
    public InputActionReference inputActionReference;
    public GameObject ball;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        inputActionReference.action.Enable();
        rigidbody = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputActionReference.action.IsPressed()) {
            ball.transform.position = new Vector3(0, 1f, 0.5f);
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector3.zero);
            rigidbody.useGravity = false;
        }
    }
}
