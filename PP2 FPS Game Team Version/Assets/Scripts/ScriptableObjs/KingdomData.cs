using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Kingdom", menuName = "Kingdom/KingdomData")]
public class KingdomData : ScriptableObject
{
    public string kingdomName;
    public List<SettlementData> settlements; // list of cities and villages

    public float kingdomWealth; // Overall wealth of kingdom
    public float tradeBalance; // Balance of internal trade between settlements
}
