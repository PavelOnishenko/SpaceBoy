using System;
using UnityEditor;
using UnityEngine;

public class GameParametersEditor : EditorWindow
{
    private GameParameters parameters;

    [MenuItem("Window/Game Parameters")]
    public static void ShowWindow() => GetWindow<GameParametersEditor>("Game Parameters");

    private void OnGUI()
    {
        GUILayout.Label("Game Parameters", EditorStyles.boldLabel);

        parameters = (GameParameters)EditorGUILayout.ObjectField("Game Parameters", parameters, typeof(GameParameters), false);

        if (parameters != null)
        {
            parameters.humanBulletSpeed = FloatField("скоростьѕулиѕротагониста", () => parameters.humanBulletSpeed);
            parameters.aiBulletSpeed = FloatField("скоростьѕули¬рага", () => parameters.aiBulletSpeed);
            parameters.bulletDestructionTime = FloatField("врем€”ничтожени€ѕули", () => parameters.bulletDestructionTime);

            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(parameters);
                AssetDatabase.SaveAssets();
                UpdatePrefabs();
            }
        }
    }

    private void UpdatePrefabs()
    {
        var guids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            var bullet = prefab.GetComponent<BulletBehavior>();
            if (bullet != null)
            {
                bullet.ApplyParameters();
                EditorUtility.SetDirty(prefab);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private float FloatField(string labelText, Func<float> valueGetter) => EditorGUILayout.FloatField(labelText, valueGetter());
}