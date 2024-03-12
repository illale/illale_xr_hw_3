using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private bool isThrown; 
    private Rigidbody grabbedObjectRigidBody;
    private List<Vector3> trackPos = new List<Vector3>();
    private float force = 1000f;


    private void Start()
    {
        action.action.Enable();
        // Find the other hand
        lastPosition = new Vector3(0, 0, 0);
        lastRotation = new Quaternion(0, 0, 0, 0);
        isThrown = false;
    }

    void Update()
    {  
        grabbing = action.action.IsPressed();
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0]: null;

            if (grabbedObject)
            {
                if (!isThrown) {
                    grabbedObjectRigidBody = grabbedObject.GetComponent<Rigidbody>();
                    grabbedObjectRigidBody.useGravity = false;
                    grabbedObjectRigidBody.isKinematic = true;
                    isThrown = true;
                }
                
                // Change these to add the delta position and rotation instead
                // Save the position and rotation at the end of Update function, so you can compare previous pos/rot to  current 
                Quaternion rotationChange = transform.rotation * Quaternion.Inverse(lastRotation);
                Vector3 positionChange = transform.position - lastPosition;
                Vector3 objToController = grabbedObject.position - transform.position;
                Vector3 rotatedVector = objToController - rotationChange * objToController;
                grabbedObject.rotation = rotationChange * grabbedObject.rotation;
                grabbedObject.position += positionChange - rotatedVector;

                //grabbedObject.position = transform.position;
                //grabbedObject.rotation = transform.rotation;

                if (trackPos.Count > 15) {
                    trackPos.RemoveAt(0);
                }

                trackPos.Add(transform.position);

            }
        }
        // If let go of button, release object
        else if (grabbedObject) {
            Throw();
            grabbedObject = null;
            lastRotation = new Quaternion(0, 0, 0, 0);
        }

        // Should save the current position and rotation here
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="grabbable")
            nearObjects.Remove(t);
    }

    private void Throw() {
        // Example from this video: https://www.youtube.com/watch?v=jVmqMy5vusU
        if (grabbedObject != null) {
            Vector3 direction = trackPos[trackPos.Count - 1] - trackPos[0];
            grabbedObjectRigidBody.isKinematic = false;
            grabbedObjectRigidBody.AddForce(direction*force);
            grabbedObjectRigidBody.useGravity = true;
            isThrown = false;
        }
        
    }
}