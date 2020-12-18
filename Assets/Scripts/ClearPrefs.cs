#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ClearPrefs
{
    [MenuItem ("Tools/Clear Player Prefs")]
    public static void ClearPlayerPrefs ()
    {
        PlayerPrefs.DeleteAll();
    }
}
#endif