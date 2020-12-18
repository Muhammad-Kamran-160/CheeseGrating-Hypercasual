using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class GraterButton : MonoBehaviour
{
    public enum Page
    {
        page1,
        page2
    }

    public Page _page;
    [SerializeField] Button _button;
    [SerializeField] GameObject graterImage;
    [SerializeField] GameObject questionMark;
    [SerializeField] GameObject outline;
    int index;

    bool isUnlocked = false;
    public void EnableQuestionMark (bool val)
    {
        questionMark.SetActive (val);
    }

    public void EnableOutline (bool val)
    {
        outline.SetActive (val);
    }

    public void EnableGraterImage (bool val)
    {
        graterImage.SetActive (val);
    }

    public void Lock ()
    {
        _button.interactable = false;
        EnableQuestionMark (true);
        EnableGraterImage (false);
    }

    public void Unlock ()
    {
        _button.interactable = true;
        EnableQuestionMark (false);
        EnableGraterImage (true);

        if (_page == Page.page1)
            PlayerPrefs.SetInt (GameConstants.graterUnlockPrefsPage1 + index, 1);
        else
            PlayerPrefs.SetInt (GameConstants.graterUnlockPrefsPage2 + index, 1);

        isUnlocked = true;
    }

    public void SelectGrater ()
    {
        if (_page == Page.page1)
        {
            Gameplay_References._instance._shopManager.EnableSelectedButtonOutlinePage1 (index);
            Gameplay_References._instance._gameplayManger.SelectGrater (index);
        }
        else
        {
            Gameplay_References._instance._shopManager.EnableSelectedButtonOutlinePage2 (index);
            Gameplay_References._instance._gameplayManger.SelectGrater (9 + index); // using 9 here because there are 9 graters on page 1 on selection (0 - 8).
                                                                                    // So selecting graters on page 2 should be after the last index of page 1 i.e. 8.
        }

        PlayerPrefs.SetInt (GameConstants.lastSelectedGraterPrefs, index);
    }

    public void SetButtonIndex (int b_Index)
    {
        index = b_Index;
    }

    public bool IsGraterUnlocked ()
    {
        return isUnlocked;
    }
}