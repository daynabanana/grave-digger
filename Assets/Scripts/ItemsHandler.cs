using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemsHandler : MonoBehaviour
{
    // Define text variables
    public TextMeshProUGUI OilText;
    public TextMeshProUGUI MoneyText;

    public int MoneyAmount;
    public int OilAmount;
    public Button OilButton;

    public void ItemCollected(string ItemName)
    {
        if (ItemName == "Oil")
        {
            if (OilAmount <= 10)
            {
                OilAmount += 1;
                OilText.text = $"OIL: {OilAmount}/10";
                
                if (!OilButton.interactable)
                {
                    OilButton.interactable = true;
                }
            }
        } else if (ItemName == "Money")
        {
            MoneyAmount += Random.Range(0, 11);
            MoneyText.text = $"${MoneyAmount}";
        } else if (ItemName == "Nothing")
        {
            
        }
    }
}
