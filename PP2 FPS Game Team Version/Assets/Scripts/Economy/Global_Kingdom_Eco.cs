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
    }

    public KingdomManager kingdomManager;
    public List<KingdomUI> kingdomUIElements;

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
            }
        }
    }
}
