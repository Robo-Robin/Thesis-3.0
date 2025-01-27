using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> movementAudioClips = new List<AudioClip>();
    // 0 is walking, 1 is running, 2 is the end shift
    // Start is called before the first frame update
    [HideInInspector]
    public AudioSource movementAudioSource;
    public SimplerPlayerController myPlayerController;

    void Start()
    {
        movementAudioSource = GetComponent<AudioSource>();

        //player controller is assigned in editor
    }

    // Update is called once per frame
    void Update()
    {
     
        //WE SHOULD BE HANDLING THIS VIA COROUTINE - 1/21/2025
        
        /*if (myPlayerController != null)
        {
            
            if((myPlayerController.curSpeedX != 0 || myPlayerController.curSpeedY != 0) && myPlayerController.isRunning == false)
            {
                movementAudioSource.clip = movementAudioClips[0];
                movementAudioSource.mute = false;
                Debug.Log("walking is playing?: " + movementAudioSource.isPlaying);
            }
            else if((myPlayerController.curSpeedX != 0 || myPlayerController.curSpeedY != 0) && myPlayerController.isRunning == true)
            {
                movementAudioSource.clip = movementAudioClips[1];
                movementAudioSource.mute = false;
                Debug.Log("running is playing?: " + movementAudioSource.isPlaying);
            }
            else
            {
                movementAudioSource.mute = true;
            }
            if (movementAudioSource.clip != null && movementAudioSource.isPlaying == false)
            {
                movementAudioSource.Play();
            }
        }*/
    }
}
