using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardRecord : MonoBehaviour
{
    [SerializeField] TMP_Text playerNameText;
    [SerializeField] TMP_Text playerScoreText;

    public void InitializeRecord(string playerName, float playerScore)
    {
        playerNameText.text = playerName;
        playerScoreText.text = ": " + playerScore.ToString();
    }
}
