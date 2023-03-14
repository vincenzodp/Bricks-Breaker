using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameIF;

    [SerializeField] TMP_Text lastPlayerScoreText;

    private void Start()
    {
        lastPlayerScoreText.text = DataManager.Instance.GetLastPlayerScore().ToString();
    }

    public void StartGame()
    {
        DataManager.Instance.SetPlayerName(playerNameIF.text);
        SceneManager.LoadScene(1);
    }

    public void EnableButton(Button buttonToEnable)
    {
        buttonToEnable.interactable = true;
    }
}
