using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFiller : MonoBehaviour
{
    public GameObject cheeseEndChecker;
    public GameObject cheeseEnd;

    float distance = 0;

    private void Update ()
    {
        if(cheeseEndChecker != null && cheeseEnd != null)
        {
            distance = Mathf.Clamp(Vector3.Distance(cheeseEndChecker.transform.position, cheeseEnd.transform.position), 0 , 1f);

            Debug.Log("distance = " + distance);
        }
    }
}