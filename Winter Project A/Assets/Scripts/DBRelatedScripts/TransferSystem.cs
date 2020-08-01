using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    /// Check Type 1 Transfer: if LeagueAvg and TeamAvg have a big difference 
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
    public static Dictionary<string,float> ComparePositionAvgAndTeamAvg(List<Player> TeamPlayers,Dictionary<string,float> TeamAvg, Dictionary<string, float> TeamPositionCount)
    {
        float ThisPositionScore = 0;
        float OtherPositionTotalScore = 0;
        float OtherPositionTotalCount = 0;
        float OtherPositionAvgScore = 0;

        List <Player> TempTeamPlayers = TeamPlayers;
        Dictionary<string, Player> BestPlayers = new Dictionary<string, Player>();
        Dictionary<string, Player> SecondBestPlayers = new Dictionary<string, Player>();
        Dictionary<Player,float> PlayerScoreDict = new Dictionary<Player,float>();
        Dictionary<string, float> Result = new Dictionary<string, float>();
        foreach (Player player in TeamPlayers) {
            float PlayerScore = PlayerEvaluater.EvaluatePlayer(player);
            PlayerScoreDict.Add(player,PlayerScore);
        }
        
        foreach (Player player in TempTeamPlayers)
        {

            if (!(BestPlayers.ContainsKey(player.position)))//if first time encountering this position, it will be best so far
            {
                BestPlayers.Add(player.position, player);
            }
            else {
                if (PlayerScoreDict[player] > PlayerScoreDict[BestPlayers[player.position]]) {
                    BestPlayers[player.position] = player;
                }
            }

        }
        foreach (KeyValuePair<string, Player> BestPlayer in BestPlayers) {
            TempTeamPlayers.Remove(BestPlayer.Value);
        }
        //After removing all best players we can find secnod best
        foreach (Player player in TempTeamPlayers)
        {

            if (!(SecondBestPlayers.ContainsKey(player.position)))//if first time encountering this position, it will be best so far
            {
                SecondBestPlayers.Add(player.position, player);
            }
            else
            {
                if (PlayerScoreDict[player] > PlayerScoreDict[SecondBestPlayers[player.position]])
                {
                    SecondBestPlayers[player.position] = player;
                }
            }

        }

        foreach (KeyValuePair<string, Player> pair in BestPlayers) {
            //There is only one player in that direction, in which case average score is the player's score
            if (!(SecondBestPlayers.ContainsKey(pair.Key)))
            {
                ThisPositionScore = PlayerScoreDict[pair.Value];
            }
            else {
                ThisPositionScore = (PlayerScoreDict[pair.Value] + PlayerScoreDict[SecondBestPlayers[pair.Key]])/2;
            } 
            foreach (KeyValuePair<string, float> TeamPair in TeamAvg) {
                if (TeamPair.Key != pair.Key) { //not the same position
                    OtherPositionTotalScore += TeamPair.Value * TeamPositionCount[TeamPair.Key];
                    OtherPositionTotalCount += TeamPositionCount[TeamPair.Key];
                }
            }
            
            OtherPositionAvgScore = OtherPositionTotalScore / OtherPositionTotalCount;
            
            if (ThisPositionScore < OtherPositionAvgScore) {
                Result.Add(pair.Key, OtherPositionAvgScore);
            }

        }
        return Result;
    }
    public static Dictionary<string, Player> TypeTwoPlayers(Dictionary<string, float> PositionsNeeded, List<Player> AllChinesePlayers, int TeamWealth)
    {
        Dictionary<string, Player> PlayersNeeded = new Dictionary<string, Player>();
        if (PositionsNeeded.Count > 0)
        {
            foreach (Player player in AllChinesePlayers)
            {
                if (PositionsNeeded.ContainsKey(player.position))
                {
                    float evaluation = PlayerEvaluater.EvaluatePlayer(player);
                    int TransferBaseFee = PlayerEvaluater.EvaluatePlayerTransferFee(player);

                    if (!(PlayersNeeded.ContainsKey(player.position)))//if there is no such position in PlayersNeeded
                    {
                        if ((evaluation > PositionsNeeded[player.position])&& (TeamWealth >= 1.5 * TransferBaseFee))
                        {
                                PlayersNeeded.Add(player.position, player);
                                continue;
                        }
                    }
                    else{
                        if ((evaluation > PositionsNeeded[player.position]) && (TeamWealth >= 1.5 * TransferBaseFee))
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




    //Type3




    //TODO: A function that takes 3 dictionaries and decide which players to buy(based on need, maybe an Array to Queue)



}
