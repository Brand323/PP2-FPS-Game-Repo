using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Kingdom", menuName = "Kingdom/KingdomData")]
public class KingdomData : ScriptableObject
{
    public string kingdomName;
    public List<SettlementData> settlements; // list of cities and villages

    // Calculate total wealth based on the wealth of each settlement
    public float GetKingdomWealth()
    {
        float totalWealth = 0f;
        foreach (SettlementData settlement in settlements)
        {
            totalWealth += settlement.wealth;
        }
        return totalWealth;
    }

    public float kingdomWealth; // Overall wealth of kingdom
    public float tradeBalance; // Balance of internal trade between settlements
}
