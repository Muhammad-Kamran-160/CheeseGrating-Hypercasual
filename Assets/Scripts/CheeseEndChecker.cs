using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseEndChecker : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "cheeseEnd")
        {
            Debug.Log ("cheese has ended");
            // Stop moving here and end level
            Gameplay_References._instance._gameplayManger.StartNextLevel ();
        }
    }
}