using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthCalculation : MonoBehaviour
{
    public static float RandomFloat(double minValue, double maxValue)
    {
        int RandomIndex = Random.Range(1, 5); //5 is arbitrarily chosen

        return (float)(RandomIndex * (maxValue - minValue) / 5 + minValue);

    }
    public static void ChangePlayerUnder18(Player player) {
        float MainGrowth18 = RandomFloat(0.04, 0.07);
        float SecondaryGrowth18 = RandomFloat(0.02, 0.04);
        float LittleGrowth18 = RandomFloat(0.01, 0.03);

        player.shooting = player.shooting + SecondaryGrowth18;
        player.pace += MainGrowth18;
        player.anticipation += LittleGrowth18;
        player.tackling += SecondaryGrowth18;
        player.header += SecondaryGrowth18;
        player.shortPass += SecondaryGrowth18;
        player.longPass += SecondaryGrowth18;
        player.strength += MainGrowth18;
        player.stamina += MainGrowth18;
        player.agility += MainGrowth18;
    }
    public static void ChangePlayer29to32(Player player)
    {
        float MainDecrease2932 = RandomFloat(-0.1, -0.06);
        float LittleGrowth2932 = RandomFloat(0.005, 0.015);

        player.pace += MainDecrease2932;
        player.anticipation += LittleGrowth2932;
        player.strength += MainDecrease2932;
        player.stamina += MainDecrease2932;
        player.agility += MainDecrease2932;
    }
    /// <summary>
    /// will add more increase to attributes that weigh more than 10%
    /// </summary>
    /// <param name="player"></param>
    //shooting = 15%;
    //pace = 12%
    //anticipation = 11%;
    //tackling = 3%;
    //header = 9%;
    //shortpass = 12%;
    //longpass = 12%;
    //strength = 7%;
    //stamina = 8%;
    //agility = 10%;
    public static void ChangePlayerAM(Player player) {

        float PositionGrowth = RandomFloat(0.015, 0.025);


        player.shooting += PositionGrowth;
        player.pace += PositionGrowth;
        player.anticipation += PositionGrowth;
        player.shortPass += PositionGrowth;
        player.longPass += PositionGrowth;
        

    }
    //shooting = 2%;
    //pace = 7%
    //anticipation = 21%;
    //tackling = 20%;
    //header = 10%;
    //shortpass = 5%;
    //longpass = 5%;
    //strength = 15%;
    //stamina = 10%;
    //agility = 5%;
    public static void ChangePlayerCB(Player player)
    {

        float PositionGrowth = RandomFloat(0.01, 0.02);


        player.anticipation += PositionGrowth;
        player.tackling += PositionGrowth;
        player.strength += PositionGrowth;
        player.stamina += PositionGrowth;
        player.header += PositionGrowth;


    }

    //shooting = 20%;
    //pace = 9%
    //anticipation = 15%;
    //tackling = 0%;
    //header = 16%;
    //shortpass = 5%;
    //longpass = 5%;
    //strength = 15%;
    //stamina = 10%;
    //agility = 5%;
    public static void ChangePlayerCF(Player player)
    {

        float PositionGrowth = RandomFloat(0.02, 0.03);


        player.shooting += PositionGrowth;
        player.anticipation += PositionGrowth;
        player.strength += PositionGrowth;


        
    }

    public static void ChangePlayerCM(Player player)
    {

        float PositionGrowth = RandomFloat(0.0075, 0.015);
        player.shooting += PositionGrowth;
        player.pace += PositionGrowth;
        player.anticipation += PositionGrowth;
        player.tackling += PositionGrowth;
        player.header += PositionGrowth;
        player.shortPass += PositionGrowth;
        player.longPass += PositionGrowth;
        player.strength += PositionGrowth;
        player.stamina += PositionGrowth;
        player.agility += PositionGrowth;
    }

    //shooting = 5%;
    //pace = 8%
    //anticipation = 18%;
    //tackling = 15%;
    //header = 10%;
    //shortpass = 6%;
    //longpass = 6%;
    //strength = 10%;
    //stamina = 14%;
    //agility = 8%;
    public static void ChangePlayerDM(Player player)
    {

        float PositionGrowth = RandomFloat(0.01, 0.02);
        player.shooting += PositionGrowth;
        
        player.anticipation += PositionGrowth;
        player.tackling += PositionGrowth;
        player.header += PositionGrowth;

        player.strength += PositionGrowth;
        player.stamina += PositionGrowth;
        
    }
    //shooting = 4%;
    //pace = 12%
    //anticipation = 11%;
    //tackling = 18%;
    //header = 7%;
    //shortpass = 8%;
    //longpass = 8%;
    //strength = 9%;
    //stamina = 10%;
    //agility = 13%;
    public static void ChangePlayerFB(Player player)
    {

        float PositionGrowth = RandomFloat(0.015, 0.025);
        
        player.anticipation += PositionGrowth;
        player.tackling += PositionGrowth;
        player.stamina += PositionGrowth;
        player.agility += PositionGrowth;
    }

    public static void ChangePlayerST(Player player)
    {
        //shooting = 16%;
        //pace = 13%
        //anticipation = 11%;
        //tackling = 1%;
        //header = 13%;
        //shortpass = 9%;
        //longpass = 7%;
        //strength = 10%;
        //stamina = 10%;
        //agility = 10%;

        float PositionGrowth = RandomFloat(0.002, 0.03);
        player.shooting += PositionGrowth;
        player.pace += PositionGrowth;
        player.anticipation += PositionGrowth;
        player.header += PositionGrowth;
        player.strength += PositionGrowth;
        player.stamina += PositionGrowth;
        player.agility += PositionGrowth;
    }
    //shooting = 10%;
    //pace = 20%
    //anticipation = 7%;
    //tackling = 1%;
    //header = 7%;
    //shortpass = 8%;
    //longpass = 8%;
    //strength = 7%;
    //stamina = 12%;
    //agility = 20%;
    public static void ChangePlayerWF(Player player)
    {

        float PositionGrowth = RandomFloat(0.01, 0.03);
        player.shooting += PositionGrowth;
        player.pace += PositionGrowth;
        player.pace += (float)0.01;
        player.stamina += PositionGrowth;

        player.agility += PositionGrowth;
        player.agility += (float)0.01;
    }
    //shooting = 10%;
    //pace = 15%
    //anticipation = 10%;
    //tackling = 7%;
    //header = 7%;
    //shortpass = 10%;
    //longpass = 10%;
    //strength = 7%;
    //stamina = 12%;
    //agility = 12%;
    public static void ChangePlayerWM(Player player)
    {
        float PositionGrowth = RandomFloat(0.01, 0.03);
        player.pace += PositionGrowth;
        player.anticipation += PositionGrowth;
        player.shortPass += PositionGrowth;
        player.longPass += PositionGrowth;
        player.stamina += PositionGrowth;
        player.agility += PositionGrowth;
        
    }

    /// <summary>
    /// reset a player's ability to make future calculation consistent
    /// </summary>
    /// <param name="player"></param>
    public static void ResetPlayer(Player player) {
        player.shooting = 10;
        player.pace = 10;
        player.anticipation = 10;
        player.tackling = 10;
        player.header = 10;
        player.shortPass = 10;
        player.longPass = 10;
        player.strength = 10;
        player.stamina = 10;
        player.agility = 10;
    }

    public static void RoundUpdate(List<Player> players) {
        foreach(Player player in players){
            int age = player.GetAge();
            if (age <= 18){
                ChangePlayerUnder18(player);
            }
            if (age >= 29 && age <= 32) {
                ChangePlayer29to32(player);
            }

            if (player.position == "前腰")
                ChangePlayerAM(player);
            else if (player.position == "后腰")
                ChangePlayerDM(player);
            else if (player.position == "中前卫")
                ChangePlayerCM(player);
            else if (player.position == "前锋")
                ChangePlayerST(player);
            else if (player.position == "中锋")
                ChangePlayerCF(player);
            else if (player.position.Contains("边卫"))
                ChangePlayerFB(player);
            else if (player.position.Contains("边前卫"))
                ChangePlayerWM(player);
            else if (player.position.Contains("边锋"))
                ChangePlayerWF(player);
            else if (player.position == ("中后卫"))
                ChangePlayerCB(player);
        }
        
    }


}
