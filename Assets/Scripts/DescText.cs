using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescText : MonoBehaviour
{
    private TMP_Text myText;
    private string fullText;

    public StatsSO PlayerStatsSO;

    private void OnEnable()
    {
        myText = gameObject.GetComponentInChildren<TMP_Text>();
    }
    public void SetText(string itemName, float itemOdds)
    {
        fullText = "Found " + itemName;

        if (PlayerStatsSO.percentageHuman > 50f)
        {
            myText.fontSize = 100f;
            fullText = fullText + "\n " + itemOdds.ToString() + "% chance to make you\nmore Human";
        }
        else if (PlayerStatsSO.percentageHuman < 50f)
        {
            myText.fontSize = 100f;
            fullText = fullText + "\n " + (100f - itemOdds).ToString() + "% chance to make you\nmore Animal";
        }
        else if(PlayerStatsSO.percentageHuman == 50f)
        {
            myText.fontSize = 73f;
            fullText = fullText + "\n You will be forever changed \n if you interact with this object";
        }

        myText.SetText(fullText); 
    }
}
