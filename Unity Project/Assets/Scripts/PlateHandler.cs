using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> piecesOnPlate;
    [SerializeField] int threshold;
    private void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.tag == "cheeseShred")
        {
            if (other.gameObject.GetComponent<CheeseShredHandler> ())
            {
                AddPiecesToList (other.gameObject);

                // other.gameObject.GetComponent<CheeseShredHandler>().DisableAfterDelay(2f);
                // other.gameObject.GetComponent<CheeseShredHandler>().EnableParticleEffect(false);
            }
        }
    }

    void AddPiecesToList (GameObject piece)
    {
        piece.GetComponent<CheeseShredHandler> ().EnableParticleEffect (false);
        piecesOnPlate.Add (piece);

        RemovePiecesIfMoreThanThreshold ();
    }

    void RemovePiecesIfMoreThanThreshold ()
    {
        if (piecesOnPlate.Count > threshold)
        {
            piecesOnPlate[piecesOnPlate.Count - 1].GetComponent<CheeseShredHandler> ().DisableAfterDelay (1f);
            piecesOnPlate[piecesOnPlate.Count - 1].GetComponent<CheeseShredHandler> ().EnableParticleEffect (false);
            piecesOnPlate.RemoveAt (piecesOnPlate.Count - 1);
        }
    }
}