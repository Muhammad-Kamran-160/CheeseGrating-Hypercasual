using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSliceObject : MonoBehaviour
{
    Rigidbody rb;
    bool isHit = false;
    bool canBeCut = false;

    private void Awake ()
    {
        rb = GetComponent<Rigidbody> ();
    }

    public void OnKnifeHit ()
    {
        // Debug.Log ("can be cut now: " + canBeCut);
        // Debug.Log ("isHit " + isHit);
        if (!isHit && canBeCut)
        {
            // rb.isKinematic = false;
            // rb.AddForce (new Vector3 (0, 0f, 0), ForceMode.Impulse);
            isHit = true;
            // Gameplay_References._instance._slicesTracker.OnCutSingleSlice ();
            // Gameplay_References._instance._gratedPiecesHandler.SpawnPiece(transform.position);
            Destroy(gameObject);
        }
    }

    public void CanBeCutNow ()
    {
        canBeCut = true;
    }
}