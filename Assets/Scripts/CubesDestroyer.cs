using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesDestroyer : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if(other.tag == "cheeseShred")
        {
            // Destroy(other.gameObject);
            other.gameObject.transform.localPosition = new Vector3(0,-0.17f,0);
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}