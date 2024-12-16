using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsBehavior : MonoBehaviour
{
    public StatBlockSO myPlayerStats;

    private bool InterrimTimerRunning = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayerStats.pathProgress > -25 && myPlayerStats.pathProgress < 25 && !InterrimTimerRunning)
        {
            InterrimTimerRunning = true;
            StartCoroutine(RunInterrimTimer());
        }
    }

    IEnumerator RunInterrimTimer()
    {
        while (InterrimTimerRunning && myPlayerStats.timeInbetween < (myPlayerStats.InterrimLimitInMinutes * 60))
        {
            yield return new WaitForSeconds(1f);
            myPlayerStats.timeInbetween += 1f;
            myPlayerStats.timeInInterrimMinutes = (int)(myPlayerStats.timeInbetween / 60);
        }
    }
}
