using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settlement", menuName = "Kingdom/Settlement")]

public class SettlementData : ScriptableObject
{
    public string settlementName;
    public int population;
    public float wealth;
    public float productionRate;
    public float consumptionRate;

    public SettlementType settlementType;
    public enum SettlementType { City, Village }
    public bool IsCity => settlementType == SettlementType.City;

    // Resources managed by the settlement
    public List<ResourceData> resourcesProduces; // Resources that this settlement produces
    public List<ResourceData> resourcesStocked; // Resources that this city will stock or trade

    public List<ResourceData> resourcesToCraft; // Cities will craft these from stocked resources
}
