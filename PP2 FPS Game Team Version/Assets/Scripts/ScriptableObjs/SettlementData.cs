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
    public float consumptionRate;
    public float prosperity; // influences production rate and wealth

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
            int amountProduced = Mathf.RoundToInt(resource.productionRate * Time.deltaTime); // We can replace time.Delta time with a tick based time system (time of day system)
            // Add this to the towns stock
            var stockedResource = resourcesStocked.Find(r => r.resourceName == resource.resourceName);
            if (stockedResource != null)
            {
                stockedResource.baseValue += amountProduced;
            }
            else
            {
                ResourceData newResource = ScriptableObject.CreateInstance<ResourceData>();
                newResource.resourceName = resource.resourceName;
                newResource.baseValue = amountProduced;
                resourcesStocked.Add(newResource);

            }
        }

        FindObjectOfType<Global_Kingdom_Eco>().UpdateUI();
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
        prosperity += amount;
        productionRate *= (1 + prosperity / 100); // Production rate will increase with prosperity
    }

    public void OnSuccessfulTrade()
    {
        AdjustProsperity(0.2f);
    }
}
