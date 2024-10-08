using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan
{
    public SettlementData origin;
    public SettlementData destination;
    public List<ResourceData> cargo;

    public float travelDistance = 5f; // ex

    public void Travel()
    {
        // Move the goods from origin to destination
        if (Vector3.Distance(origin.position, destination.position) < travelDistance)
        {
            // Deliver the goods
            destination.resourcesStocked.AddRange(cargo);
            origin.resourcesStocked.RemoveAll(r => cargo.Contains(r));

            // Successful trade, increase prosperity
            origin.OnSuccessfulTrade();
            destination.OnSuccessfulTrade();
            
        }
    }

}
