using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseShredHandler : MonoBehaviour
{
    public GameObject _particleEffectObj;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private void Start ()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.rotation;
    }

    private void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.tag == "cheeseShred")
        {
            _particleEffectObj.SetActive (false);
        }
    }

    public void EnableParticleEffect (bool val)
    {
        _particleEffectObj.SetActive (val);
    }

    public void DisableAfterDelay (float delay)
    {
        StartCoroutine (DisableShred (delay));
    }

    IEnumerator DisableShred (float delay)
    {
        yield return new WaitForSeconds (delay);
        transform.localPosition = originalPosition;
        transform.rotation = originalRotation;
        EnableParticleEffect (true);
        transform.parent.gameObject.SetActive (false);
    }
}