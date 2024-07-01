using Assets.Scripts.Edtitor;
using System.Linq;
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
            FillParametersFromInputs();
            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(parameters);
                AssetDatabase.SaveAssets();
                UpdatePrefabs();
            }
        }
    }

    private void FillParametersFromInputs()
    {
        parameters.humanBulletSpeed = FloatField("������������������������", parameters.humanBulletSpeed);
        parameters.aiBulletSpeed = FloatField("�����������������", parameters.aiBulletSpeed);
        parameters.bulletDestructionTime = FloatField("��������������������", parameters.bulletDestructionTime);
        parameters.attackDelay_Brainman = FloatField("�������������Brainman", parameters.attackDelay_Brainman);
        parameters.attackDelay_Lizard = FloatField("�������������Lizard", parameters.attackDelay_Lizard);
        parameters.attackDelay_Octopus = FloatField("�������������Octopus", parameters.attackDelay_Octopus);

        parameters.initialHp = EditorGUILayout.IntField("���������������������������", parameters.initialHp);
        parameters.initialCountdownTime = EditorGUILayout.IntField("������������", parameters.initialCountdownTime);
    }

    private void UpdatePrefabs()
    {
        var guids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            ApplyTo<BulletBehavior>(prefab);
            ApplyTo<Ai>(prefab);
            ApplyTo<CharacterState>(prefab);
        }
        
        var gos = Resources.FindObjectsOfTypeAll<GameObject>().Where(go => go.scene.isLoaded).ToArray();
        foreach (var go in gos) ApplyTo<Countdown>(go);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static void ApplyTo<T>(GameObject prefab) where T : MonoBehaviour, IDesignerConfigurable
    {
        if (prefab.TryGetComponent<T>(out var component))
        {
            component.ApplyParameters();
            EditorUtility.SetDirty(prefab);
        }
    }

    private float FloatField(string labelText, float theValue) => EditorGUILayout.FloatField(labelText, theValue);
}