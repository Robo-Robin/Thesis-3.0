using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButtonsBehavior : MonoBehaviour
{

    private AudioSource UIAudio;

    public List<AudioClip> UIClips;
    //0 is a lil button click
    //1 is the take audio (shuffling into pockets)
    //2 is the eat audio (crompchy)



    // Start is called before the first frame update
    void Start()
    {
        UIAudio = GameObject.Find("UISoundManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RevealInteractionButtons()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void TakeObject()
    {
        UIAudio.PlayOneShot(UIClips[0]);
        StartCoroutine(UIAudioPlay(UIClips[1]));

        SimplerPlayerController playerController = FindObjectOfType<SimplerPlayerController>();
        playerController.RelockCursor();
    }
    public void EatObject()
    {
        UIAudio.PlayOneShot(UIClips[0]);
        StartCoroutine(UIAudioPlay(UIClips[2]));

        SimplerPlayerController playerController = FindObjectOfType<SimplerPlayerController>();
        playerController.RelockCursor();
    }

    IEnumerator UIAudioPlay(AudioClip clip)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1.8f);

        UIAudio.PlayOneShot(clip);


    }
}
