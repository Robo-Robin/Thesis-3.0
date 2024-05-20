using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerBehavior : MonoBehaviour
{
    public List<ItemSO> possibleItems = new List<ItemSO>();
    
    public List<ItemSO> foundItems = new List<ItemSO>();

    public int currentItemID;

    public DescText myDescriptionText;
    public void FindItem()
    {
        foreach (ItemSO item in possibleItems)
        {
            if (item.itemID == currentItemID)
            {
                foundItems.Add(item);
                Debug.Log("Added item to found list");
            }
            else
            {
                Debug.Log("item does not exist/item already found");
            }
        }
    }

    public void SetDescriptionText()
    {
        foreach (ItemSO item in possibleItems)
        {
            if (item.itemID == currentItemID && myDescriptionText.enabled == true)
            {
                myDescriptionText.SetText(item.itemName, item.oddsHumanAnimal);
                Debug.Log("Added item to found list");
            }
            else
            {
                Debug.Log("item does not exist/item already found");
            }
        }
    }

}
