using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Global_Kingdom_Eco : MonoBehaviour
{
    /*
     
        - 4 Kingdoms
           - 2 Main City Fiefs
           - 3 Supporting villages

           - towns/cities have specific production items
           - those items value will adjust dynamically on a specific range based on dynamic supply and demand
                - affected by global conditions such as 
     
     
     
     */


    public GameObject Kingdom_1;
    public TextMeshProUGUI K_1_Money_UIDisplay;

    public GameObject Kingdom_2;
    public TextMeshProUGUI K_2_Money_UIDisplay;

    public GameObject Kingdom_3;
    public TextMeshProUGUI K_3_Money_UIDisplay;

    public GameObject Kingdom_4;
    public TextMeshProUGUI K_4_Money_UIDisplay;


    private int k_One_MainFunds;
    private int k_Two_MainFunds;
    private int k_Three_MainFunds;
    private int k_Four_MainFunds;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        InitMainFunds();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

    }

    public string IntToText(int i)
    {
        return i.ToString();
    }
    public void InitMainFunds()
    {
        k_One_MainFunds = 100;
        k_Two_MainFunds = 100;
        k_Three_MainFunds = 100;
        k_Four_MainFunds = 100;

    }

    public void UpdateUI()
    {
        K_1_Money_UIDisplay.SetText(IntToText(k_One_MainFunds));
        K_2_Money_UIDisplay.SetText(IntToText(k_Two_MainFunds));
        K_3_Money_UIDisplay.SetText(IntToText(k_Three_MainFunds));
        K_4_Money_UIDisplay.SetText(IntToText(k_Four_MainFunds));

    }

}
