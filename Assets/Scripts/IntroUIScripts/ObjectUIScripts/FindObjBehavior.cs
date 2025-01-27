using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class FindObjBehavior : MonoBehaviour
{
    public AudioSource UIAudio;

    public List<AudioClip> UIClips;
    //0 is a lil button click
    //1 is the take audio (shuffling into pockets)
    //2 is the eat audio (crompchy)

    public ParticleSystem turnToDustSystem;


    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealInitUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void TakeInitObject()
    {
        UIAudio.PlayOneShot(UIClips[0]);
        StartCoroutine(UIAudioPlay(UIClips[1]));
    }
    public void EatInitObject()
    {
        UIAudio.PlayOneShot(UIClips[0]);
        turnToDustSystem.Play();
        StartCoroutine(UIAudioPlay(UIClips[2]));
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
