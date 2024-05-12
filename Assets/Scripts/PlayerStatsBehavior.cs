using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsBehavior : MonoBehaviour
{

    public StatsSO myStats;

    public PlayerController myPlayerController;

    public void ChangeLookBounds()
    {
        //default is -45f for lookupbounds
        //default for snap is -15f
        if (myStats.percentageHuman < 65f && myStats.percentageHuman > 35f)
        {
            myPlayerController.lookUpwardsLimit = -45f;
            transform.localScale = new Vector3(0f, 0.52f, 0f);
        }
        else if (myStats.percentageHuman >= 65f)
        {
            myPlayerController.lookUpwardsLimit -= myStats.percentageHuman - 55f;
            transform.localScale += new Vector3(0f, 0.15f, 0f);
        }
        else if (myStats.percentageHuman <= 35f)
        {
            myPlayerController.lookUpwardsLimit -= (-4f / 7f * myStats.percentageHuman) - 15f;
            transform.localScale += new Vector3(0f, -0.15f, 0f);
        }
    }

    public void AddItemCount()
    {
        myStats.itemsFound++;
    }
}
