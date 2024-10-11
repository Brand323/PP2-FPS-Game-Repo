using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementLinker : MonoBehaviour
{
    public SettlementData settlementData;

    private void Start()
    {
        // Set the cube's position to match the settlement's stored pos
        settlementData.position = transform.position;

    }
}
