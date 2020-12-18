using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicer;
using UnityEngine;

public class CheeseHandler : MonoBehaviour
{
    public static CheeseHandler _instance;
    IBzSliceableAsync _sliceableAsync;
    Plane plane;
    [SerializeField] MoveCheese _moveCheeseScript;
    [SerializeField] GameObject cheeseEnd;

    public float TotalPiecesCount;
    [SerializeField] float piecesCutCount = 0;
    float numberOfPartsToPlayPlayExclamationTextOn = 4;
    [SerializeField] float partToPlayExclamationTextOn;
    float onePart;
    [SerializeField] GameObject cheeseShredPrefab;

    [SerializeField] MeshSlicerSlicesTracker.CheeseType _cheeseType;

    private void Awake ()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy (gameObject);
        }

        plane = new Plane (new Vector3 (0.0f, -2f, 0.0f), new Vector3 (-0.2f, 0.8f, 1f));
        _sliceableAsync = GetComponent<IBzSliceableAsync> ();

        Vibration.Init ();
    }

    private void Start ()
    {
        Gameplay_References._instance._meshSlicesTracker._cheeseType = _cheeseType;
        Gameplay_References._instance._gameplayManger.SetCurrentCheese (transform.parent.gameObject);
        Gameplay_References._instance._gameplayManger.SetCheeseShred (cheeseShredPrefab);

        partToPlayExclamationTextOn = onePart = TotalPiecesCount/numberOfPartsToPlayPlayExclamationTextOn;
    }

    public void CreateSlice (GameObject slicer)
    {
        SpawnSeveralCutPieces (slicer);
        _sliceableAsync.Slice (plane, 0, OnSliceFinished);
    }

    void SpawnSeveralCutPieces (GameObject slicer)
    {
        for (int i = 0; i < slicer.transform.childCount; i++)
        {
            Gameplay_References._instance._gratedPiecesHandler.SpawnPiece (slicer.transform.GetChild (i).position);
        }
    }

    public void OnSliceFinished (BzSliceTryResult result)
    {
        if (!result.sliced)
            return;

        piecesCutCount++;
        // Debug.Log("pieces Cut: " + piecesCutCount);

        Gameplay_References._instance._soundManager.PlaySoundEffectOneShot (0);
        Gameplay_References._instance._uiManager.SetLevelFillerValue (piecesCutCount / TotalPiecesCount);
        PlayExclamationText();
        Gameplay_References._instance._meshSlicesTracker.GetCutSlices (result.outObjectPos, result.outObjectNeg);

        if (GameConstants.isVibrationEnabled)
        {
            // Debug.Log ("vibrating");
            Vibration.VibratePeek ();
        }
    }

    public void PlayExclamationText()
    {
        if(piecesCutCount == partToPlayExclamationTextOn && piecesCutCount != TotalPiecesCount)
        {
            Gameplay_References._instance._gameplayManger.EnableExclamationText();
            partToPlayExclamationTextOn = onePart + partToPlayExclamationTextOn;
        }
    }
    
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "slicer")
        {
            // Gameplay_References._instance._gameplayManger.CheckKnifeCutCount ();
            other.gameObject.SetActive (false);
            
            if(other.GetComponent<CheeseSlicer>() != null)
                other.GetComponent<CheeseSlicer>().EnableCorrespondingLayer();

            // other.GetComponent<CheeseSlicer> ().MoveSlicer ();
            CreateSlice (other.gameObject);
        }
    }

    public void StopMovingCheese ()
    {
        _moveCheeseScript.enabled = false;
    }
}