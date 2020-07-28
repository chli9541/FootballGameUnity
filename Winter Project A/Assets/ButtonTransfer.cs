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
        {//I am using current team = 0's players to test
            StreamingDatabaseManager.UpdateNull();
            List<Player> TempPlayer = StreamingDatabaseManager.GetAllPlayers();
            //List<Player> TempPlayer = StreamingDatabaseManager.GetPlayersFromTeam(0);


            //            for (int i = 1; i < 61; i++)


            Debug.Log(TempPlayer[11].playerName); 

        });
    }


    // Update is called once per frame
    void OnDestroy()
    {
        myBtn.onClick.RemoveAllListeners();
    }
}

