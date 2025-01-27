using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//this method not only handles actual changes to stats (easy access)
//it also handles what happens WHEN those stats change
public class PlayerStatsBehavior : MonoBehaviour
{
    public StatBlockSO myPlayerStats;

    public AudioSource playerAudioSource;

    //0-2 human (groan, bone crack, sigh), 3-5 beast (growl-whimper, fur puff, whine-yawn)
    public List<AudioClip> transformSounds;

    [Range(0.5f, 3f)]
    public float timeToTransform;


    //used to change colorspace at runtime
    public Volume GlobalVolume;
    public List<VolumeProfile> PlayerVolumes;


    private bool InterrimTimerRunning = false;

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
            if (myPlayerStats.timeInInterrimMinutes > 1 && myPlayerStats.timeInbetween % 60 == 0)
            {
                myPlayerStats.ChangePathMessage();
            }
        }
    }

    private void ChangeExperience(int fromStat, int changeByValue)
    {
        //adding more coroutines that'll go across everything according to stat change

        StartCoroutine(ImmediateEffects(fromStat, changeByValue));
       
        StartCoroutine(ScalePlayer(changeByValue));
        
        StartCoroutine(VolumeSpaceControl(fromStat, changeByValue));

        //StartCoroutine(AudioSpaceControl(fromStat, changeByValue));
    }

    //functionally complete - audio is imported all good
    IEnumerator ImmediateEffects(int fromStat, int changeVal)
    {
        //should play a bone crack or a fur sound, depending on up or down
        //should also change the walking sounds - 4 beast, 2 man, none for interrim
        //ACTUALLY GOING TO DO THAT VIA "MOVESOUNDMANAGER" - do not do above line here
        //these are like one time and immediate rather than something that happens over time a little bit
        if (fromStat <= -25)
        {
            playerAudioSource.PlayOneShot(transformSounds[0]);
            yield return new WaitForSeconds(transformSounds[0].length);

        }
        else if(fromStat >= 25)
        {
            playerAudioSource.PlayOneShot(transformSounds[3]);
            yield return new WaitForSeconds(transformSounds[3].length);
        }



        if (changeVal < 0) // becoming more human
        {
            playerAudioSource.PlayOneShot(transformSounds[1]);
            yield return new WaitForSeconds(transformSounds[1].length);
        }
        else if (changeVal > 0) //becoming more animal
        {
            playerAudioSource.PlayOneShot(transformSounds[4]);
            yield return new WaitForSeconds(transformSounds[4].length);
        }

        float finalStat = fromStat + changeVal;
        
        if (finalStat <= -25)
        {
            playerAudioSource.PlayOneShot(transformSounds[2]);
            yield return new WaitForSeconds(transformSounds[2].length);
        }
        else if (finalStat >= 25)
        {
            playerAudioSource.PlayOneShot(transformSounds[5]);
            yield return new WaitForSeconds(transformSounds[5].length);
        }

    }

    //this one works work on the next
    IEnumerator ScalePlayer(int changeVal)
    {
        //i think our minimum should be like 0.3m, and our max should be like 1.8m?
        //and we start at 1.2, so lets scale between that
        //for now just make it linear - shouldnt need initial value then, only the changeby (fraction of range 1.5)

        float totalScaleChange = (-changeVal / 200f) * 1.5f;
        float timer = 0f;
        float currentScale = transform.localScale.y;
        while (timer <= timeToTransform)
        {

            float newY = Mathf.Lerp(currentScale, currentScale + totalScaleChange, timer / timeToTransform);

            transform.localScale = new Vector3(1f, newY, 1f);

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        yield return new WaitForEndOfFrame();
        //not implemented yet - we're figuring out all the stuff that should change
    }

    //this now works, if a little bit snappy rather than smooth. no change until have time. 
    IEnumerator VolumeSpaceControl(int fromStat, int changeVal)
    {
        //This currently just changes when you reach the threshold, fix later to be smooth transitions
        // you'll have to change settings and lerp or slerp them, but this will work for now as a snappy transition
        //instead of a gradual smooth one. (so our first object DOES do this for now, but shouldnt later). 
        float finalStat = fromStat + changeVal;

        if (finalStat >= 25)
        {
            GlobalVolume.GetComponent<Volume>().profile = PlayerVolumes[1];
        }
        else if (finalStat <= -25)
        {
            GlobalVolume.GetComponent<Volume>().profile = PlayerVolumes[2];
        }
        else
        {
            GlobalVolume.GetComponent<Volume>().profile = PlayerVolumes[0];
        }


        //This controls the volume profiles in the game (color, effects, postprocessing)
        yield return new WaitForEndOfFrame();
        //not implemented yet - we're figuring out all the stuff that should change
    }

    //this needs more sounds to matter, dont do yet. 
    IEnumerator AudioSpaceControl(int fromStat, int changeVal)
    {
        //This should loop across all audio sources with the tagging
        //it should then turn things up, down, and/or change the radius of said sound (rebalancing/mastering basically)
        yield return new WaitForEndOfFrame();
        //not implemented yet - we're figuring out all the stuff that should change
    }




    //these are implemented for debugging stats and use in game: no args = set amount, args overload = use by item
    public void BeMoreHuman()
    {
        myPlayerStats.pathProgress -= 25;
        myPlayerStats.ChangePathMessage();
        ChangeExperience(myPlayerStats.pathProgress, -25);
    }

    public void BeMoreHuman(int value)
    {
        myPlayerStats.ProgressPath(value);
        ChangeExperience(myPlayerStats.pathProgress, -value);
    }

    public void DriftMore()
    {
        myPlayerStats.timeInbetween += 60;
        myPlayerStats.timeInInterrimMinutes += 1;
    }
    public void BeMoreAnimal()
    {
        myPlayerStats.pathProgress += 25;
        myPlayerStats.ChangePathMessage();
        ChangeExperience(myPlayerStats.pathProgress, 25);
    }

    public void BeMoreAnimal(int value)
    {
        myPlayerStats.ProgressPath(-value);
        ChangeExperience(myPlayerStats.pathProgress, value);
    }

    public void ResetStats()
    {
        myPlayerStats.pathProgress = 0;
        myPlayerStats.timeInbetween = 0;
        myPlayerStats.timeInInterrimMinutes = 0;
        myPlayerStats.ChangePathMessage();
    }
}
