using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{

    private int healthPotionCount = 0;
    private int staminaPotionCount = 0;

    public int GetPotionCount(string type)
    {
        int potionNum = 0;
        if (type == "Health")
        {
            potionNum = healthPotionCount;
        }
        else if (type == "Stamina")
        {
            potionNum = staminaPotionCount;
        }
        return potionNum;
    }

    public void SetPotionCount(string type, int count)
    {
        if (type == "Health")
        {
            healthPotionCount = count;
            UIManager.instance.healthPotionText.text = healthPotionCount.ToString();
        }
        else if (type == "Stamina")
        {
            staminaPotionCount = count;
            UIManager.instance.staminaPotionText.text = staminaPotionCount.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Potion potion = other.GetComponent<Potion>();

        if (potion != null)
        {
            if (potion.type == Potion.potionType.health)
            {
                healthPotionCount++;
                UIManager.instance.healthPotionText.text = healthPotionCount.ToString();
            }
            else if (potion.type == Potion.potionType.stamina)
            {
                staminaPotionCount++;
                UIManager.instance.staminaPotionText.text = staminaPotionCount.ToString();
            }
        }

        Destroy(other.gameObject);
    }
}
