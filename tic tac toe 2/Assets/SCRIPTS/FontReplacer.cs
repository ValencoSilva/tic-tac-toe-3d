using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;

public class FontReplacer : EditorWindow
{
    public Font targetFont; // Font A
    public Font replacementFont; // Font B

    [MenuItem("Tools/Replace Fonts in All Scenes")]
    public static void ShowWindow()
    {
        GetWindow<FontReplacer>("Font Replacer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Replace Fonts in All Scenes", EditorStyles.boldLabel);

        targetFont = (Font)EditorGUILayout.ObjectField("Target Font (A):", targetFont, typeof(Font), false);
        replacementFont = (Font)EditorGUILayout.ObjectField("Replacement Font (B):", replacementFont, typeof(Font), false);

        if (GUILayout.Button("Replace Fonts"))
        {
            if (targetFont == null || replacementFont == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign both Target Font (A) and Replacement Font (B).", "OK");
                return;
            }

            ReplaceFontsInAllScenes();
        }
    }

    private void ReplaceFontsInAllScenes()
    {
        // Get all scenes in the build settings
        string[] scenePaths = EditorBuildSettings.scenes.Select(s => s.path).ToArray();

        int totalReplaced = 0;

        foreach (string scenePath in scenePaths)
        {
            // Open the scene
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

            // Replace fonts in the active scene
            Text[] textComponents = Resources.FindObjectsOfTypeAll<Text>();
            int replacedCount = 0;

            foreach (Text text in textComponents)
            {
                if (text.font == targetFont)
                {
                    Undo.RecordObject(text, "Replace Font");
                    text.font = replacementFont;
                    EditorUtility.SetDirty(text);
                    replacedCount++;
                }
            }

            totalReplaced += replacedCount;

            // Save the scene
            if (replacedCount > 0)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            }

            Debug.Log($"Replaced {replacedCount} fonts in scene: {scenePath}");
        }

        Debug.Log($"Font replacement complete. Total replaced: {totalReplaced}");
        EditorUtility.DisplayDialog("Font Replacement Complete", $"Replaced {totalReplaced} fonts in all scenes.", "OK");
    }
}
