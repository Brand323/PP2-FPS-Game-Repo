using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settlement", menuName = "Kingdom/Settlement")]

public class SettlementData : ScriptableObject
{
    public string settlementName;
    public int population;
    public float wealth; // current wealth of the settlement

    public float productionRate;
    public float baseProductionRate = 1.0f;
    public float consumptionRate;

    [Range(0f, 100f)] public float prosperity = 1.0f; // influences production rate and wealth

    public SettlementType settlementType;
    public enum SettlementType { City, Village }
    public bool IsCity => settlementType == SettlementType.City;

    // Resources managed by the settlement
    public List<ResourceData> resourcesProduces; // Resources that this settlement produces
    public List<ResourceData> resourcesStocked; // Resources that this city will stock or trade
    public List<ResourceData> resourcesToCraft; // Cities will craft these from stocked resources
    public Vector3 position; // The world position of the settlement

    // Methods
    public void TradeWith(SettlementData targetSettlement)
    {
        foreach (var resource in resourcesStocked)
        {
            // Check if trgt settlement has this resource in stock
            var targetStock = targetSettlement.resourcesStocked.Find(r => r.resourceName == resource.resourceName);

            // Determine the amnt available to transfer and the current price per unit
            int amountAvailable = resource.baseValue;
            float resourcePrice = resource.basePrice;

            // Determine the max amount buyer can afford
            int maxAffordableByBuyer = Mathf.FloorToInt(targetSettlement.wealth / resourcePrice);

            // Ensure that the amount to transfer is within the trader's stock and the buyer's
            int amountToTransfer = Mathf.Min(amountAvailable, 10, maxAffordableByBuyer);

            if (amountToTransfer > 0)
            {
                // Calc trade value
                float tradeValue = amountToTransfer + resourcePrice;

                if (targetSettlement.wealth >= tradeValue && this.wealth >= tradeValue)
                {
                    // If the target settlement doesnt have the resource, create one
                    if (targetStock == null)
                    {
                        ResourceData newResource = ScriptableObject.CreateInstance<ResourceData>();
                        newResource.resourceName = resource.resourceName;
                        newResource.baseValue = amountToTransfer;
                        targetSettlement.resourcesStocked.Add(newResource);
                    }
                    else
                    {
                        // If the target already has this resource, increase stock
                        targetStock.baseValue += amountToTransfer;
                    }

                    // Deduct resources from the seller and transfer gold
                    resource.baseValue -= amountToTransfer;
                    this.wealth += tradeValue;
                    targetSettlement.wealth -= resourcePrice;

                    // Optionally adjust prosperity or other factors after a successful trade
                    this.OnSuccessfulTrade();
                    targetSettlement.OnSuccessfulTrade();
                }
            }
        }
    }
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
        if (resource == null || amountProduced <= 0)
        {
            return; // Do nothing if the resource is null or amount is invalid
        }

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

    public void InitializeWealth(float minWealth, float maxWealth)
    {
        wealth = Random.Range(minWealth, maxWealth);
    }

    public void RemoveEmptyResources()
    {
        resourcesStocked.RemoveAll(resource => resource == null || resource.baseValue <= 0);
    }


}
