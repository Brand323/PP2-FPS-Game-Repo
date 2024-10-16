using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    [SerializeField] ResourceData item;

    // Update is called once per frame
    public void addItemToInventory()
    {
        if (item != null)
        {
            gameManager.instance.AddItemToInventory(item);
        }
    }
}
