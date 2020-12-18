using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GratedPiecesHandler : MonoBehaviour
{
    public void SpawnPiece (Vector3 position)
    {
        GameObject piece = ObjectPooler.SharedInstance.GetPooledObject (0);
        piece.transform.position = position;
        piece.SetActive(true);
    }
}