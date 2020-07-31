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
    //Type1
    public static Dictionary<string, Player> TypeOnePlayers(List<string> PositionsNeeded, List<Player> AllChinesePlayers, Dictionary<string, float> LeagueAvg, Dictionary<string, float> TeamAvg, int TeamWealth) {
        Dictionary<string, Player> PlayersNeeded = new Dictionary<string, Player>();
        if(PositionsNeeded.Count > 0){
            foreach (Player player in AllChinesePlayers)
            {
                if (PositionsNeeded.Contains(player.position))
                {
                    float evaluation = PlayerEvaluater.EvaluatePlayer(player);
                    int TransferBaseFee = PlayerEvaluater.EvaluatePlayerTransferFee(player);

                    if (!(PlayersNeeded.ContainsKey(player.position)))
                    {
                        if (evaluation > TeamAvg[player.position])
                        {
                            if ((evaluation >= (LeagueAvg[player.position] * 0.9)) && (evaluation <= (LeagueAvg[player.position] * 1.1)) && (TeamWealth >= 1.5 * TransferBaseFee))
                            {
                                PlayersNeeded.Add(player.position, player);
                                continue;
                            }
                        }
                    }

                    if (evaluation > TeamAvg[player.position])
                    {

                        if ((evaluation >= (LeagueAvg[player.position] * 0.9)) && (evaluation <= (LeagueAvg[player.position] * 1.1)) && (TeamWealth >= 1.5 * TransferBaseFee))
                        {

                        int TransferBaseFeeSoFar = PlayerEvaluater.EvaluatePlayerTransferFee(PlayersNeeded[player.position]);
                        if (TransferBaseFee < TransferBaseFeeSoFar)
                            {
                            PlayersNeeded[player.position] = player;
                            }

                        }
                    }

                }
            }
        }
        return PlayersNeeded;
    }
    //Type2

    //Type3




    //TODO: A function that takes 3 dictionaries and decide which players to buy(based on need, maybe an Array to Queue)
    


}
