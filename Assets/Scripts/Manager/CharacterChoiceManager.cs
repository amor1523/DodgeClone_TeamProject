using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterChoiceManager : MonoBehaviour
{
    [SerializeField] private Button Giwoong;
    [SerializeField] private Button JiYoon;
    [SerializeField] private Button Jihyo;
    [SerializeField] private Button Sunho;

    void Start()
    {
        Giwoong.onClick.AddListener(() => CharacterChoiceBtn("Giwoong"));
        JiYoon.onClick.AddListener(() => CharacterChoiceBtn("JiYoon"));
        Jihyo.onClick.AddListener(() => CharacterChoiceBtn("Jihyo"));
        Sunho.onClick.AddListener(() => CharacterChoiceBtn("Sunho"));
    }

    public void CharacterChoiceBtn(string characterName)
    {
        PlayerPrefs.SetString("setPlayer", characterName);
        SceneManager.LoadScene("MainScene");
        Debug.Log(characterName);
    }
}
