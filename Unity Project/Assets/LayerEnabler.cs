using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerEnabler : MonoBehaviour
{
    [SerializeField] private GameObject correspondingLayer;

    bool checkResetOnExit = false;

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "cheese")
        {
            checkResetOnExit = true;
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.tag == "cheese")
        {
            Debug.Log ("exited");

            if (checkResetOnExit)
            {
                // Gameplay_References._instance._gameplayManger.RelocateCheeseBlock ();
                // Gameplay_References._instance._gameplayManger.EnableKnifes ();
                correspondingLayer.SetActive (true);
                gameObject.SetActive (false);
                checkResetOnExit = false;
            }
        }
    }

    public void SetCorrespondingLayer (GameObject layer)
    {
        correspondingLayer = layer;
    }
}