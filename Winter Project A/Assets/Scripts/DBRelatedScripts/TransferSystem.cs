using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferSystem : MonoBehaviour
{
    
    
    /*    
        public static Dictionary<string,float> InitializeLeagueAvg() {
            Dictionary<string,float> LeagueAvg = new Dictionary<string, float>();
            LeagueAvg.Add("左边前卫", 0);
            LeagueAvg.Add("右边前卫", 0);
            LeagueAvg.Add("左边卫", 0);
            LeagueAvg.Add("右边卫", 0);
            LeagueAvg.Add("左边锋", 0);
            LeagueAvg.Add("右边锋", 0);
            LeagueAvg.Add("中锋", 0);
            LeagueAvg.Add("前锋", 0);
            LeagueAvg.Add("前腰", 0);
            LeagueAvg.Add("中前卫", 0);
            LeagueAvg.Add("后腰", 0);
            LeagueAvg.Add("中后卫", 0);
            LeagueAvg.Add("门将", 0);
            return LeagueAvg;
        }
        public static Dictionary<string, float> InitializePositionCount()
        {

            Dictionary<string, float> PositionCount = new Dictionary<string, float>();
            PositionCount.Add("左边前卫", 0);
            PositionCount.Add("右边前卫", 0);
            PositionCount.Add("左边卫", 0);
            PositionCount.Add("右边卫", 0);
            PositionCount.Add("左边锋", 0);
            PositionCount.Add("右边锋", 0);
            PositionCount.Add("中锋", 0);
            PositionCount.Add("前锋", 0);
            PositionCount.Add("前腰", 0);
            PositionCount.Add("中前卫", 0);
            PositionCount.Add("后腰", 0);
            PositionCount.Add("中后卫", 0);
            PositionCount.Add("门将", 0);
            return PositionCount;
        }
        */
    public static void GetChinesePlayersInTeam(List<Player> TeamPlayers) {
        foreach (Player player in TeamPlayers.ToArray()) {
            if (!(player.nationality == "中国")) {
                TeamPlayers.Remove(player);
            }
        }
    }
            //Right now we only make Chinese Players transfer system

    public static void CalculateLeagueAvg(Dictionary<string, float> LeagueAvg, Dictionary<string, float> PositionCount, List<Player> players) {
        foreach (Player player in players) {
            //if key does not exist, create a new one
            if (!(LeagueAvg.ContainsKey(player.position))){
                PositionCount.Add(player.position, 0);
                LeagueAvg.Add(player.position, 0);
            }
            float evaluation = PlayerEvaluater.EvaluatePlayer(player);
            float totalScorePreviously = LeagueAvg[player.position] * PositionCount[player.position];
            PositionCount[player.position] += 1;
            LeagueAvg[player.position] = (totalScorePreviously + evaluation) / PositionCount[player.position];
        }
    }
    /// <summary>
    /// Check Type 1 Transer: if LeagueAvg and TeamAvg have a big difference 
    /// </summary>
    /// <param name="LeagueAvg"></param>
    /// <param name="TeamAvg"></param>
    /// <returns></returns>
    public static List<string> CompareTeamAvgAndLeagueAvg(Dictionary<string, float> LeagueAvg, Dictionary<string, float> TeamAvg)
    {
        List<string> PositionsNeeded = new List<string>();
        foreach (KeyValuePair<string, float> PositionScore in TeamAvg)
        {
            if (LeagueAvg[PositionScore.Key] - PositionScore.Value > 3) //League average is bigger than team average for more than 3 points
            {
                 PositionsNeeded.Add(PositionScore.Key);
            }
        }
        return PositionsNeeded;
    }
    public static Dictionary<string, List<Player>> FindPlayerByPosition(List<string> PositionsNeeded, List<Player> AllChinesePlayers, Dictionary<string, float> LeagueAvg, int TeamWealth) {
        Dictionary<string, List<Player>> PlayersNeeded = new Dictionary<string, List<Player>>();
        PlayersNeeded.Add("Type1",new List<Player>());
        PlayersNeeded.Add("Type2", new List<Player>());
        PlayersNeeded.Add("Type3", new List<Player>());

        foreach (Player player in AllChinesePlayers) {
            if (PositionsNeeded.Contains(player.position)) {
                float evaluation = PlayerEvaluater.EvaluatePlayer(player);
                int TransferBaseFee = PlayerEvaluater.EvaluatePlayerTransferFee(player);
                if ((evaluation >= (LeagueAvg[player.position] * 0.9)) && (evaluation <= (LeagueAvg[player.position] * 1.1)) &&(TeamWealth >= 1.5 * TransferBaseFee))  {
                    PlayersNeeded["Type1"].Add(player);
                }
                //TODO: Add type2 and Type3 condition
            }
            }
        return PlayersNeeded;
    }
    //TODO: For same position, buy the cheapest players to tyr to satisfy all 3 needs. Do a check on positions needed


}
