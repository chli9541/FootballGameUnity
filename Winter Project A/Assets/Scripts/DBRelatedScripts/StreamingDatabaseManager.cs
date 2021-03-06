using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class StreamingDatabaseManager
{

    static SqliteConnection dbconn = null;
    static SqliteCommand dbcmd = null;
    static SqliteDataReader reader = null;

    /// <summary>
    /// Restores the db connection to unconnected settings
    /// </summary>
    static void RestoreConnection()
    {
        if (reader != null)
            reader.Close();
        reader = null;
        if (dbcmd != null)
            dbcmd.Dispose();
        dbcmd = null;
        if (dbconn != null)
            dbconn.Close();
        dbconn = null;
    }

    public static void UpdateLeagueTable(int leagueID, string tableString)
    {
        string query = string.Format("UPDATE League SET Ranking = '{0}' WHERE ID = '{1}' ;", tableString, leagueID);
        MakeNonSelectionQuery(query);
    }

    private static void MakeNonSelectionQuery(string sqlQuery)
    {
        using (SqliteConnection c = new SqliteConnection("URI=file:" + Application.dataPath + "/StreamingAssets/db.db"))
        {
            c.Open();
            using (SqliteCommand cmd = new SqliteCommand(sqlQuery, c))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }


    //PLAYER RELATED SQL FUNCTIONS IS BEBLOW:
    /*
   /// <summary>
   /// get the count of players in PlayerInfo table
   /// </summary>
   /// <returns>int player count</returns>
   public static int GetPlayersCount()
   {
       string query = string.Format("SELECT COUNT(*) FROM PlayerInfo;");
       int ret = -1;
       string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/UnityFirstTry.db"; //Path to database.
       using (SqliteConnection c = new SqliteConnection(conn))
       {
           c.Open();
           using (SqliteCommand cmd = new SqliteCommand(query, c))
           {
               using (SqliteDataReader reader = cmd.ExecuteReader())
               {
                   while (reader.Read())
                   {
                       ret = reader.GetInt32(0);
                   }
               }
           }
       }
       return ret;
   }


   public static void UpdatePlayerAbility(int PlayerAbility, int PlayerID)
   {
       string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/UnityFirstTry.db"; //Path to database.
       string query = string.Format("UPDATE PlayerInfo SET PlayerScore = '{0}' WHERE PlayerID = '{1}' ;", PlayerAbility, PlayerID);
       using (SqliteConnection c = new SqliteConnection(conn))
       {
           c.Open();

           using (SqliteCommand cmd = new SqliteCommand(query, c))
           {
               cmd.ExecuteNonQuery();
           }
       }
   }
   */

    public static int GetPlayerScore(int PlayerID)
    {
        string query = string.Format("SELECT PlayerScore FROM PlayerInfo WHERE PlayerID = '{0}' ;", PlayerID);
        int ret = -1;
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/UnityFirstTry.db"; //Path to database.
        using (SqliteConnection c = new SqliteConnection(conn))
        {
            c.Open();
            using (SqliteCommand cmd = new SqliteCommand(query, c))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ret = reader.GetInt32(0);
                    }
                }
            }
        }
        return ret;
    }




    //HELPER FUNCTIONS
    public static Player GetPlayer(int playerID)
    {
        string query = string.Format("SELECT * FROM Players WHERE ID = '{0}' ;", playerID);
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/db.db"; //Path to database.
        Player p = new Player();
        using (SqliteConnection c = new SqliteConnection(conn))
        {
            c.Open();
            using (SqliteCommand cmd = new SqliteCommand(query, c))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string currentTeam = reader.GetString(1);
                        string playerName = reader.GetString(2);
                        if (playerID == 0 && playerName.Length == 0)
                            playerName = "老总";
                        float keeperAnticipation = reader.GetFloat(3);
                        float keeperSaving = reader.GetFloat(4);
                        float pace = reader.GetFloat(5);
                        float anticipation = reader.GetFloat(6);
                        float tackling = reader.GetFloat(7);
                        float header = reader.GetFloat(8);
                        float shortPass = reader.GetFloat(9);
                        float longPass = reader.GetFloat(10);
                        float strength = reader.GetFloat(11);
                        float agility = reader.GetFloat(12);
                        float fragile = reader.GetFloat(13);
                        float weakerFoot = reader.GetFloat(14);
                        string previousClubs = reader.GetString(15);
                        string nationality = reader.GetString(16);
                        string position = reader.GetString(17);
                        string umaString = reader.GetString(18);
                        int marketValue = reader.GetInt32(19);
                        int numberPreference = reader.GetInt32(20);
                        float ambition = reader.GetFloat(21);
                        int professional = reader.GetInt32(22);
                        int moneyStatus = reader.GetInt32(23);
                        float married = reader.GetInt32(24);
                        string childrenNames = reader.GetString(25);
                        float composure = reader.GetFloat(26);
                        float dirtyness = reader.GetFloat(27);
                        float angerControl = reader.GetFloat(28);
                        float speechMaking = reader.GetFloat(29);
                        float domesticPopulation = reader.GetFloat(30);
                        float internationalPopulation = reader.GetFloat(31);
                        string birthday = reader.GetString(32);
                        string birthPlace = reader.GetString(33);
                        string academyPlace = reader.GetString(34);
                        int educationLevel = reader.GetInt32(35);
                        int salary = reader.GetInt32(36);
                        string clubRelations = reader.GetString(37);
                        string peopleRelations = reader.GetString(38);
                        string injuryHistory = reader.GetString(39);
                        string relationshipHistory = reader.GetString(40);
                        string partnerName = reader.GetString(41);
                        string personalSponsor = reader.GetString(42);
                        string brandPreference = reader.GetString(43);
                        float majiang = reader.GetFloat(44);
                        float doudizhu = reader.GetFloat(45);
                        float alchohol = reader.GetFloat(46);
                        float xiangqi = reader.GetFloat(47);
                        int signatureFont = reader.GetInt32(48);
                        int shooting = reader.GetInt32(49);
                        int stamina = reader.GetInt32(50);
                        int generated = reader.GetInt16(51);
                        int height = reader.GetInt16(52);
                        string profession = reader.GetString(53);
                        string graduatedSchool = reader.GetString(54);
                        int numberWearing = reader.GetInt32(55);
                        string skills = reader.GetString(56);
                        int weight = reader.GetInt16(57);
                        string statsHistory = reader.GetString(58);
                        string contractDetails = reader.GetString(59);

                        p.agility = agility;
                        p.alchohol = alchohol;
                        p.ambition = ambition;
                        p.angerControl = angerControl;
                        p.anticipation = anticipation;
                        p.birthday = birthday;
                        p.birthPlace = birthPlace;
                        p.brandPreference = brandPreference;
                        p.chlidrenNames = childrenNames;
                        p.clubRelations = clubRelations;
                        p.composure = composure;
                        p.currentClub = currentTeam;
                        p.dirtyness = dirtyness;
                        p.domesticPopulation = domesticPopulation;
                        p.doudizhu = doudizhu;
                        p.educationLevel = educationLevel;
                        p.fragile = fragile;
                        p.header = header;
                        p.ID = id;
                        p.injuryHistory = injuryHistory;
                        p.internationalPopulation = internationalPopulation;
                        p.keeperAnticipation = keeperAnticipation;
                        p.keeperSaving = keeperSaving;
                        p.longPass = longPass;
                        p.majiang = majiang;
                        p.marketValue = marketValue;
                        p.married = married == 1 ? 1 : 0;
                        p.moneyStatus = moneyStatus;
                        p.nationality = nationality;
                        p.numberPreference = numberPreference;
                        p.pace = pace;
                        p.partnerName = partnerName;
                        p.peopleRelations = peopleRelations;
                        p.personalSponsor = personalSponsor;
                        p.playerName = playerName;
                        p.position = position;
                        p.previousClubs = previousClubs;
                        p.professional = professional;
                        p.relationshipHistory = relationshipHistory;
                        p.salary = salary;
                        p.shooting = shooting;
                        p.shortPass = shortPass;
                        p.speechMaking = speechMaking;
                        p.stamina = stamina;
                        p.strength = strength;
                        p.tackling = tackling;
                        p.umaString = umaString;
                        p.weakerFoot = weakerFoot;
                        p.xiangqi = xiangqi;
                        p.generated = generated;
                        p.signatureFont = signatureFont;
                        p.height = height;
                        p.profession = profession;
                        p.graduatedSchool = graduatedSchool;
                        p.numberWearing = numberWearing;
                        p.skill = skills;
                        p.weight = weight;
                        p.statsHistory = statsHistory;
                        p.contractDetails = contractDetails;
                    }
                }
            }
        }
        return p;
    }


   

    /// <summary>
    /// Argument format - [TEAM_ID]
    /// </summary>
    public static List<Player> GetPlayersFromTeam(int teamID)
    {
        string query = string.Format("SELECT Players FROM Teams WHERE ID = {0} ;", teamID);
        List<Player> list = new List<Player>();
        string playersString = "";
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/db.db"; //Path to database.
        using (SqliteConnection c = new SqliteConnection(conn))
        {
            c.Open();
            using (SqliteCommand cmd = new SqliteCommand(query, c))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        playersString = reader.GetString(0);
                    }
                }
            }
        }

        List<int> ids = new List<int>();

        foreach (var v in playersString.Split(','))
        {
            if (v.Length == 0)
                continue;
            ids.Add(System.Int32.Parse(v.Trim()));
        }
        return GetMultiplePlayers(ids);
    }

    


    public static List<Player> GetMultiplePlayers(List<int> playerIDs)
    {
        string cond = "";
        foreach (var v in playerIDs)
            cond += (v + ",");
        cond = cond.Substring(0, cond.Length - 1);
        string query = string.Format("SELECT * FROM Players WHERE ID in ({0}) ;", cond);
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/db.db"; //Path to database.
        List<Player> players = new List<Player>();
        using (SqliteConnection c = new SqliteConnection(conn))
        {
            c.Open();
            using (SqliteCommand cmd = new SqliteCommand(query, c))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Player p = new Player();
                        players.Add(p);
                        int id = reader.GetInt32(0);
                        string currentTeam = reader.GetString(1);
                        string playerName = reader.GetString(2);
                        if (id == 0 && playerName.Length == 0)
                            playerName = "老总";
                        float keeperAnticipation = reader.GetFloat(3);
                        float keeperSaving = reader.GetFloat(4);
                        float pace = reader.GetFloat(5);
                        float anticipation = reader.GetFloat(6);
                        float tackling = reader.GetFloat(7);
                        float header = reader.GetFloat(8);
                        float shortPass = reader.GetFloat(9);
                        float longPass = reader.GetFloat(10);
                        float strength = reader.GetFloat(11);
                        float agility = reader.GetFloat(12);
                        float fragile = reader.GetFloat(13);
                        float weakerFoot = reader.GetFloat(14);
                        string previousClubs = reader.GetString(15);
                        string nationality = reader.GetString(16);
                        string position = reader.GetString(17);
                        string umaString = reader.GetString(18);
                        int marketValue = reader.GetInt32(19);
                        int numberPreference = reader.GetInt32(20);
                        float ambition = reader.GetFloat(21);
                        int professional = reader.GetInt32(22);
                        int moneyStatus = reader.GetInt32(23);
                        float married = reader.GetInt32(24);
                        string childrenNames = reader.GetString(25);
                        float composure = reader.GetFloat(26);
                        float dirtyness = reader.GetFloat(27);
                        float angerControl = reader.GetFloat(28);
                        float speechMaking = reader.GetFloat(29);
                        float domesticPopulation = reader.GetFloat(30);
                        float internationalPopulation = reader.GetFloat(31);
                        string birthday = reader.GetString(32);
                        string birthPlace = reader.GetString(33);
                        string academyPlace = reader.GetString(34);
                        int educationLevel = reader.GetInt32(35);
                        int salary = reader.GetInt32(36);
                        string clubRelations = reader.GetString(37);
                        string peopleRelations = reader.GetString(38);
                        string injuryHistory = reader.GetString(39);
                        string relationshipHistory = reader.GetString(40);
                        string partnerName = reader.GetString(41);
                        string personalSponsor = reader.GetString(42);
                        string brandPreference = reader.GetString(43);
                        float majiang = reader.GetFloat(44);
                        float doudizhu = reader.GetFloat(45);
                        float alchohol = reader.GetFloat(46);
                        float xiangqi = reader.GetFloat(47);
                        int signatureFont = reader.GetInt32(48);
                        int shooting = reader.GetInt32(49);
                        int stamina = reader.GetInt32(50);
                        int generated = reader.GetInt16(51);
                        int height = reader.GetInt16(52);
                        string profession = reader.GetString(53);
                        string graduatedSchool = reader.GetString(54);
                        int numberWearing = reader.GetInt32(55);
                        string skills = reader.GetString(56);
                        int weight = reader.GetInt16(57);
                        string statsHistory = reader.GetString(58);
                        string contractDetails = reader.GetString(59);

                        p.agility = agility;
                        p.alchohol = alchohol;
                        p.ambition = ambition;
                        p.angerControl = angerControl;
                        p.anticipation = anticipation;
                        p.birthday = birthday;
                        p.birthPlace = birthPlace;
                        p.brandPreference = brandPreference;
                        p.chlidrenNames = childrenNames;
                        p.clubRelations = clubRelations;
                        p.composure = composure;
                        p.currentClub = currentTeam;
                        p.dirtyness = dirtyness;
                        p.domesticPopulation = domesticPopulation;
                        p.doudizhu = doudizhu;
                        p.educationLevel = educationLevel;
                        p.fragile = fragile;
                        p.header = header;
                        p.ID = id;
                        p.injuryHistory = injuryHistory;
                        p.internationalPopulation = internationalPopulation;
                        p.keeperAnticipation = keeperAnticipation;
                        p.keeperSaving = keeperSaving;
                        p.longPass = longPass;
                        p.majiang = majiang;
                        p.marketValue = marketValue;
                        p.married = married == 1 ? 1 : 0;
                        p.moneyStatus = moneyStatus;
                        p.nationality = nationality;
                        p.numberPreference = numberPreference;
                        p.pace = pace;
                        p.partnerName = partnerName;
                        p.peopleRelations = peopleRelations;
                        p.personalSponsor = personalSponsor;
                        p.playerName = playerName;
                        p.position = position;
                        p.previousClubs = previousClubs;
                        p.professional = professional;
                        p.relationshipHistory = relationshipHistory;
                        p.salary = salary;
                        p.shooting = shooting;
                        p.shortPass = shortPass;
                        p.speechMaking = speechMaking;
                        p.stamina = stamina;
                        p.strength = strength;
                        p.tackling = tackling;
                        p.umaString = umaString;
                        p.weakerFoot = weakerFoot;
                        p.xiangqi = xiangqi;
                        p.generated = generated;
                        p.signatureFont = signatureFont;
                        p.height = height;
                        p.profession = profession;
                        p.graduatedSchool = graduatedSchool;
                        p.numberWearing = numberWearing;
                        p.skill = skills;
                        p.weight = weight;
                        p.statsHistory = statsHistory;
                        p.contractDetails = contractDetails;
                    }
                }
            }
        }
        return players;
    }


    ///Here are player trainig related codes
    /// <summary> 
    /// this is the function that reads player abilities and update them and write them in db
    /// for ability changes due to training 
    /// </summary>
    /// <param name="TeamID"></param>
    public static void UpdatePlayerTrainingInTeam(int TeamID, string TrainingType) {
            switch (TrainingType) {
            case "Strength":
                StrengthPractice(TeamID);
                break;
            case "Agility":
                AgilityPractice(TeamID);
                break;
            case "Tackling":
                TacklingPractice(TeamID);
                break;
        }
        
    }

    /// <summary>
    /// changed name from updatePlayerAttribute to UpdatePlayerTraining
    /// because this is a training specific function
    /// and the new value type is also changed to float
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="attributeName"></param>
    /// <param name="newValue"></param>
    public static void UpdatePlayerTraining(int playerID, string attributeName, float newValue)
    {
        string query = string.Format("UPDATE Players SET {0} = '{1}' WHERE ID = '{2}' ;", attributeName, newValue, playerID);
        MakeNonSelectionQuery(query);
    }
    /// <summary>
    /// this function updates strength of team 0 after training
    /// </summary>
    /// <param name="teamID"></param>
    /// 
    public static void StrengthPractice(int teamID)
    {
        //List<Player> list = GetPlayersFromTeam(teamID);
        UnityEngine.Debug.Log("start2");
        string query1 = string.Format("update Players SET strength = strength + 0.1 where CurrentTeam = {0} and age<20", teamID);
        string query2 = string.Format("update Players SET strength = strength + 0.07 where CurrentTeam = {0} and 19<age<25", teamID);
        string query3 = string.Format("update Players SET strength = strength + 0.04 where CurrentTeam = {0} and 24<age<30", teamID);
        string query4 = string.Format("update Players SET strength = strength + 0.02 where CurrentTeam = {0} and 29<age<35", teamID);
        MakeNonSelectionQuery(query1);
        MakeNonSelectionQuery(query2);
        MakeNonSelectionQuery(query3);
        MakeNonSelectionQuery(query4);
    }
    public static void AgilityPractice(int teamID)
    {
        //List<Player> list = GetPlayersFromTeam(teamID);
        UnityEngine.Debug.Log("start2");
        string query1 = string.Format("update Players SET agility = agility + 0.1 where CurrentTeam = {0} and age<20", teamID);
        string query2 = string.Format("update Players SET agility = agility + 0.07 where CurrentTeam = {0} and 19<age<25", teamID);
        string query3 = string.Format("update Players SET agility = agility + 0.04 where CurrentTeam = {0} and 24<age<30", teamID);
        string query4 = string.Format("update Players SET agility = agility + 0.02 where CurrentTeam = {0} and 29<age<35", teamID);
        MakeNonSelectionQuery(query1);
        MakeNonSelectionQuery(query2);
        MakeNonSelectionQuery(query3);
        MakeNonSelectionQuery(query4);
    }

    public static void TacklingPractice(int teamID)
    {
        //List<Player> list = GetPlayersFromTeam(teamID);
        UnityEngine.Debug.Log("start2");
        string query1 = string.Format("update Players SET tackling = tackling + 0.1 where CurrentTeam = {0} and age<20", teamID);
        string query2 = string.Format("update Players SET tackling = tackling + 0.07 where CurrentTeam = {0} and 19<age<25", teamID);
        string query3 = string.Format("update Players SET tackling = tackling + 0.04 where CurrentTeam = {0} and 24<age<30", teamID);
        string query4 = string.Format("update Players SET tackling = tackling + 0.02 where CurrentTeam = {0} and 29<age<35", teamID);
        MakeNonSelectionQuery(query1);
        MakeNonSelectionQuery(query2);
        MakeNonSelectionQuery(query3);
        MakeNonSelectionQuery(query4);
    }

    ///Here are player evaluation related codes

    /**
    public static void UpdatePlayersAbility(List<Player> players)
    {
        foreach (Player player in players) {
            UpdatePlayerAbility(player);
        }
    }
    **/

    public static List<Player> GetAllTradableChinesePlayers() {
        string query = string.Format("SELECT * FROM Players WHERE Nationality = '中国'AND Tradable = 0");
        List<int> ids = new List<int>();
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/db.db"; //Path to database.
        using (SqliteConnection c = new SqliteConnection(conn))
        {
            c.Open();
            using (SqliteCommand cmd = new SqliteCommand(query, c))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        ids.Add(reader.GetInt32(0));
                    }
                }
            }
        }
        return GetMultiplePlayers(ids);
    }

    public static void UpdateNull()
    {
        string query = string.Format("UPDATE Players SET UMAString  = 'NA' WHERE UMAString IS NULL;");
        MakeNonSelectionQuery(query);

    }
    /// <summary>
    /// Return all the chinese players from the same league as teamID, and this function allows evaluation calculation possible
    /// </summary>
    /// <param name="TeamID"></param>
    /// <returns></returns>
    public static List<Player> SearchChinesePlayersFromSameLeague(int TeamID)
    {
        //string query = string.Format("SELECT * FROM Players where Nationality = '中国' and CurrentTeam IN (SELECT ID FROM Teams WHERE BelongingLeague = 3);");
        string query = string.Format("SELECT * FROM Players where Nationality = '中国' and CurrentTeam !={0} and CurrentTeam IN (SELECT ID FROM Teams WHERE BelongingLeague = (SELECT BelongingLeague FROM Teams WHERE ID = {0}));",TeamID);
        List<int> ids = new List<int>();
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/db.db"; //Path to database.
        using (SqliteConnection c = new SqliteConnection(conn))
        {
            c.Open();
            using (SqliteCommand cmd = new SqliteCommand(query, c))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        ids.Add(reader.GetInt32(0));
                    }
                }
            }
        }
        return GetMultiplePlayers(ids);
    }
    public static int  GetTeamWealth(int TeamID) 
    {
        //string query = string.Format("SELECT * FROM Players where Nationality = '中国' and CurrentTeam IN (SELECT ID FROM Teams WHERE BelongingLeague = 3);");
        string query = string.Format("SELECT TeamWealth FROM Teams WHERE ID = {0};", TeamID);
        int TeamWealth = -1;
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/db.db"; //Path to database.
        using (SqliteConnection c = new SqliteConnection(conn))
        {
            c.Open();
            using (SqliteCommand cmd = new SqliteCommand(query, c))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        TeamWealth = reader.GetInt32(0);
                    }
                }
            }
        }
        return TeamWealth;
    }
    


    public static void Transfer(int teamID, Dictionary<Player, int> TransferFinalList)
    {
        //List<Player> list = GetPlayersFromTeam(teamID);
        UnityEngine.Debug.Log("start2");
        foreach (KeyValuePair<Player, int> pair in TransferFinalList) {
            string OldClub = pair.Key.currentClub; // this will be the old club when transfer happens
            string query1 = string.Format("update Players SET CurrentTeam = {0} where ID = {1}", teamID, pair.Key.ID);
            MakeNonSelectionQuery(query1);
            string query2 = string.Format("update Players SET tradable = 1 where ID = {0}", pair.Key.ID);
            MakeNonSelectionQuery(query2);

            string query3 = string.Format("update Teams SET TeamWealth = TeamWealth + {0} where ID = {1}",pair.Value,OldClub);
            string query4 = string.Format("update Teams SET TeamWealth = TeamWealth - {0} where ID = {1}", pair.Value, teamID);
            MakeNonSelectionQuery(query3);
            MakeNonSelectionQuery(query4);
        }
            
        
        
    }



    public static void MakeAllPlayersTradable()
    {   
        string query1 = string.Format("update Players SET Tradable = 0");   
        MakeNonSelectionQuery(query1);
    }
    /// <summary>
    /// Due to previous design problems, this function aims to correct database team wealth
    /// </summary>
    public static void CorrectDBWealth()
    {
        string query1 = string.Format("update Teams SET TeamWealth = 100 where TeamWealth = 0");
        MakeNonSelectionQuery(query1);
        string query2 = string.Format("update Teams SET TeamWealth = TeamWealth * 100000");
        MakeNonSelectionQuery(query2);
    }





}

