using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public StatBlockSO playerStats;

    //next up
    public GameObject humanEndingManager;
    public GameObject animalEndingManager;
    //implementing/ed
    public GameObject driftingEndingManager;


    //-1 as base, 0 as human, 1 as animal, 2 is drifting
    public int endingActive = -1;

    private void Start()
    {
        //these should all be inactive on start, so i shouldnt HAVE to set them to be inactive but we'll see. 
                
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.pathChosen == "Mostly Human" || playerStats.pathChosen == "Mostly Beast" || playerStats.pathChosen == "Drifting")
        {
            endingActive = 2;
        }
        else if (playerStats.pathChosen == "Human")
        {
            endingActive = 0;
        }
        else if (playerStats.pathChosen == "Beast")
        {
            endingActive = 1;
        }

        //we're only implementing drifting for now, we'll implement human and beast in a bit
        //this little section just sets them active or not though
        if (endingActive == 2)
        {
            humanEndingManager.SetActive(false);
            animalEndingManager.SetActive(false);
            driftingEndingManager.SetActive(true);
        }
        else if (endingActive == 1)
        {
            humanEndingManager.SetActive(false);
            animalEndingManager.SetActive(true);
            driftingEndingManager.SetActive(false);
        }
        else if (endingActive == 0)
        {
            humanEndingManager.SetActive(true);
            animalEndingManager.SetActive(false);
            driftingEndingManager.SetActive(false);
        }
        else
        {
            humanEndingManager.SetActive(false);
            animalEndingManager.SetActive(false);
            driftingEndingManager.SetActive(false);
        }
    }
}
