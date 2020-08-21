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
            //StreamingDatabaseManager.CorrectDBWealth(); You only need to run this once, because now that all team wealth has been multiplied by 10w
            StreamingDatabaseManager.UpdateNull();//Fixed Database Null bug, not necessary if database does not have null values.
            StreamingDatabaseManager.MakeAllPlayersTradable();// Only use this at the begining of trading season, this will make all players available,
            //but we don't want this during trading season when players are traded they shouldn't be traded again.
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
            //以上是准备工作，下面是决定不同type的transfer
            List<string> PositionsNeeded = TransferSystem.CompareTeamAvgAndLeagueAvg(LeagueAvg, TeamAvg);
            Dictionary<string, Player> TypeOneTransfer = TransferSystem.TypeOnePlayers(PositionsNeeded, AllChinesePlayers, LeagueAvg, TeamAvg, TeamWealth);
            Dictionary<string, float> TypeTwoPositionsNeeded = TransferSystem.ComparePositionAvgAndTeamAvg(TeamPlayers, TeamAvg, TeamPositionCount);
            Dictionary<string, float> TypeThreePositionsNeeded = TransferSystem.FindPositionsLackingInTheTeam(TeamPositionCount,LeagueAvg);
            Dictionary<string, Player> TypeThreeTransfer = TransferSystem.GetTransfersByPosEval(TypeThreePositionsNeeded, AllChinesePlayers, TeamWealth);
            Dictionary<string, Player> TypeTwoTransfer = TransferSystem.GetTransfersByPosEval(TypeTwoPositionsNeeded, AllChinesePlayers, TeamWealth);

            Dictionary<string, Player> AgeTransfer = TransferSystem.GetAgeTransferPlayers(TeamPlayers,AllChinesePlayers,TeamWealth);
            Dictionary<string, float> KeyPlayerSubPositionNeeded = TransferSystem.IfNeedKeyPlayerSub(TeamPlayers);
            Dictionary<string, Player> KeyPlayerSubTransfer = TransferSystem.GetTransfersByPosEval(KeyPlayerSubPositionNeeded,AllChinesePlayers,TeamWealth);

            List<Player> TransferList = TransferSystem.FinalPlayersToBuy(TypeOneTransfer,TypeTwoTransfer,TypeThreeTransfer,AgeTransfer,KeyPlayerSubTransfer);
            /*foreach (Player player in TransferList) {
                Debug.Log(player.position);
                Debug.Log(PlayerEvaluater.EvaluatePlayer(player));
            }
            */
            Dictionary<Player,int> TransferFinalList = TransferSystem.MakeTransfer(TypeOneTransfer, TypeTwoTransfer, TypeThreeTransfer, AgeTransfer, KeyPlayerSubTransfer, TeamWealth);
            foreach (KeyValuePair < Player,int> pair in TransferFinalList)
            {
                Debug.Log(pair.Key.position);
                Debug.Log(PlayerEvaluater.EvaluatePlayer(pair.Key));
            }

        }
        


        );
    }
    

    void OnDestroy()
    {
        myBtn.onClick.RemoveAllListeners();
    }
}

