﻿using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;
using System.Linq;

namespace Assets.Scripts.Editor
{
    public class FixShootAnimationWindow : EditorWindow
    {
        [MenuItem("Tools/Fix Shoot Animations")]
        public static void ShowWindow() => GetWindow<FixShootAnimationWindow>("Fix Shoot Animations");

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Fix Shoot Animations", EditorStyles.boldLabel);
            if (GUILayout.Button("Fix Shoot Animations"))
                FixShootAnimationsInAllFolders();
        }

        private void FixShootAnimationsInAllFolders()
        {
            var characterFolders = Directory.GetDirectories("Assets/Animations");
            foreach (string folderPath in characterFolders)
                FixShootAnimationInFolder(folderPath);
            Debug.Log("Shoot animations fixed where missing.");
        }

        private void FixShootAnimationInFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Debug.LogError($"Folder path does not exist: {folderPath}");
                return;
            }

            var controllerPath = Directory.GetFiles(folderPath, "*.controller", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (string.IsNullOrEmpty(controllerPath))
            {
                Debug.LogError($"No Animator Controller found in folder: {folderPath}");
                return;
            }

            var controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(controllerPath);
            if (controller == null)
            {
                Debug.LogError($"Could not load Animator Controller at path: {controllerPath}");
                return;
            }

            foreach (AnimatorControllerLayer layer in controller.layers)
                foreach (ChildAnimatorState state in layer.stateMachine.states)
                    if (state.state.name.Contains("Shoot"))
                    {
                        var animationPath = Directory.GetFiles(folderPath, "*Shoot*.anim", SearchOption.TopDirectoryOnly).FirstOrDefault();
                        if (!string.IsNullOrEmpty(animationPath))
                        {
                            AnimationClip shootClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(animationPath);
                            if (shootClip != null)
                            {
                                state.state.motion = shootClip;
                                Debug.Log($"Assigned '{shootClip.name}' to state '{state.state.name}' in controller '{controller.name}'");
                            }
                            else
                            {
                                Debug.LogWarning($"Could not load animation clip at path: {animationPath}");
                            }
                        }
                        else
                        {
                            Debug.LogWarning($"No 'Shoot' animation found in folder: {folderPath}");
                        }
                    }

            // Mark the controller as dirty so changes are saved
            EditorUtility.SetDirty(controller);

            // Save all changes to the assets
            AssetDatabase.SaveAssets();
        }
    }
}