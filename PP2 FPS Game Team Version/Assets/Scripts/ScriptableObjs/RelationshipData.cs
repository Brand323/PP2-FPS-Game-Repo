using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RelationshipData", menuName = "Kingdom/RelationshipData")]
public class RelationshipData : ScriptableObject
{
    public KingdomData kingdomData; // Reference to the KingdomData Object.
    public string kingdomRank; // Rank name.
    [Range(-100,100)] public int kingdomPoints; // Faction points. Added if you help the Kingdom or subtracted if you betray them.
}
