using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlayerEvaluation : MonoBehaviour
{
    Button myBtn;
    


    void Start()
    {
        myBtn = GetComponent<Button>();
        myBtn.onClick.AddListener(delegate
        {//I am using current team = 0's players to test
            List<Player> TempPlayer = StreamingDatabaseManager.GetPlayersFromTeam(0);


            float before = PlayerEvaluater.EvaluateCM(TempPlayer[11]);
            for (int i = 1; i < 61; i++)
            {
                GrowthCalculation.RoundUpdate(TempPlayer);
            }
            float after = PlayerEvaluater.EvaluateCM(TempPlayer[11]);


            //TempPlayer[11].pace += (float)0.5;




            Debug.Log(TempPlayer[11].playerName + " " + TempPlayer[11].position + " " + TempPlayer[11].GetAge() + "Pace is: " + TempPlayer[11].pace + "Before: " + before + "After: " + after); //+"Before: "+before + "After: " + after

        });
    }


    // Update is called once per frame
    void OnDestroy()
    {
        myBtn.onClick.RemoveAllListeners();
    }
}



/**
            Player player = new Player();
            GrowthCalculation.ResetPlayer(player);

            //float MainGrowth = (float)0.08;
            //float SecondaryGrowth = (float)0.04;
            //float LittleGrowth = (float)0.02;

            float AM = PlayerEvaluater.EvaluateAM(player);
            float CB = PlayerEvaluater.EvaluateCB(player);
            float CF = PlayerEvaluater.EvaluateCF(player);
            float CM = PlayerEvaluater.EvaluateCM(player);
            float DM = PlayerEvaluater.EvaluateDM(player);
            float FB = PlayerEvaluater.EvaluateFB(player);
            float ST = PlayerEvaluater.EvaluateST(player);
            float WF = PlayerEvaluater.EvaluateWF(player);
            float WM = PlayerEvaluater.EvaluateWM(player);
            //orginal player evaluation

            for (int i = 1; i < 61; i++)
            {
                GrowthCalculation.ChangePlayerUnder18(player);
                GrowthCalculation.ChangePlayerWM(player);
            }

           

            float AMnow = PlayerEvaluater.EvaluateAM(player);
            float CBnow = PlayerEvaluater.EvaluateCB(player);
            float CFnow = PlayerEvaluater.EvaluateCF(player);
            float CMnow = PlayerEvaluater.EvaluateCM(player);
            float DMnow = PlayerEvaluater.EvaluateDM(player);
            float FBnow = PlayerEvaluater.EvaluateFB(player);
            float STnow = PlayerEvaluater.EvaluateST(player);
            float WFnow = PlayerEvaluater.EvaluateWF(player);
            float WMnow = PlayerEvaluater.EvaluateWM(player);

            //Debug.Log("AM18 one year after compared with orginal:" + (AMnow-AM));
            //Debug.Log("CB18 one year after compared with orginal:" + (CBnow-CB));
            //Debug.Log("CF18 one year after compared with orginal:" + (CFnow-CF));
            //Debug.Log("CM18 one year after compared with orginal:" + (CMnow-CM));
            //Debug.Log("DM18 one year after compared with orginal:" + (DMnow-DM));
            //Debug.Log("FB18 one year after compared with orginal:" + (FBnow-FB));
            //Debug.Log("ST18 one year after compared with orginal:" + (STnow-ST));
            //Debug.Log("WF18 one year after compared with orginal:" + (WFnow-WF));
            Debug.Log("WM18 one year after compared with orginal:" + (WMnow-WM));

            GrowthCalculation.ResetPlayer(player);

            for (int i = 1; i < 61; i++)
            {
                GrowthCalculation.ChangePlayer29to32(player);
                GrowthCalculation.ChangePlayerWM(player);
            }


            float AM29 = PlayerEvaluater.EvaluateAM(player);
            float CB29 = PlayerEvaluater.EvaluateCB(player);
            float CF29 = PlayerEvaluater.EvaluateCF(player);
            float CM29 = PlayerEvaluater.EvaluateCM(player);
            float DM29 = PlayerEvaluater.EvaluateDM(player);
            float FB29 = PlayerEvaluater.EvaluateFB(player);
            float ST29 = PlayerEvaluater.EvaluateST(player);
            float WF29 = PlayerEvaluater.EvaluateWF(player);
            float WM29 = PlayerEvaluater.EvaluateWM(player);




            //Debug.Log("AM29 one year after compared with orginal:" + (AM29 - 10));
            //Debug.Log("CB29 one year after compared with orginal:" + (CB29 - 10));
            //Debug.Log("CF29 one year after compared with orginal:" + (CF29 - 10));
            //Debug.Log("CM29 one year after compared with orginal:" + (CM29 - 10));
            //Debug.Log("DM29 one year after compared with orginal:" + (DM29 - 10));
            //Debug.Log("FB29 one year after compared with orginal:" + (FB29 - 10));
            //Debug.Log("ST29 one year after compared with orginal:" + (ST29 - 10));
            //Debug.Log("WF29 one year after compared with orginal:" + (WF29 - 10));
            Debug.Log("WM29 one year after compared with orginal:" + (WM29 - 10));
            
            **/
