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
        parameters.humanBulletSpeed = FloatField("скоростьѕулиѕротагониста", parameters.humanBulletSpeed);
        parameters.aiBulletSpeed = FloatField("скоростьѕули¬рага", parameters.aiBulletSpeed);
        parameters.bulletDestructionTime = FloatField("врем€”ничтожени€ѕули", parameters.bulletDestructionTime);
        parameters.attackDelay_Brainman = FloatField("задержкајтакиBrainman", parameters.attackDelay_Brainman);
        parameters.attackDelay_Lizard = FloatField("задержкајтакиLizard", parameters.attackDelay_Lizard);
        parameters.attackDelay_Octopus = FloatField("задержкајтакиOctopus", parameters.attackDelay_Octopus);

        parameters.initialHp = EditorGUILayout.IntField("начальное оличество—ердечек", parameters.initialHp);
        parameters.initialCountdownTime = EditorGUILayout.IntField("врем€ќтсчЄта", parameters.initialCountdownTime);
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