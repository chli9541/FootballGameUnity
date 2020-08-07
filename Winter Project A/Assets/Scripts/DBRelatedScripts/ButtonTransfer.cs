using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTransfer : MonoBehaviour
{
    Button myBtn;



    void Start()
    {
        myBtn = GetComponent<Button>();
        myBtn.onClick.AddListener(delegate
        {

            StreamingDatabaseManager.UpdateNull();//Fixed Database Null bug, not necessary if database does not have null values.
            int TeamIndex = 4;//just using team 4 as an example, later you can change to any team.
            int TeamWealth = StreamingDatabaseManager.GetTeamWealth(TeamIndex);

            List<Player> TeamPlayers = StreamingDatabaseManager.GetPlayersFromTeam(TeamIndex);
            TransferSystem.GetChinesePlayersInTeam(TeamPlayers); //This step is very important to include: right now we only make Chinese Players transfer system

            List<Player> LeaguePlayers = StreamingDatabaseManager.SearchChinesePlayersFromSameLeague(TeamIndex);


            Dictionary<string, float> LeagueAvg = new Dictionary<string, float>();
            Dictionary<string, float> LeaguePositionCount = new Dictionary<string, float>();
            Dictionary<string, float> TeamAvg = new Dictionary<string, float>();
            Dictionary<string, float> TeamPositionCount = new Dictionary<string, float>();

            TransferSystem.CalculateLeagueAvg(LeagueAvg, LeaguePositionCount, LeaguePlayers);
            TransferSystem.CalculateLeagueAvg(TeamAvg, TeamPositionCount, TeamPlayers);
            
            

            List<Player> AllChinesePlayers = StreamingDatabaseManager.GetAllTradableChinesePlayers();

            List<string> PositionsNeeded = TransferSystem.CompareTeamAvgAndLeagueAvg(LeagueAvg, TeamAvg);
            Dictionary<string, Player> TypeOneTransfer = TransferSystem.TypeOnePlayers(PositionsNeeded, AllChinesePlayers, LeagueAvg, TeamAvg, TeamWealth);
            Dictionary<string, float> TypeTwoPositionsNeeded = TransferSystem.ComparePositionAvgAndTeamAvg(TeamPlayers, TeamAvg, TeamPositionCount);
            Dictionary<string, float> TypeThreePositionsNeeded = TransferSystem.FindPositionsLackingInTheTeam(TeamPositionCount,LeagueAvg);
            Dictionary<string, Player> TypeThreePlayersNeeded = TransferSystem.TypeTwoAndThreePlayers(TypeThreePositionsNeeded, AllChinesePlayers, TeamWealth);
            Dictionary<string, Player> TypeTwoPlayersNeeded = TransferSystem.TypeTwoAndThreePlayers(TypeTwoPositionsNeeded, AllChinesePlayers, TeamWealth);
            /*
            foreach (KeyValuePair<string, Player> Pair in TypeThreePlayersNeeded) {
                Debug.Log(Pair.Key);
                Debug.Log(Pair.Value.playerName);
                Debug.Log(PlayerEvaluater.EvaluatePlayer(Pair.Value));
            }
            */
            Dictionary<string, Player> AgeTransferPlayersNeeded = TransferSystem.GetAgeTransferPlayers(TeamPlayers,AllChinesePlayers,TeamWealth);
            foreach (KeyValuePair<string, Player> Pair in AgeTransferPlayersNeeded)
            {
                Debug.Log(Pair.Key);
                Debug.Log(Pair.Value.GetAge());
                Debug.Log(PlayerEvaluater.EvaluatePlayer(Pair.Value));
            }

        }
        
        

            //            List<string> KeyList = new List<string>(TypeTwoPositionsNeeded.Keys);
            //            List<float> ValueList = new List<float>(TypeTwoPositionsNeeded.Values);
            //           Debug.Log(KeyList[0]);
            //            Debug.Log(ValueList[0]);
            //            Debug.Log(TeamAvg["中后卫"]);




            //Debug.Log(BestPlayers["前锋"].playerName);
            //Debug.Log(PlayerEvaluater.EvaluatePlayer(BestPlayers["门将"]));

        

        );
    }
    


    // Update is called once per frame
    void OnDestroy()
    {
        myBtn.onClick.RemoveAllListeners();
    }
}

