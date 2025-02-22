using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DriftEndManager : MonoBehaviour
{
    //this should be *relatively* simple - if only because whenever this thing is active
    // we're counting up towards the ending. 

    private GameObject _canvasObject;

    private SimplerPlayerController myPlayer;

    public GameObject DriftEndUI_BG_1;
    public GameObject DriftEndUI_BG_2;
    public GameObject DriftEndUI_BG_Final;

    bool phaseOneDone = false;
    bool phaseTwoDone = false;
    bool phaseThreeDone = false;


    [SerializeField]
    private int totalTimeActiveInMinutes;
    private float totalTimeActive;

    public bool timerIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<SimplerPlayerController>();
        _canvasObject = FindObjectOfType<Canvas>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //hard coding in the time limits to be 2, 4, and 5 for now
        //this should only become active once you leave the cave as well, its not *technically* related to the player stats yet
        if (!timerIsPaused)
        {
            totalTimeActive += Time.deltaTime;
            totalTimeActiveInMinutes = (int)totalTimeActive / 60;
        }

        //this section for expedited testing
        /*if (totalTimeActive >= 5f && phaseOneDone == false)
        {
            timerIsPaused = true;
            PhaseOnePopupExecute();
            phaseOneDone = true;
        }
        else if (totalTimeActive >= 10f && phaseTwoDone == false)
        {
            timerIsPaused = true;
            PhaseTwoPopupExecute();
            phaseTwoDone = true;
        }
        else if (totalTimeActive >= 15f && phaseThreeDone == false)
        {
            timerIsPaused = true;
            PhaseThreePopupExecute();
            phaseThreeDone = true;
        }*/

        //this section for playtesting
        if (totalTimeActiveInMinutes >= 2 && phaseOneDone == false)
        {
            timerIsPaused = true;
            PhaseOnePopupExecute();
            phaseOneDone = true;
        }
        else if (totalTimeActiveInMinutes >= 4 && phaseTwoDone == false)
        {
            timerIsPaused = true;
            PhaseTwoPopupExecute();
            phaseTwoDone = true;
        }
        else if (totalTimeActiveInMinutes >= 5 && phaseThreeDone == false)
        {
            timerIsPaused = true;
            PhaseThreePopupExecute();
            phaseThreeDone = true;
        }
        
        //this section for the "shipped version"
        /*if (totalTimeActiveInMinutes >= 4 && phaseOneDone == false)
        {
            timerIsPaused = true;
            PhaseOnePopupExecute();
            phaseOneDone = true;
        }
        else if (totalTimeActiveInMinutes >= 7 && phaseTwoDone == false)
        {
            timerIsPaused = true;
            PhaseTwoPopupExecute();
            phaseTwoDone = true;
        }
        else if (totalTimeActiveInMinutes >= 10 && phaseThreeDone == false)
        {
            timerIsPaused = true;
            PhaseThreePopupExecute();
            phaseThreeDone = true;
        }*/
    }

    void PhaseOnePopupExecute()
    {
        myPlayer.PlayerLock();
        myPlayer.UnlockCursor();

        Instantiate(DriftEndUI_BG_1, _canvasObject.transform);
    }

    void PhaseTwoPopupExecute()
    {
        myPlayer.PlayerLock();
        myPlayer.UnlockCursor();

        Instantiate(DriftEndUI_BG_2, _canvasObject.transform);
    }

    void PhaseThreePopupExecute()
    {
        myPlayer.PlayerLock();
        myPlayer.UnlockCursor();

        Instantiate(DriftEndUI_BG_Final, _canvasObject.transform);

    }

    public void UnpauseTimer()
    {
        myPlayer.PlayerUnlock();
        myPlayer.RelockCursor();
        timerIsPaused = false;
    }
}
