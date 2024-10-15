using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Kingdom/Resource")]
public class ResourceData : ScriptableObject
{
    public enum ResourceType
    { 
        Item,
        Food,
        Resources,
        Luxury
    }
    public string resourceName;
    public string resourceDescription;
    public ResourceType resourceType;
    public int baseValue;   // The amount of resource in stock
    public float productionRate;
    public float consumptionRate;
    public float basePrice;
    public float priceMultiplier = 1f; // multiplier based on stock lvl

    // Returns the current price of the resource based on supply
    public float GetCurrentPrice()
    {
        return basePrice * priceMultiplier;
    }

    public void UpdatePriceBasedOnStock()
    {
        // less stock avlble higher the price
        priceMultiplier = 1f / Mathf.Max(1, baseValue);
    }



}
