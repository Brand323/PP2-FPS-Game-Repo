using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settlement", menuName = "Kingdom/Settlement")]

public class SettlementData : ScriptableObject
{
    public string settlementName;
    public int population;
    public float wealth;
    public float productionRate;
    public float baseProductionRate = 1.0f;
    public float consumptionRate;
    [Range(-50f, 100f)] public float prosperity = 1.0f; // influences production rate and wealth

    public SettlementType settlementType;
    public enum SettlementType { City, Village }
    public bool IsCity => settlementType == SettlementType.City;

    // Resources managed by the settlement
    public List<ResourceData> resourcesProduces; // Resources that this settlement produces
    public List<ResourceData> resourcesStocked; // Resources that this city will stock or trade

    public List<ResourceData> resourcesToCraft; // Cities will craft these from stocked resources

    public Vector3 position; // The world position of the settlement

    // Methods
    public void ProduceResources()
    {
        foreach (var resource in resourcesProduces)
        {
            int amountProduced = Mathf.RoundToInt(resource.productionRate * baseProductionRate * prosperity);
            AddResourceToStock(resource, amountProduced);
        }

        FindObjectOfType<Global_Kingdom_Eco>().UpdateUI();
    }

    public void AddResourceToStock(ResourceData resource, int amountProduced)
    {
        var stockedResource = resourcesStocked.Find(r => r.resourceName == resource.resourceName);

        if (stockedResource != null)
            stockedResource.baseValue += amountProduced;
        else
        {
            ResourceData newResource = ScriptableObject.CreateInstance<ResourceData>();
            newResource.resourceName = resource.resourceName;
            newResource.baseValue = amountProduced;
            resourcesStocked.Add(newResource);
        }
    }

    public void TradeWith(SettlementData otherSettlement)
    {
        // Check the resources the settlement needs (consumptionRate) and what the other settlement has in stock
        foreach (var resourceNeeded in resourcesToCraft)
        {
            if (otherSettlement.resourcesStocked.Contains(resourceNeeded))
            {
                // Trade occurs, transfer the resources
                otherSettlement.resourcesStocked.Remove(resourceNeeded);
                resourcesStocked.Add(resourceNeeded);
            }
        }

        FindObjectOfType<Global_Kingdom_Eco>().UpdateUI();

    }

    public void AdjustProsperity(float amount)
    {
        prosperity = Mathf.Clamp(prosperity + amount, 0, 100);
        baseProductionRate = 1 + (prosperity / 100); // Higher prosperity will increase production
    }

    public void OnSuccessfulTrade()
    {
        AdjustProsperity(0.2f);
    }

    public void AdjustResourcePrices()
    {
        foreach (var resource in resourcesStocked)
        {
            resource.UpdatePriceBasedOnStock();
        }
    }

}
