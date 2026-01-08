using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneOpenerWindow : EditorWindow
{
    [MenuItem("Tools/Scene Opener")]
    public static void ShowWindow()
    {
        GetWindow<SceneOpenerWindow>("Scene Opener");
    }

    private void OnGUI()
    {
        GUILayout.Label("Scenes In Build Settings", EditorStyles.boldLabel);
        GUILayout.Space(5);

        var scenes = EditorBuildSettings.scenes;

        if (scenes.Length == 0)
        {
            EditorGUILayout.HelpBox("No scenes found in Build Settings.", MessageType.Warning);
            return;
        }

        foreach (var scene in scenes)
        {
            if (!scene.enabled) continue;

            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);

            if (GUILayout.Button(sceneName, GUILayout.Height(25)))
            {
                OpenScene(scene.path);
            }


        }

        if (GUILayout.Button("Clear PlayerPref", GUILayout.Height(25)))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void OpenScene(string scenePath)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenePath);
        }
    }
}
