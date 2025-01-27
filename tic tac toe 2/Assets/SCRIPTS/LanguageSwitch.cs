using UnityEngine;

public class LanguageSwitch : MonoBehaviour
{
    [SerializeField] private GameObject painelLanguage;
    public void SwitchToPortuguese()
    {
        LocalizationManager.Instance.SetLanguage("pt");
        UpdateAllTexts();
        painelLanguage.SetActive(false);
    }

    public void SwitchToEnglish()
    {
        LocalizationManager.Instance.SetLanguage("en");
        UpdateAllTexts();
        painelLanguage.SetActive(false);
    }

    private void UpdateAllTexts()
    {
        var localizedTexts = FindObjectsOfType<LocalizedText>();
        foreach (var localizedText in localizedTexts)
        {
            localizedText.UpdateText();
        }
    }
}
