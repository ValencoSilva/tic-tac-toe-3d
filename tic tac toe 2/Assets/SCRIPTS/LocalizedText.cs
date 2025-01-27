using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string key;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        var textComponent = GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = LocalizationManager.Instance.GetTranslation(key);
        }
    }
}
