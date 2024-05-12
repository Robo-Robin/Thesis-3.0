using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StatsSO", menuName = "ScriptableObjects/StatsSO", order = 1)]
public class StatsSO : ScriptableObject
{
    public float percentageHuman = 50f;
    public int itemsFound = 0;

    [TextArea]
    public string statusMessage;

    [HideInInspector]
    public int timeInInterrimMinutes;

    [HideInInspector]
    public List<string> nameOfObjectsFound = new List<string>();

    public void HumanityChanger(float changeby)
    {
        percentageHuman += changeby;
        percentageHuman = Mathf.Clamp(percentageHuman, 0f, 100f);
        MessageChanger();
    }

    public void MessageChanger()
    {
        if(percentageHuman >= 65f)
        {
            statusMessage = "You are primarily HUMAN";
        }
        else if (percentageHuman >= 35f)
        {
            statusMessage = "You float in the interrim";
        }
        else if (percentageHuman < 35f)
        {
            statusMessage = "You are primarily BEAST";
        }
    }

}
