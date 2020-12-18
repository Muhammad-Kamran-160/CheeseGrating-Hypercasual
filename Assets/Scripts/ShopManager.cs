using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject[] usableGraterButtonsPage1;
    [SerializeField] GameObject[] usableGraterButtonsPage2;
    [SerializeField] List<GraterButton> graterButtonsScriptsPage1 = new List<GraterButton> ();
    [SerializeField] List<GraterButton> graterButtonsScriptsPage2 = new List<GraterButton> ();
    [SerializeField] List<GraterButton> lockedGratersPage1 = new List<GraterButton> ();
    [SerializeField] List<GraterButton> lockedGratersPage2 = new List<GraterButton> ();
    bool havePage1GraterBeenUnlocked = false;
    int l = 0;

    private void Start ()
    {
        PlayerPrefs.SetInt (GameConstants.graterUnlockPrefsPage1 + l, 1); // Unlock First Grater
        GetGraterButtonScripts ();
        CheckLocks ();
        CheckLocksOnPage1 ();
    }

    private void OnEnable ()
    {
        CheckLocksOnPage1 ();
    }

    void GetGraterButtonScripts ()
    {
        for (int i = 0; i < usableGraterButtonsPage1.Length; i++)
        {
            if (usableGraterButtonsPage1[i].GetComponent<GraterButton> () != null)
                graterButtonsScriptsPage1.Add (usableGraterButtonsPage1[i].GetComponent<GraterButton> ());
        }

        for (int i = 0; i < usableGraterButtonsPage2.Length; i++)
        {
            if (usableGraterButtonsPage2[i].GetComponent<GraterButton> () != null)
                graterButtonsScriptsPage2.Add (usableGraterButtonsPage2[i].GetComponent<GraterButton> ());
        }
    }

    void CheckLocks ()
    {
        ClearLockedGratersLists ();

        for (int i = 0; i < graterButtonsScriptsPage1.Count; i++)
        {
            if (PlayerPrefs.HasKey (GameConstants.graterUnlockPrefsPage1 + i))
            {
                if (PlayerPrefs.GetInt (GameConstants.graterUnlockPrefsPage1 + i) == 1)
                {
                    graterButtonsScriptsPage1[i].Unlock ();
                }
                else
                {
                    graterButtonsScriptsPage1[i].Lock ();
                    lockedGratersPage1.Add (graterButtonsScriptsPage1[i]);
                }
            }
            else
            {
                PlayerPrefs.SetInt (GameConstants.graterUnlockPrefsPage1 + i, 0); // Set Prefs and lock grater if prefs does not have key
                graterButtonsScriptsPage1[i].Lock ();
                lockedGratersPage1.Add (graterButtonsScriptsPage1[i]);
            }

            graterButtonsScriptsPage1[i].SetButtonIndex (i);
        }

        for (int i = 0; i < graterButtonsScriptsPage2.Count; i++)
        {
            if (PlayerPrefs.HasKey (GameConstants.graterUnlockPrefsPage2 + i))
            {
                if (PlayerPrefs.GetInt (GameConstants.graterUnlockPrefsPage2 + i) == 1)
                {
                    graterButtonsScriptsPage2[i].Unlock ();
                }
                else
                {
                    graterButtonsScriptsPage2[i].Lock ();
                    lockedGratersPage2.Add (graterButtonsScriptsPage2[i]);
                }
            }
            else
            {
                PlayerPrefs.SetInt (GameConstants.graterUnlockPrefsPage2 + i, 0); // Set Prefs and lock grater if prefs does not have key
                graterButtonsScriptsPage2[i].Lock ();
                lockedGratersPage2.Add (graterButtonsScriptsPage2[i]);
            }

            graterButtonsScriptsPage2[i].SetButtonIndex (i);
        }

        CheckAndSetGraterUnlockButtonActive();
    }

    public void CheckLocksOnPage1 ()
    {
        for (int i = 0; i < usableGraterButtonsPage1.Length; i++)
        {
            if (PlayerPrefs.HasKey (GameConstants.graterUnlockPrefsPage1 + i))
            {
                if (PlayerPrefs.GetInt (GameConstants.graterUnlockPrefsPage1 + i) == 0)
                {
                    havePage1GraterBeenUnlocked = false;
                    return;
                }
            }
        }

        havePage1GraterBeenUnlocked = true;
    }

    public void EnableSelectedButtonOutlinePage1 (int index)
    {
        for (int i = 0; i < graterButtonsScriptsPage1.Count; i++)
        {
            graterButtonsScriptsPage1[i].EnableOutline (false);
        }

        for (int i = 0; i < graterButtonsScriptsPage2.Count; i++)
        {
            graterButtonsScriptsPage2[i].EnableOutline (false);
        }

        graterButtonsScriptsPage1[index].EnableOutline (true);
    }

    public void EnableSelectedButtonOutlinePage2 (int index)
    {
        for (int i = 0; i < graterButtonsScriptsPage2.Count; i++)
        {
            graterButtonsScriptsPage2[i].EnableOutline (false);
        }

        for (int i = 0; i < graterButtonsScriptsPage1.Count; i++)
        {
            graterButtonsScriptsPage1[i].EnableOutline (false);
        }

        graterButtonsScriptsPage2[index].EnableOutline (true);
    }

    void EnableUnlockedButtons (bool val)
    {
        for (int i = 0; i < graterButtonsScriptsPage1.Count; i++)
        {
            if (graterButtonsScriptsPage1[i].IsGraterUnlocked ())
            {
                graterButtonsScriptsPage1[i].GetComponent<UnityEngine.UI.Button> ().interactable = val;
            }
        }

        for (int i = 0; i < graterButtonsScriptsPage2.Count; i++)
        {
            if (graterButtonsScriptsPage2[i].IsGraterUnlocked ())
            {
                graterButtonsScriptsPage2[i].GetComponent<UnityEngine.UI.Button> ().interactable = val;
            }
        }
    }

    public void CheckAndSetGraterUnlockButtonActive ()
    {
        if (lockedGratersPage1.Count <= 0 && lockedGratersPage2.Count <= 0)
        {
            Gameplay_References._instance._uiManager.SetGraterUnlockButtonActive (false);
        }
        else
        {
            Gameplay_References._instance._uiManager.SetGraterUnlockButtonActive (true);
        }
    }

    public bool HavePage1GraterBeenUnlocked ()
    {
        return havePage1GraterBeenUnlocked;
    }

    void ClearLockedGratersLists ()
    {
        lockedGratersPage1.Clear ();
        lockedGratersPage2.Clear ();
    }

    #region unlockGrater
    public void UnlockGrater ()
    {
        Gameplay_References._instance._uiManager.DisableGraterUnlockButton ();
        Gameplay_References._instance._uiManager.EnableInputBlocker (true);

        if (!GameConstants.testMode)
            Gameplay_References._instance._gameplayManger.AddCoins (-50);
        else
            Gameplay_References._instance._gameplayManger.AddCoins (0);

        EnableUnlockedButtons (false);
        OnPressUnlockGraterButton ();
    }

    private void OnPressUnlockGraterButton ()
    {
        StartCoroutine (UnlockGraterRandomly ());
    }

    [SerializeField] bool hasUnlockingStopped = false;
    float unlockTimer = 3f;
    private IEnumerator UnlockGraterRandomly ()
    {
        ClearLockedGratersLists ();
        CheckLocks ();
        CheckLocksOnPage1 ();

        if (!HavePage1GraterBeenUnlocked ())
        {
            if (lockedGratersPage1.Count <= 0)
                yield break;
            else if (lockedGratersPage1.Count == 1)
            {
                lockedGratersPage1[0].Unlock ();
                lockedGratersPage1[0].SelectGrater ();

                Gameplay_References._instance._uiManager.SetGraterUnlockButtonActive (false);
                Gameplay_References._instance._uiManager.EnableInputBlocker (false);
                if (!GameConstants.testMode)
                    Gameplay_References._instance._uiManager.EnableGraterUnlockButton ();
                else
                    Gameplay_References._instance._uiManager.EnableGraterUnlockButtonTestMode ();

                EnableUnlockedButtons (true);
                CheckLocks ();
                CheckLocksOnPage1 ();

                hasUnlockingStopped = false;
                yield break;
            }

            StartCoroutine (UnlockTimer ());

            unlockTimer = Random.Range (2f, 4f);

            GraterButton _graterToUnlock = null;
            while (!hasUnlockingStopped)
            {
                int rand = Random.Range (0, lockedGratersPage1.Count);
                int previousRand = rand;

                if (rand == previousRand)
                    rand++;

                if (rand >= lockedGratersPage1.Count)
                    rand = 0;

                previousRand = rand;

                for (int i = 0; i < lockedGratersPage1.Count; i++)
                {
                    lockedGratersPage1[i].EnableOutline (false);
                }

                lockedGratersPage1[rand].EnableOutline (true);

                _graterToUnlock = lockedGratersPage1[rand];

                yield return new WaitForSecondsRealtime (0.3f);
            }
            _graterToUnlock.Unlock ();
            _graterToUnlock.SelectGrater ();
        }
        else
        {
            if (lockedGratersPage2.Count <= 0)
                yield break;
            else if (lockedGratersPage2.Count == 1)
            {
                lockedGratersPage2[0].Unlock ();
                lockedGratersPage2[0].SelectGrater ();
                Gameplay_References._instance._uiManager.EnableInputBlocker (false);

                Gameplay_References._instance._uiManager.SetGraterUnlockButtonActive (false);
                Gameplay_References._instance._uiManager.EnableInputBlocker (false);
                if (!GameConstants.testMode)
                    Gameplay_References._instance._uiManager.EnableGraterUnlockButton ();
                else
                    Gameplay_References._instance._uiManager.EnableGraterUnlockButtonTestMode ();

                EnableUnlockedButtons (true);
                CheckLocks ();
                CheckLocksOnPage1 ();

                hasUnlockingStopped = false;

                yield break;
            }

            StartCoroutine (UnlockTimer ());

            unlockTimer = Random.Range (2f, 4f);

            GraterButton _graterToUnlock = null;
            while (!hasUnlockingStopped)
            {
                int rand = Random.Range (0, lockedGratersPage2.Count);
                int previousRand = rand;

                if (rand == previousRand)
                    rand++;

                if (rand >= lockedGratersPage2.Count)
                    rand = 0;

                previousRand = rand;

                for (int i = 0; i < lockedGratersPage2.Count; i++)
                {
                    lockedGratersPage2[i].EnableOutline (false);
                }

                lockedGratersPage2[rand].EnableOutline (true);

                _graterToUnlock = lockedGratersPage2[rand];

                yield return new WaitForSecondsRealtime (0.3f);
            }
            _graterToUnlock.Unlock ();
            _graterToUnlock.SelectGrater ();
        }

        hasUnlockingStopped = false;

        Gameplay_References._instance._uiManager.EnableInputBlocker (false);
        EnableUnlockedButtons (true);

        if (!GameConstants.testMode)
            Gameplay_References._instance._uiManager.EnableGraterUnlockButton ();
        else
            Gameplay_References._instance._uiManager.EnableGraterUnlockButtonTestMode ();
    }

    IEnumerator UnlockTimer ()
    {
        yield return new WaitForSecondsRealtime (3f);
        hasUnlockingStopped = true;
    }

    #endregion
}