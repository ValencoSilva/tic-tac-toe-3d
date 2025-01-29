using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    private Dictionary<string, Dictionary<string, string>> localizedText;
    private string currentLanguage = "en"; // Default to English

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadLocalizationData();
        }
    }

    private void LoadLocalizationData()
    {
        localizedText = new Dictionary<string, Dictionary<string, string>>();

        // Load the JSON file
        TextAsset jsonFile = Resources.Load<TextAsset>("translations");
        if (jsonFile == null)
        {
            Debug.LogError("Failed to load translations.json from Resources folder.");
            return;
        }

        // Parse the JSON data
        LocalizationData jsonData = JsonUtility.FromJson<LocalizationData>(jsonFile.text);
        if (jsonData.languages == null || jsonData.languages.Count == 0)
        {
            Debug.LogError("No languages found in the JSON file.");
            return;
        }

        foreach (var langData in jsonData.languages)
        {
            localizedText[langData.languageCode] = new Dictionary<string, string>();
            foreach (var entry in langData.entries)
            {
                if (localizedText[langData.languageCode].ContainsKey(entry.key))
                {
                    Debug.LogWarning($"Duplicate key '{entry.key}' in language '{langData.languageCode}'.");
                    continue;
                }
                localizedText[langData.languageCode][entry.key] = entry.value;
            }
        }

        Debug.Log("Localization data loaded successfully.");
    }


    public void SetLanguage(string languageCode)
    {
        if (localizedText.ContainsKey(languageCode))
        {
            currentLanguage = languageCode;
        }
        else
        {
            Debug.LogError($"Language {languageCode} not found!");
        }
    }

    public string GetTranslation(string key)
    {
        if (localizedText[currentLanguage].ContainsKey(key))
        {
            return localizedText[currentLanguage][key];
        }
        return $"Missing Translation: {key}";
    }
}

[System.Serializable]
public class LocalizationData
{
    public List<LanguageData> languages;
}

[System.Serializable]
public class LanguageData
{
    public string languageCode;
    public List<LocalizationEntry> entries;
}

[System.Serializable]
public class LocalizationEntry
{
    public string key;
    public string value;
}
