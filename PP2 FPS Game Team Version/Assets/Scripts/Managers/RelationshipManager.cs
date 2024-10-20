using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipManager : MonoBehaviour
{
    public List<KeyValuePair<string, int>> relationshipRanks = new List<KeyValuePair<string, int>>()
    {
        new KeyValuePair<string, int>("Betrayer of the Kingdom", -100),
        new KeyValuePair<string, int>("Enemy", -50),
        new KeyValuePair<string, int>("Neutral", 0),
        new KeyValuePair<string, int>("Ally", 50),
        new KeyValuePair<string, int>("Hero of the Kingdom", 100)
    };

    public List<RelationshipData> relationships;

    // Start is called before the first frame update
    private void Start()
    {

    }

    void updateKingdomRelationshipRank(RelationshipData relationship)
    {
        int relationshipKingdomPoints = relationship.kingdomPoints;

        string relationshipRank = "";

        if (relationshipKingdomPoints == -100)
            relationshipRank = "Betrayer of the Kingdom";
        if (relationshipKingdomPoints > -99 && relationshipKingdomPoints < -50)
            relationshipRank = "Enemy";
        else if (relationshipKingdomPoints > -49 && relationshipKingdomPoints < 49)
            relationshipRank = "Neutral";
        else if (relationshipKingdomPoints > 50 && relationshipKingdomPoints < 99)
            relationshipRank = "Ally";
        else if (relationshipKingdomPoints == 100)
            relationshipRank = "Hero of the Kingdom";

        relationship.kingdomRank = relationshipRank;
    }

    void setKingdomPoints(int points ,RelationshipData relationship)
    {
        if (relationship != null)
        {
            relationship.kingdomPoints += points;
            updateKingdomRelationshipRank(relationship);
        }
    }

    void addKingdomPoints(int pointsToAdd, RelationshipData relationship)
    {
        if (relationship != null)
        {
            relationship.kingdomPoints += pointsToAdd;
            updateKingdomRelationshipRank(relationship);
        }
    }

    void subtractKingdomPoints(int pointsToSubtract, RelationshipData relationship)
    {
        if (relationship != null)
        {
            relationship.kingdomPoints -= pointsToSubtract;
            updateKingdomRelationshipRank(relationship);
        }
    }
}
