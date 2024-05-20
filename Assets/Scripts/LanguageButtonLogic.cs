using UnityEngine;

public class LanguageButtonLogic : MonoBehaviour
{
    public void ChangeLanguage(string targetLanguage) { LocalizationManager.Instance.ChangeLanguage(targetLanguage); }
}
