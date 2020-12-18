using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour
{
    public static BoundaryChecker _instance;

    bool isOutOFBounds = false;

    Vector3 stayPosition;
    private void Awake ()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy (gameObject);
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.tag == "boundary")
        {
            stayPosition = transform.position;
            isOutOFBounds = true;
        }
    }

    private void OnTriggerStay (Collider other)
    {
        if (other.tag == "boundary")
        {
            isOutOFBounds = false;
        }
    }

    public void ResetPoisitionIfOutOfBounds ()
    {
        if (isOutOFBounds)
            Gameplay_References._instance._gameplayManger.RelocateToOriginalPosition ();
    }
}