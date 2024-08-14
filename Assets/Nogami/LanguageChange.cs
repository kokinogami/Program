using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageChange : MonoBehaviour
{
    [SerializeField] List<TextMeshLanguage> textMeshLanguages;
    [SerializeField] List<TextLanguage> textLanguages;

    [SerializeField]bool L;
    [SerializeField]bool M;
    public void JapaneseChange()
    {
        GameManager.language = GameManager.Language.Japanease;
        changeLanguage();
    }
    public void EnglishChange()
    {
        GameManager.language = GameManager.Language.English;
        changeLanguage();
    }

    private void changeLanguage()
    {
        for (int i = 0; i < textMeshLanguages.Count; i++)
        {
            Debug.Log("aaa");
            textMeshLanguages[i].changeLanguage();
        }
        for (int i = 0; i < textLanguages.Count; i++)
        {
            Debug.Log("aa");
            textLanguages[i].Language();
        }
    }
}
