using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public void EnableVibration (bool val)
    {
        if (val)
        {
            // Debug.Log ("Vibration is enabled");
            PlayerPrefs.SetInt (GameConstants.enableVibrationPrefs, 1);
            GameConstants.isVibrationEnabled = true;
            Gameplay_References._instance._uiManager.EnableVibrationOffButton (false);
        }
        else
        {
            // Debug.Log ("Vibration is disabled");
            PlayerPrefs.SetInt (GameConstants.enableVibrationPrefs, 0);
            GameConstants.isVibrationEnabled = false;
            Gameplay_References._instance._uiManager.EnableVibrationOffButton (true);
        }
    }
    public void EnableSound (bool val)
    {
        if (val)
        {
            PlayerPrefs.SetInt (GameConstants.enableSoundPrefs, 1);
            GameConstants.isSoundEnabled = true;
            Gameplay_References._instance._uiManager.EnableSoundOffButton (false);
        }
        else
        {
            PlayerPrefs.SetInt (GameConstants.enableSoundPrefs, 0);
            GameConstants.isSoundEnabled = false;
            Gameplay_References._instance._uiManager.EnableSoundOffButton (true);
        }
    }

    public void CheckVibrationSetting ()
    {
        if (PlayerPrefs.HasKey (GameConstants.enableVibrationPrefs))
        {
            GameConstants.isVibrationEnabled = PlayerPrefs.GetInt (GameConstants.enableVibrationPrefs) == 1 ? true : false;

            if (GameConstants.isVibrationEnabled)
            {
                Gameplay_References._instance._uiManager.EnableVibrationOffButton (false);
            }
            else
            {
                Gameplay_References._instance._uiManager.EnableVibrationOffButton (true);
            }
        }
        else
        {
            PlayerPrefs.SetInt (GameConstants.enableVibrationPrefs, 1);
            GameConstants.isVibrationEnabled = true;
            // Debug.Log ("vibration: " + GameConstants.isVibrationEnabled);
            Gameplay_References._instance._uiManager.EnableVibrationOffButton (false);
        }
    }

    public void CheckSoundsSetting ()
    {
        if (PlayerPrefs.HasKey (GameConstants.enableSoundPrefs))
        {
            GameConstants.isSoundEnabled = PlayerPrefs.GetInt (GameConstants.enableSoundPrefs) == 1 ? true : false;

            if (GameConstants.isSoundEnabled)
            {
                Gameplay_References._instance._uiManager.EnableSoundOffButton (false);
            }
            else
            {
                Gameplay_References._instance._uiManager.EnableSoundOffButton (true);
            }
        }
        else
        {
            PlayerPrefs.SetInt (GameConstants.enableSoundPrefs, 1);
            GameConstants.isSoundEnabled = true;
            Gameplay_References._instance._uiManager.EnableSoundOffButton (false);
        }
    }

}