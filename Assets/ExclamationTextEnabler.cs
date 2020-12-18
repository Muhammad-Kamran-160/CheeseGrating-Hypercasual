using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationTextEnabler : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "cheese")
        {
            Gameplay_References._instance._gameplayManger.EnableExclamationText();
        }
    }
}