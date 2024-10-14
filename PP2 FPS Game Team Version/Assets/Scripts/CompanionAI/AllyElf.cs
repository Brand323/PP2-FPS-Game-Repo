using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyElf : RangedAlly
{
    // Start is called before the first frame update
    void Start()
    {
        AllyCombatManager.instance.companionList.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
