using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseSlicer : MonoBehaviour
{
    public LayerEnabler _correspondingLayerEnabler;
    bool canMoveNow = true;
    bool checkResetOnExit;

    private void Start ()
    {
        Gameplay_References._instance._gameplayManger.AddKnife (gameObject);

        if (_correspondingLayerEnabler != null)
            _correspondingLayerEnabler.SetCorrespondingLayer (gameObject);
    }

    public void MoveSlicer ()
    {
        if (canMoveNow)
        {
            iTween.MoveBy (gameObject, iTween.Hash ("y", 0.02f, "time", 0.2f, "easeType", iTween.EaseType.easeInQuad));
            iTween.MoveBy (gameObject, iTween.Hash ("y", -0.02f, "time", 0.5f, "delay", 0.2f, "easeType", iTween.EaseType.easeInQuad));
            canMoveNow = false;

            StartCoroutine (canMoveAfterDelay (0.2f));
        }

        // StartCoroutine(EnableAfterDelay(0.2f, false));
    }

    public void EnableCorrespondingLayer ()
    {
        if (_correspondingLayerEnabler != null)
            _correspondingLayerEnabler.gameObject.SetActive (true);
    }

    IEnumerator canMoveAfterDelay (float delay)
    {
        yield return new WaitForSeconds (delay);

        canMoveNow = true;
    }

    IEnumerator EnableAfterDelay (float delay, bool val)
    {
        yield return new WaitForSeconds (delay);
        gameObject.SetActive (val);
    }

    public void EnableInstantly ()
    {
        StartCoroutine (EnableAfterDelay (0, true));
    }
}