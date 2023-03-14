using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardsManager : MonoBehaviour
{
    [SerializeField] int leaderboardRecordsShown = 3;
    
    [SerializeField] GameObject scoreRecordPrefab;
    [SerializeField] Transform leaderBoardContainer;



    private void Start()
    {
        float yOffset = 0;
        for(int i = 0; i < leaderboardRecordsShown; i++)
        {

            if (DataManager.Instance.TryGetPlayerDataAt(i, out string playerName, out float playerScore))
            {
                LeaderBoardRecord leaderBoardRecord = Instantiate(scoreRecordPrefab, leaderBoardContainer).GetComponent<LeaderBoardRecord>();
                leaderBoardRecord.InitializeRecord(playerName, playerScore);

                leaderBoardRecord.transform.position = new Vector3(
                    leaderBoardRecord.transform.position.x, 
                    leaderBoardRecord.transform.position.y - yOffset, 
                    leaderBoardRecord.transform.position.z);

                yOffset += 25;
            }
            else
                break;


        }
    }



}
