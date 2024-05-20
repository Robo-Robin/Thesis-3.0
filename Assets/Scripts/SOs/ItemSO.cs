using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Items/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public int itemID;

    public string itemName;

    [TextArea]
    public string itemText;

    [TextArea]
    public List<string> dialogueStringSet = new List<string>();

    //always from 0 is all the way beast, 100 is all the way human. 
    public float oddsHumanAnimal;


}
