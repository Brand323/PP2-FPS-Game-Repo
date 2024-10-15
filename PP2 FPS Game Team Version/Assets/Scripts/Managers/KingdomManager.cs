using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KingdomManager : MonoBehaviour
{
    public static KingdomManager Instance;
    public List<KingdomData> kingdoms;
    private float tradeInterval = 10f; // trade every 10 seconds
    private float productionInterval = 1f; // produce resources every 5 seconds
    private float nextProductionTime, nextTradeTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Initialize the kingdom's economies
        foreach (var kingdom in kingdoms)
        {
            CalculateKingdomEconomy(kingdom);
        }

        // set timers
        nextProductionTime = Time.time + productionInterval;
        nextTradeTime = Time.time + tradeInterval;
    }

    private void Update()
    {
        // Periodic resource production
        if (Time.time >= nextProductionTime)
        {
            UpdateKingdomEconomies();
            nextProductionTime = Time.time + productionInterval;
        }

        // Periodic trade between kingdoms
        if (Time.time >= nextTradeTime)
        {
            SimulateInternalTrade();
            SimulateExternalTrade();
            nextTradeTime = Time.time + tradeInterval;
        }
    }

    public void OnDayPassed()
    {
        // Each day all settlements produce resources
        foreach (var kingdom in kingdoms)
        {
            foreach (var settlement in kingdom.settlements)
            {
                settlement.ProduceResources();
            }
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
            settlement.RemoveEmptyResources();
        }

        kingdom.kingdomWealth = totalWealth;
        kingdom.tradeBalance = totalProduction - totalConsumption; // surplus or deficit

        Debug.Log($"{kingdom.kingdomName} has a wealth of {kingdom.kingdomWealth} and a trade balance of {kingdom.tradeBalance}");
    }

    private void UpdateKingdomEconomies()
    {
        foreach (var kingdom in kingdoms)
        {
            foreach (var settlement in kingdom.settlements)
            {
                settlement.ProduceResources();
            }
        }

        FindObjectOfType<Global_Kingdom_Eco>().UpdateUI();
    }

    // Find settlements in other kingdoms for trading purposes
    private List<KingdomData> FindOtherKingdoms(KingdomData currentKingodm)
    {
        return kingdoms.Where(K => K != currentKingodm).ToList();
    }

    // Simulates trade within a kingdom between its settlements
    private void SimulateInternalTrade()
    {
        foreach (var kingdom in kingdoms)
        {
            foreach (var settlement in kingdom.settlements)
            {
                // Trade with other settlements within the same kingdom
                var tradeTargets = kingdom.settlements.Where(s => s != settlement).ToList();
                foreach (var targetSettlement in tradeTargets)
                {
                    settlement.TradeWith(targetSettlement);
                }
            }
        }
        FindObjectOfType<Global_Kingdom_Eco>().UpdateUI();
    }

    // Simulates trade between kingdoms
    private void SimulateExternalTrade()
    {
        foreach (var kingdom in kingdoms)
        {
            foreach (var settlement in kingdom.settlements)
            {
                var tradeTargets = FindOtherKingdoms(kingdom); // Find other kingdoms to trade with
                foreach (var targetKingdom in tradeTargets)
                {
                    foreach (var targetSettlement in targetKingdom.settlements)
                    {
                        settlement.TradeWith(targetSettlement); // Trade with other kingdom's settlements
                    }
                }
            }
        }

        FindObjectOfType<Global_Kingdom_Eco>().UpdateUI(); // Update UI after external trade
    }
}
