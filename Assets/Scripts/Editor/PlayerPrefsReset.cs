using UnityEditor;
using UnityEngine;

public class PlayerPrefsReset
{
    [MenuItem("Tools/Reset PlayerPrefs")]
    public static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs have been reset.");
    }
}