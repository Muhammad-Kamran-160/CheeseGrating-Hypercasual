using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    int count = 0;
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "sliceCube")
        {
            if (other.GetComponent<SingleSliceObject> ())
                other.GetComponent<SingleSliceObject> ().OnKnifeHit ();

            Debug.Log("Count: " + count);

            count++;

            if (count >= 8)
                gameObject.SetActive (false);
        }
    }

    public void ResetCount()
    {
        count = 0;
    }
}