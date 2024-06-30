using UnityEditor;
using UnityEngine;

public class GameParametersEditor : EditorWindow
{
    private GameParameters gameParameters;

    [MenuItem("Window/Game Parameters")]
    public static void ShowWindow()
    {
        GetWindow<GameParametersEditor>("Game Parameters");
    }

    private void OnGUI()
    {
        GUILayout.Label("Game Parameters", EditorStyles.boldLabel);

        gameParameters = (GameParameters)EditorGUILayout.ObjectField("Game Parameters", gameParameters, typeof(GameParameters), false);

        if (gameParameters != null)
        {
            gameParameters.humanBulletSpeed = EditorGUILayout.FloatField(
                "скоростьѕулиѕротагониста", gameParameters.humanBulletSpeed);
            gameParameters.aiBulletSpeed = EditorGUILayout.FloatField("скоростьѕули¬рага", gameParameters.aiBulletSpeed);
            gameParameters.bulletDestructionTime = EditorGUILayout.FloatField(
                "врем€ уничтожение пули", gameParameters.bulletDestructionTime);

            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(gameParameters);
                AssetDatabase.SaveAssets();
                UpdatePrefabs();
            }
        }
    }

    private void UpdatePrefabs()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            BulletBehavior bullet = prefab.GetComponent<BulletBehavior>();
            if (bullet != null)
            {
                bullet.ApplyParameters();
                EditorUtility.SetDirty(prefab);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}