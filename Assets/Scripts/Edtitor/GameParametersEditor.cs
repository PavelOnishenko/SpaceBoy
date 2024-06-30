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
            FillParameters();
            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(parameters);
                AssetDatabase.SaveAssets();
                UpdatePrefabs();
            }
        }
    }

    private void FillParameters()
    {
        parameters.humanBulletSpeed = FloatField("������������������������", () => parameters.humanBulletSpeed);
        parameters.aiBulletSpeed = FloatField("�����������������", () => parameters.aiBulletSpeed);
        parameters.bulletDestructionTime = FloatField("��������������������", () => parameters.bulletDestructionTime);
        parameters.attackDelay_Brainman = FloatField("�������������Brainman", () => parameters.attackDelay_Brainman);
        parameters.attackDelay_Lizard = FloatField("�������������Lizard", () => parameters.attackDelay_Lizard);
        parameters.attackDelay_Octopus = FloatField("�������������Octopus", () => parameters.attackDelay_Octopus);
    }

    private void UpdatePrefabs()
    {
        var guids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            ApplyToBulletBehavior(prefab);
            ApplyToAi(prefab);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static void ApplyToAi(GameObject prefab)
    {
        if (prefab.TryGetComponent<Ai>(out var component))
        {
            component.ApplyParameters();
            EditorUtility.SetDirty(prefab);
        }
    }

    private static void ApplyToBulletBehavior(GameObject prefab)
    {
        if (prefab.TryGetComponent<BulletBehavior>(out var component))
        {
            component.ApplyParameters();
            EditorUtility.SetDirty(prefab);
        }
    }

    private float FloatField(string labelText, Func<float> valueGetter) => EditorGUILayout.FloatField(labelText, valueGetter());
}