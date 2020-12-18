using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header ("Cheese")]
    [SerializeField] GameObject[] allCheese;
    [SerializeField] GameObject currentCheese;
    // [Header ("Knifes")]
    List<GameObject> knifes = new List<GameObject> ();

    [Header ("Graters")]
    [SerializeField] GameObject[] graters;

    [Header ("Level Complete")]
    [SerializeField] GameObject LevelCompeleteAnimation;

    [SerializeField] string[] exclamationTexts;
    bool isExclamationTextActive;

    public bool testCheese;
    public int testCheeseIndex;

    Vector3 blockOriginalPosition;
    public bool CanBeRelocatedNow
    {
        get;
        set;
    }

    public bool testMode = false;

    private void Awake ()
    {
        Gameplay_References._instance._settingsManager.CheckVibrationSetting ();
        Gameplay_References._instance._settingsManager.CheckSoundsSetting ();
        GameConstants.testMode = testMode;
        isExclamationTextActive = false;
    }

    // [SerializeField] GameObject restartButton;

    private void Start ()
    {
        if (PlayerPrefs.HasKey (GameConstants.levelNumberPrefs))
            GameConstants.LevelNumber = PlayerPrefs.GetInt (GameConstants.levelNumberPrefs);
        else
        {
            GameConstants.LevelNumber = 1;
            PlayerPrefs.SetInt (GameConstants.levelNumberPrefs, GameConstants.LevelNumber);
        }

        CanBeRelocatedNow = false;
        Gameplay_References._instance._uiManager.SetLevelText ();

        if (PlayerPrefs.HasKey (GameConstants.lastSelectedGraterPrefs))
            SelectGrater (PlayerPrefs.GetInt (GameConstants.lastSelectedGraterPrefs));
        else
        {
            PlayerPrefs.SetInt (GameConstants.lastSelectedGraterPrefs, 0);
            SelectGrater (0);
        }

        EnableRandomCheese ();

        CheckAndSetCoinsValue ();
    }

    public void SetCheeseShred (GameObject shred)
    {
        ObjectPooler.SharedInstance.AddObject (shred, 40);
    }

    void EnableRandomCheese ()
    {
        int rand = 0;

        if (GameConstants.LevelNumber == 1)
        {
            rand = 7;
        }
        else
        {
            rand = Random.Range (0, allCheese.Length);
        }

        if (testCheese)
        {
            rand = testCheeseIndex;
        }

        allCheese[rand].SetActive (true);
    }

    public void SetCurrentCheese (GameObject newBlock)
    {
        currentCheese = newBlock;
        blockOriginalPosition = currentCheese.transform.position;
    }

    // public void RelocateCheeseBlock ()
    // {
    //     if (CanBeRelocatedNow)
    //     {
    //         cheeseBlock.transform.position = blockOriginalPosition;
    //         CanBeRelocatedNow = false;
    //         EnableKnifes ();
    //         cutCount = 0;
    //     }
    // }

    public void RelocateToOriginalPosition ()
    {
        // if (!CanBeRelocatedNow)
        // {
        currentCheese.transform.position = blockOriginalPosition;
        EnableKnifes ();
        // }
    }

    // public void EnableRestartButton ()
    // {
    //     restartButton.SetActive (true);
    // }

    bool isLoadNextLevelCalled = false;

    public void StartNextLevel ()
    {
        StartCoroutine (LoadNextLevel ());
    }

    IEnumerator LoadNextLevel ()
    {
        if (isLoadNextLevelCalled)
            yield break;

        Gameplay_References._instance._uiManager.EnableInputBlocker (true);

        isLoadNextLevelCalled = true;
        Gameplay_References._instance._meshSlicesTracker.RemoveAllPieces ();
        CheeseHandler._instance.StopMovingCheese ();
        Gameplay_References._instance._confettiManager.PlayConfetti ();
        Gameplay_References._instance._soundManager.PlaySoundEffectOneShot (1);
        Gameplay_References._instance._uiManager.DisableExclamationText();
        yield return new WaitForSeconds (0.1f);
        Gameplay_References._instance._uiManager.SetLevelFillerValue (1f);
        yield return new WaitUntil (() => Gameplay_References._instance._confettiManager.HasConfettiEnded () == true);
        LevelCompeleteAnimation.SetActive (true);
        AddCoins (10);
        GameConstants.LevelNumber++;
        PlayerPrefs.SetInt (GameConstants.levelNumberPrefs, GameConstants.LevelNumber);
        yield return new WaitForSeconds (3f);
        Gameplay_References._instance._uiManager.EnableNextLevelPanel ();
        UnityAdsManager._instance.ShowInterstitialAd ();
        yield return new WaitForSeconds (3f);

        Restart ();
    }

    public void Restart ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex);
    }

    public void EnableKnifes ()
    {
        for (int i = 0; i < knifes.Count; i++)
        {
            knifes[i].SetActive (true);
            // knifes[i].GetComponent<Knife>().ResetCount();
        }
    }

    public void AddKnife (GameObject knife)
    {
        knifes.Add (knife);
    }

    public void SelectGrater (int index)
    {
        for (int i = 0; i < graters.Length; i++)
        {
            graters[i].SetActive (false);
        }

        graters[index].SetActive (true);
    }

    public void AddCoins (int val)
    {
        GameConstants.totalCoinsEarned += val;
        PlayerPrefs.SetInt (GameConstants.totalCoinsEarnedPrefs, GameConstants.totalCoinsEarned);

        Gameplay_References._instance._uiManager.SetCoinsText ();
    }

    public void CheckAndSetCoinsValue ()
    {
        if (PlayerPrefs.HasKey (GameConstants.totalCoinsEarnedPrefs))
        {
            GameConstants.totalCoinsEarned = PlayerPrefs.GetInt (GameConstants.totalCoinsEarnedPrefs);
        }
        else
        {
            GameConstants.totalCoinsEarned = 0;
            PlayerPrefs.SetInt (GameConstants.totalCoinsEarnedPrefs, 0);
        }

        Gameplay_References._instance._uiManager.SetCoinsText ();
    }

    public void EnableExclamationText ()
    {
        if (!isExclamationTextActive)
        {
            isExclamationTextActive = true;
            StartCoroutine(SetExclamationTextIsActive());
            Gameplay_References._instance._uiManager.SetAndEnableExclamationText (exclamationTexts[Random.Range (0, exclamationTexts.Length)]);
        }
    }
    
    IEnumerator SetExclamationTextIsActive()
    {
        yield return new WaitForSeconds(1f);

        isExclamationTextActive = false;
    }

    public void DisableExclamationText()
    {

    }

    // int cutCount = 0;

    // public void CheckKnifeCutCount ()
    // {
    //     cutCount++;

    //     Debug.Log ("cutcount: " + cutCount);

    //     if (cutCount >= knifes.Length)
    //     {
    //         Debug.Log ("Can relocate now");

    //         CanBeRelocatedNow = true;
    //     }
    // }
}