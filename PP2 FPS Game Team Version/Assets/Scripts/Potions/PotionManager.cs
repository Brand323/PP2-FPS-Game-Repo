using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    private int healthPotionCount = 0;
    private int staminaPotionCount = 0;

    #region Getters and Setters

    public int HealthPotions
    {
        get { return healthPotionCount; }
        set { healthPotionCount = value; }
    }
    public int StaminaPotions
    {
        get { return staminaPotionCount; }
        set { staminaPotionCount = value; }
    }

    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            UsePotion("Health");
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            UsePotion("Stamina");
        }
    }
    void UsePotion(string type)
    {
        if (type == "Health" && gameManager.instance.playerScript.currentHealth < gameManager.instance.playerScript.MaxHealthPoints && healthPotionCount > 0)
        {
            gameManager.instance.playerScript.AddHealth(25);
            healthPotionCount--;
            UIManager.instance.healthPotionText.text = healthPotionCount.ToString();
        }
        else if (type == "Stamina" && gameManager.instance.playerScript.currentStamina < gameManager.instance.playerScript.MaxStaminaPoints && staminaPotionCount > 0)
        {
            gameManager.instance.playerScript.AddStamina(25);
            staminaPotionCount--;
            UIManager.instance.staminaPotionText.text = staminaPotionCount.ToString();
        }
    }

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

    public void AddHealthPotions(int amount)
    {
        healthPotionCount += amount;
    }

    public void AddStaminaPotions(int amount)
    {
        staminaPotionCount += amount;
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

            Destroy(other.gameObject);
        }
    }
}
