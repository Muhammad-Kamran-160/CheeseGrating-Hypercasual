using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicesTracker : MonoBehaviour
{
    [SerializeField] SliceLayer[] _sliceLayers;
    int currentLayer = -1;
    int currentLayerSlicesCount;
    int numberOfSlicesCut = 0;
    float totalNumberOfSlicesCut = 0;
    [SerializeField] float totalNumberOfSlices;

    private void Start ()
    {
        CalculateTotalNumberOfSlices();
        EnableNextLayer ();
    }

    void EnableNextLayer ()
    {
        Debug.Log ("enabling next Layer");
        currentLayer++;

        if (currentLayer >= _sliceLayers.Length)
        {
            // Gameplay_References._instance._gameplayManger.EnableRestartButton ();
            return;
        }

        Gameplay_References._instance._gameplayManger.EnableKnifes();

        currentLayerSlicesCount = _sliceLayers[currentLayer].singleSliceCubes.Length;

        for (int i = 0; i < _sliceLayers[currentLayer].singleSliceCubes.Length; i++)
        {
            _sliceLayers[currentLayer].singleSliceCubes[i].GetComponent<SingleSliceObject> ().CanBeCutNow ();
        }
    }

    

    public void OnCutSingleSlice ()
    {
        numberOfSlicesCut++;
        totalNumberOfSlicesCut++;

        Gameplay_References._instance._uiManager.SetLevelFillerValue(totalNumberOfSlicesCut/totalNumberOfSlices);

        if (numberOfSlicesCut >= currentLayerSlicesCount)
        {
            numberOfSlicesCut = 0;
            EnableNextLayer ();
            Gameplay_References._instance._gameplayManger.CanBeRelocatedNow = true;
        }
    }

    void CalculateTotalNumberOfSlices()
    {
        for (int i = 0; i < _sliceLayers.Length; i++)
        {
            for (int j = 0; j < _sliceLayers[i].singleSliceCubes.Length; j++)
            {
                totalNumberOfSlices++;
            }
        }
    }
}

[System.Serializable]
public class SliceLayer
{
    public GameObject[] singleSliceCubes;
}