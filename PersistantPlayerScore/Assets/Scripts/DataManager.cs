using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance;

    private string currentPlayerName;
    private float lastPlayerScore;

    private LeaderBoardData leaderBoard;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadLeaderBoard();
        SortLeaderboard();
    }

    private string GetDataFilePath()
    {
        return Application.persistentDataPath + "/DataFile.text";
    }

    private void LoadLeaderBoard()
    {
        if (File.Exists(GetDataFilePath()))
        {
            leaderBoard = JsonUtility.FromJson<LeaderBoardData>(File.ReadAllText(GetDataFilePath()));
            lastPlayerScore = (float)leaderBoard.lastPlayerScore;
            Debug.Log("Leaderboards Loaded!");
        }
    }

    public float GetLastPlayerScore()
    {
        return lastPlayerScore;
    }

    public bool TryGetPlayerDataAt(int index, out string playerName, out float playerScore)
    {
        playerName = null;
        playerScore = 0;


        if(leaderBoard == null || leaderBoard.playersData.Count <= index)
        {
            return false;
        }

        PlayersData playersData = leaderBoard.playersData[index];

        playerName = playersData.playerName;
        playerScore = (float)playersData.score;
        return true;
    }


    private void SortLeaderboard()
    {
        if (leaderBoard == null) return;
        leaderBoard.playersData.Sort((p1, p2) => p2.score.CompareTo(p1.score));
    }

    public void SetPlayerName(string playerName)
    {
        currentPlayerName = playerName;
    }

    public string GetPlayerName()
    {
        return currentPlayerName;
    }

    public void SavePlayerScore(float score)
    {
        if (leaderBoard == null)
            leaderBoard = new LeaderBoardData();

        if (leaderBoard != null && leaderBoard.CheckPlayerExistance(currentPlayerName, out float bestFoundScore))
        {
            if(score > bestFoundScore)
            {
                leaderBoard.UpdatePlayerScore(currentPlayerName, score);
            }
        }
        else
        {
            PlayersData playerData = new PlayersData();
            playerData.playerName = currentPlayerName;
            playerData.score = score;
            leaderBoard.playersData.Add(playerData);
        }

        leaderBoard.lastPlayerScore = score;
        lastPlayerScore = score;

        SortLeaderboard();

        string jsonString = JsonUtility.ToJson(leaderBoard);

        File.WriteAllText(GetDataFilePath(), jsonString);
      
    }

    [System.Serializable]
    class LeaderBoardData
    {
        public List<PlayersData> playersData = new List<PlayersData>();

        public double lastPlayerScore;

        public bool CheckPlayerExistance(string playerName, out float foundPlayerScore)
        {
            foreach(PlayersData pd in playersData)
            {
                if (pd.playerName.Equals(playerName))
                {
                    foundPlayerScore = (float)pd.score;
                    return true;
                }
            }

            foundPlayerScore = 0;
            return false;

        }

        public void UpdatePlayerScore(string playerName, float newScore)
        {
            playersData.Find(x => x.playerName.Equals(playerName)).score = newScore;
        }
    }

    [System.Serializable]
    class PlayersData
    {
        public string playerName;
        public double score;
    }
}
