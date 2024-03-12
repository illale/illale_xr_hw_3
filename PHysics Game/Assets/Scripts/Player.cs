using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private int hitpoints;
    private int score;
    private bool lost = false;
    private bool start = false;
    public InputActionReference inputAction;

    void Start()
    {
        hitpoints = 3;
        score = 0;
        inputAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputAction.action.IsPressed()) {
            start = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform t = other.transform;
        if(t && t.tag.ToLower()=="projectile")
        {
            hitpoints -= 1;
        }
    }

    public void ReduceLife() {
        hitpoints -= 1;
        if (hitpoints <= 0) {
            lost = true;
        }
    }

    public void UpdateScore() {
        score += 1;
        Debug.Log("Score is now: " + score);
    }

    public int GetScore() {
        return score;
    }

    public int GetHitpoints() {
        return hitpoints; 
    }

    public void Reset() {
        hitpoints = 3;
        score = 0;
    }

    public bool GetStart() {
        return start;
    }

    public void SetStart(bool isStart) {
        start = isStart;
    }

}
