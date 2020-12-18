using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSlicerSlicesTracker : MonoBehaviour
{
    public enum CheeseType
    {
        other,
        ricota
    }

    public CheeseType _cheeseType;

    GameObject pos_Slice;
    GameObject neg_Slice;

    public void GetCutSlices (GameObject pos, GameObject neg)
    {
        pos_Slice = pos;
        neg_Slice = neg;

        RemoveBottomPiece (pos_Slice);
        TweenPiece (neg_Slice);
    }

    public void RemoveBottomPiece (GameObject ob)
    {
        Destroy (ob);
    }

    public void RemoveAllPieces ()
    {
        if (pos_Slice != null)
            Destroy (pos_Slice);

        if (neg_Slice != null)
            Destroy (neg_Slice);
    }

    public void TweenPiece (GameObject ob)
    {
        switch (_cheeseType)
        {
            case CheeseType.other:
                iTween.MoveBy (ob, iTween.Hash ("x", 0.03f, "time", 0.005f, "easeType", iTween.EaseType.easeInQuad));
                break;

                case CheeseType.ricota:
                iTween.MoveBy (ob, iTween.Hash ("z", 0.03f, "time", 0.005f, "easeType", iTween.EaseType.easeInQuad));
                break;
                
        }
    }
}