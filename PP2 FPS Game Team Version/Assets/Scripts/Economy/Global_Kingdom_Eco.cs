using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Global_Kingdom_Eco : MonoBehaviour
{

    [System.Serializable]
    public struct KingdomUI
    {
        public TextMeshProUGUI kingdomNameText;
        public TextMeshProUGUI kingdomMoneyText;
        public List<TextMeshProUGUI> settlementResourceTexts; // tracks resources
    }

    public KingdomManager kingdomManager;
    public List<KingdomUI> kingdomUIElements;

    private float lastUIUpdateTime = 0f;
    private float uiUpdateInterval = 1f;

    private void Start()
    {
        if (kingdomManager == null)
        {
            kingdomManager = FindObjectOfType<KingdomManager>();
            if (kingdomManager == null)
            {
                Debug.LogError("KingdomManager is missing! Cannot find KingdomManager in the scene.");
                return;
            }
        }
        UpdateUI();
    }


    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (Time.time - lastUIUpdateTime >= uiUpdateInterval)
        {
            for (int i = 0; i < kingdomManager.kingdoms.Count; i++)
            {
                KingdomData kingdom = kingdomManager.kingdoms[i];

                if (i < kingdomUIElements.Count)
                {
                    var ui = kingdomUIElements[i];

                    if (ui.kingdomNameText != null)
                    {
                        ui.kingdomNameText.SetText(kingdom.kingdomName);
                    }
                    if (ui.kingdomMoneyText != null)
                    {
                        ui.kingdomMoneyText.SetText($"{kingdom.kingdomWealth}");
                    }

                    // Update resources for each settlement
                    for (int j = 0; j < kingdom.settlements.Count; j++)
                    {
                        SettlementData settlement = kingdom.settlements[j];

                        if (j < ui.settlementResourceTexts.Count)
                        {
                            string resourcesText = $"{settlement.settlementName}: \n";
                            // Format the settlements name and resource info
                            foreach (var resource in settlement.resourcesStocked)
                            {
                                resourcesText += $"{resource.resourceName} ({resource.baseValue} gold)";
                            }

                            ui.settlementResourceTexts[j].SetText(resourcesText);
                        }
                    }
                }
            }

            lastUIUpdateTime = Time.time;
        }
    }
}
