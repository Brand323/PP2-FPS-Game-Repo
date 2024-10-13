using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money : MonoBehaviour
{

    private int CoinCount = 0;

    //Getter
    public int GetCoinCount()
    {
        return CoinCount;
    }
    //Setter
    public void SetCoinCount(int amount)
    {
        CoinCount += amount;
        UIManager.instance.moneyText.text = CoinCount.ToString();
    }

    //PickUp Coin 
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Coin")
        {
            CoinCount++;
            UIManager.instance.moneyText.text = CoinCount.ToString();
            Destroy(other.gameObject);
        }
    }
}
