using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Caravan : MonoBehaviour
{
    public float gold = 300f;
    public List<ResourceData> cargo = new List<ResourceData>(); // Empty cargo
    public SettlementData currentSettlement = null; // no initial 
    public SettlementData targetSettlement = null; // no initial
    public float travelSpeed = 1.0f; // Speed at which the caravan "Travels" (units per day)
    private float travelProgress = 0f;
    public bool isTravelling = false;

    private void Update()
    {
        if (isTravelling)
        {

        }
    }

    public void StartTravel(SettlementData target)
    {
        if (target == null)
        {
            Debug.LogError("No target settlement assigned to the caravan. Cannot travel.");
            return;
        }

        targetSettlement = target;
        travelProgress = 0f;
        isTravelling = true;
    }

    private void SimulateTravel()
    {
        // Calculate dist to target settlement
        if (currentSettlement == null || targetSettlement == null)
            return; // Dont simulate if not assigned

        // Calculate the distance to target settlement
        float distanceToTravel = Vector3.Distance(currentSettlement.position, targetSettlement.position);

        // Simulate travel progress based on the travel speed and time (Days)
        travelProgress += (gameTimeManager.Instance.dayLength * travelSpeed * Time.deltaTime);

        if (travelProgress >= distanceToTravel)
        {
            isTravelling = false;
            currentSettlement = targetSettlement;
            TradeWithSettlement(targetSettlement);
        }
    }

    public void TradeWithSettlement(SettlementData settlement)
    {
        // Perform trade logic 
        Debug.Log($"Caravan arrived at {settlement.settlementName}, and is ready to trade!");
    }

}
