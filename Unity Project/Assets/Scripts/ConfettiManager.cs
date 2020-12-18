using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiManager : MonoBehaviour
{
    [SerializeField] GameObject _confettiPrefab;
    [SerializeField] int numberOfBursts;
    [SerializeField] float timeBetweenEachBurst;

    bool hasConfettiEnded;

    public void PlayConfetti ()
    {
        hasConfettiEnded = false;
        StartCoroutine (PlayConfettiNow ());
    }

    IEnumerator PlayConfettiNow ()
    {
        for (int i = 0; i < numberOfBursts; i++)
        {
            GameObject newConf = Instantiate(_confettiPrefab);
            newConf.SetActive (true);
            if (i == numberOfBursts - 1)
            {
                yield return new WaitForSeconds (2f);
            }
            else
                yield return new WaitForSeconds (timeBetweenEachBurst);

            // newConf.SetActive (false);
        }

        hasConfettiEnded = true;
    }

    public bool HasConfettiEnded ()
    {
        return hasConfettiEnded;
    }
}