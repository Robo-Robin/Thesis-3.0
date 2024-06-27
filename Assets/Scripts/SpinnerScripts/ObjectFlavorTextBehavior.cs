using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectFlavorTextBehavior : MonoBehaviour
{
    private TMP_Text myText;
    private string fullText;

    public ObjectManagerBehavior myObjectManager;

    private void OnEnable()
    {
        myText = gameObject.GetComponentInChildren<TMP_Text>();
        SetText();
    }

    public void SetText()
    {
        if (myObjectManager.currentItemID != 0)
        {
            foreach (ItemSO item in myObjectManager.foundItems)
            {
                if (item.itemID == myObjectManager.currentItemID && myText.enabled == true)
                {
                    myText.fontSize = 70f;
                    /*currently this is just going to see the full text. we might need to modify this later
                    to actually fit the size of the text box and break it up into text files etc*/
                    fullText = "" + item.itemText;
                }
                else
                {
                    Debug.Log("this is not the right item");
                }
            }
        }

        myText.SetText(fullText);
    }
}
