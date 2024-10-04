using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomManager : MonoBehaviour
{
    public List<KingdomData> kingdoms;

    private void Start()
    {
        // Initialize the kingdom's economies
        foreach (var kingdom in kingdoms)
        {
            CalculateKingdomEconomy(kingdom);
        }
    }

    private void CalculateKingdomEconomy(KingdomData kingdom)
    {
        float totalWealth = 0;
        float totalProduction = 0;
        float totalConsumption = 0;
        
        foreach (var settlement in kingdom.settlements)
        {
            totalWealth += settlement.wealth;
            totalProduction += settlement.productionRate;
            totalConsumption += settlement.consumptionRate;
        }

        kingdom.kingdomWealth = totalWealth;
        kingdom.tradeBalance = totalProduction - totalConsumption; // surplus or deficit

        Debug.Log($"{kingdom.kingdomName} has a wealth of {kingdom.kingdomWealth} and a trade balance of {kingdom.tradeBalance}");
    }
}
