using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraterArea : MonoBehaviour
{
    bool checkResetOnExit;
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "cheese")
        {
            // Debug.Log ("Entered");
            checkResetOnExit = true;
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.tag == "cheese")
        {
            if (checkResetOnExit)
            {
                // Debug.Log ("exited");

                // Gameplay_References._instance._gameplayManger.RelocateCheeseBlock ();
                Gameplay_References._instance._gameplayManger.EnableKnifes ();
                checkResetOnExit = false;
            }
        }
    }
}