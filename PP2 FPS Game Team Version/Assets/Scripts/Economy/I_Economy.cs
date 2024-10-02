using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Economy
{
    public void InitializeKingdomEconomy(); // Startup the kingdom's economy with certain conditions varying per kingdom on game start

    public void PopulateKingdomMainMarket(); // Initialize the kingdom's starting market's inventories, quantities, etc. on startup

    public void PopulateKingdomTownsMarkets(); // Initialize the towns makrets inventories, quantities, etc.




}
