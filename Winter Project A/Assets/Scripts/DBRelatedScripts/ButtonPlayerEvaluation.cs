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
        {
            Player player = new Player();
            player.shooting = 90;
            player.pace = 89;
            player.anticipation = 80;
            player.tackling = 60;
            player.header = 78;
            player.shortPass = 78;
            player.longPass = 75;
            player.strength = 65;
            player.stamina = 70;
            player.agility = 80;

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
            Debug.Log("AMold" + AM);
            Debug.Log("CBold" + CB);
            Debug.Log("CFold" + CF);
            Debug.Log("CMold" + CM);
            Debug.Log("DMold" + DM);
            Debug.Log("FBold" + FB);
            Debug.Log("STold" + ST);
            Debug.Log("WFold" + WF);
            Debug.Log("WMold" + WM);




            for (int i = 1; i < 61; i++)
            {
                float MainGrowth18 = RandomFloat(0.05,0.09);
                float SecondaryGrowth18 = RandomFloat(0.03, 0.05);
                float LittleGrowth18 = RandomFloat(0.01, 0.03);
                

                player.shooting = player.shooting+SecondaryGrowth18;
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


            float AMnow = PlayerEvaluater.EvaluateAM(player);
            float CBnow = PlayerEvaluater.EvaluateCB(player);
            float CFnow = PlayerEvaluater.EvaluateCF(player);
            float CMnow = PlayerEvaluater.EvaluateCM(player);
            float DMnow = PlayerEvaluater.EvaluateDM(player);
            float FBnow = PlayerEvaluater.EvaluateFB(player);
            float STnow = PlayerEvaluater.EvaluateST(player);
            float WFnow = PlayerEvaluater.EvaluateWF(player);
            float WMnow = PlayerEvaluater.EvaluateWM(player);

            Debug.Log("AM18 one year after compared with orginal::" + (AMnow-AM));
            Debug.Log("CB18 one year after compared with orginal::" + (CBnow-CB));
            Debug.Log("CF18 one year after compared with orginal::" + (CFnow-CF));
            Debug.Log("CM18 one year after compared with orginal::" + (CMnow-CM));
            Debug.Log("DM18 one year after compared with orginal::" + (DMnow-DM));
            Debug.Log("FB18 one year after compared with orginal::" + (FBnow-FB));
            Debug.Log("ST18 one year after compared with orginal::" + (STnow-ST));
            Debug.Log("WF18 one year after compared with orginal::" + (WFnow-WF));
            Debug.Log("WM18 one year after compared with orginal::" + (WMnow-WM));

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

            for (int i = 1; i < 61; i++)
            {
                float MainGrowth2932 = RandomFloat(-0.05, -0.02);
                float LittleGrowth2932 = RandomFloat(0.005, 0.015);

                player.pace += MainGrowth2932;
                player.anticipation += LittleGrowth2932;
                player.strength += MainGrowth2932;
                player.stamina += MainGrowth2932;
                player.agility += MainGrowth2932;
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

            Debug.Log("AM29 one year after compared with orginal:" + (AM29 - 10));
            Debug.Log("CB29 one year after compared with orginal:" + (CB29 - 10));
            Debug.Log("CF29 one year after compared with orginal:" + (CF29 - 10));
            Debug.Log("CM29 one year after compared with orginal:" + (CM29 - 10));
            Debug.Log("DM29 one year after compared with orginal:" + (DM29 - 10));
            Debug.Log("FB29 one year after compared with orginal:" + (FB29 - 10));
            Debug.Log("ST29 one year after compared with orginal:" + (ST29 - 10));
            Debug.Log("WF29 one year after compared with orginal:" + (WF29 - 10));
            Debug.Log("WM29 one year after compared with orginal:" + (WM29 - 10));


        });
    }
    public static float RandomFloat(double minValue, double maxValue)
    {
        int RandomIndex = Random.Range(1, 5); //5 is arbitrarily chosen
        
        return (float)(RandomIndex * (maxValue - minValue) / 5 + minValue);
        
    }

    // Update is called once per frame
    void OnDestroy()
    {
        myBtn.onClick.RemoveAllListeners();
    }
}
