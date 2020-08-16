using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransferSystem : MonoBehaviour
{


    /*    
        public static Dictionary<string,float> InitializeLeagueAvg() {
            Dictionary<string,float> LeagueAvg = new Dictionary<string, float>();
            LeagueAvg.Add("左边前卫", 0); "左边前卫","右边前卫","左边卫","右边卫","左边锋","右边锋","中锋","前锋","前腰","中前卫","后腰","中后卫","门将"
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
            if (!(LeagueAvg.ContainsKey(player.position))) {
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
        if (PositionsNeeded.Count > 0) {
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
    public static Dictionary<string, float> ComparePositionAvgAndTeamAvg(List<Player> TeamPlayers, Dictionary<string, float> TeamAvg, Dictionary<string, float> TeamPositionCount)
    {
        float ThisPositionScore = 0;
        float OtherPositionTotalScore = 0;
        float OtherPositionTotalCount = 0;
        float OtherPositionAvgScore = 0;

        List<Player> TempTeamPlayers = TeamPlayers;
        Dictionary<string, Player> BestPlayers = new Dictionary<string, Player>();
        Dictionary<string, Player> SecondBestPlayers = new Dictionary<string, Player>();
        Dictionary<Player, float> PlayerScoreDict = new Dictionary<Player, float>();
        Dictionary<string, float> Result = new Dictionary<string, float>();
        foreach (Player player in TeamPlayers) {
            float PlayerScore = PlayerEvaluater.EvaluatePlayer(player);
            PlayerScoreDict.Add(player, PlayerScore);
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
                ThisPositionScore = (PlayerScoreDict[pair.Value] + PlayerScoreDict[SecondBestPlayers[pair.Key]]) / 2;
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
    public static Dictionary<string, Player> GetTransfersByPosEval(Dictionary<string, float> PositionsNeeded, List<Player> AllChinesePlayers, int TeamWealth)
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
                        if ((evaluation > PositionsNeeded[player.position]) && (TeamWealth >= 1.5 * TransferBaseFee))
                        {
                            PlayersNeeded.Add(player.position, player);
                            continue;
                        }
                    }
                    else {
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

    public static Dictionary<string, float> FindPositionsLackingInTheTeam(Dictionary<string, float> TeamPosCount, Dictionary<string, float> LeagueAvg) {
        List<string> PositionList = new List<string> { "左边前卫", "右边前卫", "左边卫", "右边卫", "左边锋", "右边锋", "前锋", "前腰", "中前卫", "后腰", "中后卫", "门将" }; //Here 中锋 is not included
        List<string> TempPositionLacking = new List<string>();
        Dictionary<string, float> result = new Dictionary<string, float>();
        foreach (string Position in PositionList) {
            if (!(TeamPosCount.ContainsKey(Position))) {
                TempPositionLacking.Add(Position);
            }
        }
        //decide which ones to add to result list
        if (TempPositionLacking.Contains("门将")) {
            result.Add("门将", LeagueAvg["门将"]);
        }

        if (TempPositionLacking.Contains("左边前卫") && (TempPositionLacking.Contains("左边锋")))
        {
            List<string> RandomList = new List<string> { "左边前卫", "左边锋" };
            int RandomIndex = Random.Range(-1, 1);
            if (RandomIndex < 0)
            {
                result.Add(RandomList[0], LeagueAvg[RandomList[0]]);
            }
            else {
                result.Add(RandomList[1], LeagueAvg[RandomList[1]]);
            }
        }
        if (TempPositionLacking.Contains("右边前卫") && (TempPositionLacking.Contains("右边锋")))
        {
            List<string> RandomList = new List<string> { "右边前卫", "右边锋" };
            int RandomIndex = Random.Range(-1, 1);
            if (RandomIndex < 0)
            {
                result.Add(RandomList[0], LeagueAvg[RandomList[0]]);
            }
            else
            {
                result.Add(RandomList[1], LeagueAvg[RandomList[1]]);
            }
        }
        if (TempPositionLacking.Contains("前腰") && (TempPositionLacking.Contains("后腰")) && (TempPositionLacking.Contains("中前卫")))
        {
            List<string> RandomList = new List<string> { "前腰", "中前卫", "后腰" };
            int RandomIndex = Random.Range(0, 3);
            if (RandomIndex < 1)
            {
                result.Add(RandomList[0], LeagueAvg[RandomList[0]]);
            }
            else if (RandomIndex < 2)
            {
                result.Add(RandomList[1], LeagueAvg[RandomList[1]]);
            }
            else
            {
                result.Add(RandomList[2], LeagueAvg[RandomList[2]]);
            }
        }


        if (TempPositionLacking.Contains("前锋"))
        {
            result.Add("前锋", LeagueAvg["前锋"]);
        }
        if (TempPositionLacking.Contains("中后卫"))
        {
            result.Add("中后卫", LeagueAvg["中后卫"]);
        }
        if (TempPositionLacking.Contains("左边卫"))
        {
            result.Add("左边卫", LeagueAvg["左边卫"]);
        }
        if (TempPositionLacking.Contains("右边卫"))
        {
            result.Add("右边卫", LeagueAvg["右边卫"]);
        }

        return result;

    }

    public static float GetTeamAvgAge(List<Player> TeamPlayers) {
        float TotalAge = 0;
        foreach (Player player in TeamPlayers) {
            TotalAge += player.GetAge();
        }

        return TotalAge / TeamPlayers.Count;
    }
    //this function calculates list<player> avg evaluation
    public static float GetTeamAvgEvaluation(List<Player> TeamPlayers)
    {
        float TotalEvaluation = 0;
        foreach (Player player in TeamPlayers)
        {
            TotalEvaluation += PlayerEvaluater.EvaluatePlayer(player);
        }

        return TotalEvaluation / TeamPlayers.Count;
    }

    public static Dictionary<string, Player> GetAgeTransferPlayers(List<Player> TeamPlayers, List<Player> AllChinesePlayers, int TeamWealth) {
        float TeamAvgAge = GetTeamAvgAge(TeamPlayers);
        float TeamAvgEvaluation = GetTeamAvgEvaluation(TeamPlayers);
        Dictionary<string, Player> Result = new Dictionary<string, Player>();
        List<Player> StartingLineUp = GetStartingLineUpPlayers(TeamPlayers);
        float StartingAvgAge = GetTeamAvgAge(StartingLineUp);
        if ((TeamAvgAge > 27)&&(StartingAvgAge > 28)) {
            foreach (Player player in AllChinesePlayers) {
                float evaluation = PlayerEvaluater.EvaluatePlayer(player);
                int TransferBaseFee = PlayerEvaluater.EvaluatePlayerTransferFee(player);
                if ((TeamWealth >= 1.5 * TransferBaseFee) && ((float)player.GetAge() < (TeamAvgAge * 0.9)) && (evaluation > (TeamAvgEvaluation * 0.9))) {
                    if (!(Result.ContainsKey(player.position)))
                    {
                        Result.Add(player.position, player);
                    }
                    else if(TransferBaseFee < PlayerEvaluater.EvaluatePlayerTransferFee(Result[player.position])) //cheaper than current player in Result dictionary
                    {
                        Result[player.position] = player;
                    }
                
                }
            }
        }
        return Result;
    }

    public static List<string> GenerateOptimalStartingLineup(List<Player> TeamPlayers, int[] droppableIndices = null)
    {
        if (droppableIndices == null)
            droppableIndices = new int[] { 0, 1, 3, 4, 5, 13, 14, 16, 15, 22, 23 };
        string[] formationList = new string[] { "门将", "左边卫", "中后卫", "中后卫", "右边卫", "左边前卫", "中前卫", "中前卫", "右边前卫", "前锋", "前锋" };
        var list = new List<Player>();
        var temp = new List<Player>();

        foreach (Player player in TeamPlayers)
        {
            temp.Add(player);
        }

        foreach (var pos in formationList)
        {
            var l = new List<Player>();
            foreach (var v in temp)
            {
                if (v.position == pos)
                    l.Add(v);
            }
            l.Sort(new PlayerEvaluater.AbilityBasedPlayerDescendingComparer());
            if (l.Count > 0)
            {
                list.Add(l[0]);
                temp.Remove(l[0]);
            }
        }

        temp.Sort(new PlayerEvaluater.AttackingAbilityBasedPlayerDescendingComparer());
        int idx = 0;
        while (list.Count < 11)
        {
            if (temp[idx].position != "门将")
                list.Add(temp[idx++]);
        }

        List<string> ret = new List<string>();

        for (int i = 0; i < 24; i++)
        {
            ret.Add(null);
        }


        for (int i = 0; i < droppableIndices.Length; i++)
        {
            ret[droppableIndices[i]] = list[i].ID + ",0";
            //Debug.Log(ret[droppableIndices[i]]);
        }

        return ret;
    }


    public static bool NeedKeyPlayerSubstituteHelper(KeyValuePair<Player, float> pair, Dictionary<Player, float> temp) {
        bool result = true;
        foreach (KeyValuePair<Player, float> TempPair in temp) {
            if (pair.Key.position == TempPair.Key.position) {
                if (TempPair.Value > pair.Value * 0.9) {
                    result = false;
                }
            }
        }
        return result;
    }

    public static void AddKeyPlayersHelper(Dictionary<string, float> result, Dictionary<Player, float> temp, float threshold)
    {
        foreach (KeyValuePair<Player, float> pair in temp)
        {
            if ((pair.Value > threshold) && (NeedKeyPlayerSubstituteHelper(pair, temp)))
            {
                result.Add(pair.Key.position, pair.Value * (float)0.9);
            }
        }
    }

    public static Dictionary<string, float> IfNeedKeyPlayerSub(List<Player> TeamPlayers) {
        Dictionary<string, float> result = new Dictionary<string, float>();
        Dictionary<Player, float> temp = new Dictionary<Player, float>();
        foreach (Player player in TeamPlayers) {
            temp.Add(player, PlayerEvaluater.EvaluatePlayer(player));
        }
        float TeamAvgEvaluation = GetTeamAvgEvaluation(TeamPlayers);

        if (TeamAvgEvaluation > 83)
        {
            return result;
        }
        else if (TeamAvgEvaluation > 79)
        {
            AddKeyPlayersHelper(result, temp, 88);
            return result;
        }
        else if (TeamAvgEvaluation > 74)
        {
            AddKeyPlayersHelper(result, temp, 83);
            return result;
        }
        else if (TeamAvgEvaluation > 69)
        {
            AddKeyPlayersHelper(result, temp, 79);
            return result;
        }
        else if (TeamAvgEvaluation > 59)
        {
            AddKeyPlayersHelper(result, temp, 75);
            return result;
        }
        else if (TeamAvgEvaluation > 49)
        {
            AddKeyPlayersHelper(result, temp, 67);
            return result;
        }
        else {
            return result;
        }
    }


    //TODO: split team starting line up age >28
    //TODO: list<keyvaluepair> to determine which players to buy
    //TODO: SQL
    public static List<Player> GetStartingLineUpPlayers(List<Player> Teamplayers)
    {
        List<string> StartingLineUpStringList = GenerateOptimalStartingLineup(Teamplayers);
        List<string> IDList = new List<string>();
        List<Player> Result = new List<Player>();
        Dictionary<string,Player> IDDict = new Dictionary<string, Player>();
        foreach (string str in StartingLineUpStringList) {
            if (!(str == null)) {

                string[] words = str.Split(',');
                IDList.Add(words[0]);
            }
        }
        foreach (Player player in Teamplayers) {
            IDDict.Add(player.ID.ToString(),player);

        }
        foreach (string str in IDList) {
            Result.Add(IDDict[str]);
        }
        return Result;
    }
    public static List<Player> FinalPlayersToBuy(Dictionary<string,Player> TypeOne, Dictionary<string, Player> TypeTwo, Dictionary<string, Player> TypeThree, Dictionary<string, Player> TransferAge, Dictionary<string, Player> TransferKeySub) {
        List<Player> result = new List<Player>();
        Dictionary<string, Player> ResultDictionary = new Dictionary<string, Player>();
        DetermineResultDictHelper(ResultDictionary,TypeOne);
        DetermineResultDictHelper(ResultDictionary, TypeTwo);
        DetermineResultDictHelper(ResultDictionary, TypeThree);
        DetermineResultDictHelper(ResultDictionary, TransferAge);
        DetermineResultDictHelper(ResultDictionary, TransferKeySub);
        result = ResultDictionary.Values.ToList(); //after combining each position we know have a list of only one player each position
        return result;
    }

    public static void DetermineResultDictHelper(Dictionary<string, Player> ResultDictionary, Dictionary<string, Player> TransferDict) {
        foreach (KeyValuePair<string,Player> Pair in TransferDict) {
            if (!(ResultDictionary.ContainsKey(Pair.Key))) {
                ResultDictionary.Add(Pair.Key, Pair.Value);
                continue;
            }
            else {
                float NewPlayerEva = PlayerEvaluater.EvaluatePlayer(Pair.Value);
                float OriginalPlayerEva = PlayerEvaluater.EvaluatePlayer(ResultDictionary[Pair.Key]);
                if (NewPlayerEva > OriginalPlayerEva) {
                    ResultDictionary[Pair.Key] = Pair.Value;
                }
            }
        }
    }
    public static Dictionary<Player, int> MakeTransfer(Dictionary<string, Player> TypeOne, Dictionary<string, Player> TypeTwo, Dictionary<string, Player> TypeThree, Dictionary<string, Player> TransferAge, Dictionary<string, Player> TransferKeySub, int TeamWealth) {
        Dictionary<Player, int> result = new Dictionary<Player, int>();
        List<Player> TotalList = FinalPlayersToBuy(TypeOne, TypeTwo, TypeThree, TransferAge, TransferKeySub);
        int TransferTime = 1;
        int TraverseTime = TotalList.Count;
        while ((TeamWealth > 0)&&(TransferTime <= 6)&&(TotalList.Count > 0)) {
            int index = Random.Range(0, TotalList.Count);
            int TransferFee = (int)(PlayerEvaluater.EvaluatePlayerTransferFee(TotalList[index]) * Random.Range(1.05f,1.5f));
            Debug.Log("transfer fee is: " + TransferFee);
            if (TransferFee < TeamWealth) {
                TeamWealth -= TransferFee;
                result.Add(TotalList[index],TransferFee);
                TransferTime += 1;
                Debug.Log("Current teamwealth is: " + TeamWealth);
            }
            TotalList.RemoveAt(index);
        }
        return result;
    }








}
