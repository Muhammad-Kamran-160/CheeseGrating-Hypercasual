using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header ("Panels")]
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject graterCollectionPanel;
    [SerializeField] GameObject loadingNextLevelPanel;

    [Header ("scripts")]
    [SerializeField] SwipeMenu _swipeMenu;

    [Header ("Grater Collection Screen")]
    [SerializeField] GameObject leftDisablerArrow;
    [SerializeField] GameObject rightDisablerArrow;

    [Header ("Texts")]
    [SerializeField] Text levelText;
    [SerializeField] Text coinsText;
    [SerializeField] Text graterCollectionCoinsText;
    [SerializeField] Text exclamationText;

    [Header ("Images")]
    [SerializeField] Image levelFiller;
    [SerializeField] Image inputBlocker;

    [Header ("Buttons")]
    [SerializeField] Button randomGraterUnlockButton;
    [SerializeField] Button _soundOffButton;
    [SerializeField] Button _vibrationOffButton;

    public void OpenSettings (bool val)
    {
        Time.timeScale = val ? 0 : 1f;
        settingsPanel.SetActive (val);
    }

    public void OpenGraterCollection (bool val)
    {
        Time.timeScale = val ? 0 : 1f;
        graterCollectionPanel.SetActive (val);

        if (!GameConstants.testMode)
            EnableGraterUnlockButton ();
        else
            EnableGraterUnlockButtonTestMode ();
    }

    public void ScrollGraterCollection (float val)
    {
        _swipeMenu.SetScrollPos (val);
    }

    public void EnableLeftDisablerArrow (bool val)
    {
        leftDisablerArrow.SetActive (val);
    }

    public void EnableRightDisablerArrow (bool val)
    {
        rightDisablerArrow.SetActive (val);
    }

    public void EnableSoundOffButton (bool val)
    {
        _soundOffButton.gameObject.SetActive (val);
    }

    public void EnableVibrationOffButton (bool val)
    {
        _vibrationOffButton.gameObject.SetActive (val);
    }
    public void SetLevelText ()
    {
        levelText.text = "Level " + GameConstants.LevelNumber;
    }

    public void SetAndEnableExclamationText(string str)
    {
        exclamationText.text = str;
        exclamationText.gameObject.SetActive(true);
    }

    public void DisableExclamationText()
    {
        exclamationText.gameObject.SetActive(false);
    }

    public void SetCoinsText ()
    {
        coinsText.text = GameConstants.totalCoinsEarned.ToString ();
        graterCollectionCoinsText.text = GameConstants.totalCoinsEarned.ToString ();
    }

    public void EnableRandomGraterUnlockButton (bool val)
    {
        randomGraterUnlockButton.gameObject.SetActive (val);
    }

    public void EnableNextLevelPanel ()
    {
        loadingNextLevelPanel.SetActive (true);
    }

    public void SetLevelFillerValue (float val)
    {
        levelFiller.fillAmount = val;
    }

    public void SetGraterUnlockButtonActive(bool val)
    {
        randomGraterUnlockButton.gameObject.SetActive(val);
    }

    public void EnableGraterUnlockButton ()
    {
        Debug.Log("EnableGraterUnlockButton");
        if (GameConstants.totalCoinsEarned < 50)
            randomGraterUnlockButton.interactable = false;
        else if (GameConstants.totalCoinsEarned >= 50)
            randomGraterUnlockButton.interactable = true;
    }

    public void EnableGraterUnlockButtonTestMode ()
    {
        Debug.Log("EnableGraterUnlockButtonTestMode");

        randomGraterUnlockButton.interactable = true;
    }

    public void DisableGraterUnlockButton ()
    {
        randomGraterUnlockButton.interactable = false;
    }

    public void EnableInputBlocker (bool val)
    {
        inputBlocker.gameObject.SetActive (val);
    }
}