using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SpriteReplacer : EditorWindow
{
    private Dictionary<string, Sprite> oldSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> newSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> spriteMapping = new Dictionary<string, Sprite>();
    private string oldSpritesFolderPath = "Assets/DesignVelho";
    private string newSpritesFolderPath = "Assets/DesingLakers";

    [MenuItem("Tools/Sprite Replacer")]
    public static void ShowWindow()
    {
        GetWindow<SpriteReplacer>("Sprite Replacer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Replace Sprites in All Scenes", EditorStyles.boldLabel);

        if (GUILayout.Button("Load Sprite Mapping"))
        {
            LoadSpriteMapping();
        }

        if (GUILayout.Button("Replace Sprites"))
        {
            ReplaceAllSprites();
        }
    }

    private void LoadSpriteMapping()
    {
        spriteMapping.Clear();
        oldSprites = LoadSprites(oldSpritesFolderPath);
        newSprites = LoadSprites(newSpritesFolderPath);

        foreach (var oldSprite in oldSprites)
        {
            if (newSprites.ContainsKey(oldSprite.Key))
            {
                spriteMapping[oldSprite.Key] = newSprites[oldSprite.Key];
                Debug.Log($"Mapped {oldSprite.Key} to {newSprites[oldSprite.Key].name}");
            }
            else
            {
                Debug.LogWarning($"No matching new sprite found for {oldSprite.Key}");
            }
        }

        Debug.Log("Sprite mapping loaded.");
    }

    private Dictionary<string, Sprite> LoadSprites(string folderPath)
    {
        Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            if (sprite != null)
            {
                sprites[sprite.name] = sprite;
                Debug.Log($"Loaded sprite: {sprite.name} from {assetPath}");
            }
            else
            {
                Debug.LogWarning($"Failed to load sprite at {assetPath}");
            }
        }

        return sprites;
    }

    private void ReplaceAllSprites()
    {
        string[] allScenes = AssetDatabase.FindAssets("t:Scene");
        foreach (string sceneGuid in allScenes)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
            Debug.Log($"Opening scene: {scenePath}");
            Scene openedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

            if (openedScene.isLoaded)
            {
                Debug.Log($"Scene {scenePath} loaded successfully.");
            }
            else
            {
                Debug.LogError($"Failed to load scene {scenePath}.");
                continue;
            }

            Image[] imageComponents = FindObjectsOfType<Image>();
            Debug.Log($"Found {imageComponents.Length} Image components in the scene.");

            foreach (Image imageComponent in imageComponents)
            {
                if (imageComponent.sprite != null)
                {
                    string oldSpriteName = imageComponent.sprite.name;
                    Debug.Log($"Image component on {imageComponent.gameObject.name} has sprite {oldSpriteName}");

                    if (spriteMapping.ContainsKey(oldSpriteName))
                    {
                        Sprite newSprite = spriteMapping[oldSpriteName];
                        Debug.Log($"Replacing sprite for {imageComponent.gameObject.name} from {oldSpriteName} to {newSprite.name}");
                        imageComponent.sprite = newSprite;
                        EditorUtility.SetDirty(imageComponent);
                    }
                    else
                    {
                        Debug.LogWarning($"No mapping found for {oldSpriteName} in {imageComponent.gameObject.name}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Image component on {imageComponent.gameObject.name} has no sprite assigned");
                }
            }

            bool saveResult = EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            Debug.Log(saveResult ? $"Scene {scenePath} saved successfully." : $"Failed to save scene {scenePath}.");
        }
        Debug.Log("Sprites replaced in all scenes.");
    }
}