using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipManager : MonoBehaviour
{
    public List<RelationshipData> relationships;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize the kingdom's economies
        foreach (var relationship in relationships)
        {
            GetKingdomRelationship(relationship);
        }
    }

    private void GetKingdomRelationship(RelationshipData relationship)
    {
        Debug.Log($"Player has {relationship.kingdomPoints} Kingdom Points towards the {relationship.kingdomData.kingdomName} Kingdom");
    }
}
