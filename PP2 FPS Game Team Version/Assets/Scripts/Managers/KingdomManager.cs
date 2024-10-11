using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KingdomManager : MonoBehaviour
{
    public static KingdomManager Instance;
    public GameObject caravanPrefab;
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
            UpdateTrade();
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

    private void CreateCaravanForKingdom(KingdomData kingdom)
    {
        GameObject caravanGO = Instantiate(caravanPrefab, kingdom.settlements[0].position, Quaternion.identity);
        Caravan caravan = caravanGO.GetComponent<Caravan>();

        // Assign the caravan to start travelling between two settlements in the kingdom
        caravan.currentSettlement = kingdom.settlements[0]; // Starting settlement
        caravan.StartTravel(kingdom.settlements[1]);
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

    private void UpdateTrade()
    {
        foreach (var kingdom in kingdoms)
        {
            foreach (var settlement in kingdom.settlements)
            {
                var tradeTargets = FindOtherKingdoms(kingdom);
                foreach (var targetKingdom in tradeTargets)
                {
                    foreach (var targetSettlement in targetKingdom.settlements)
                    {
                        settlement.TradeWith(targetSettlement);
                    }
                }
            }
        }

        FindObjectOfType<Global_Kingdom_Eco>().UpdateUI();
    }

    // Find settlements in other kingdoms for trading purposes
    private List<KingdomData> FindOtherKingdoms(KingdomData currentKingodm)
    {
        return kingdoms.Where(K => K != currentKingodm).ToList();
    }
}
