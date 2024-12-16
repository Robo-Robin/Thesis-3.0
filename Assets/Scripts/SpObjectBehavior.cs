using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpObjectBehavior : MonoBehaviour
{
    MeshRenderer myMesh;

    public GameObject MeshContainerPrefab;

    public AudioSource myAudioSource;

    public List<AudioClip> myAudioClips;

    public GameObject ButtonsParent;
    TheChoiceBehavior HumanBeastButtonsParent;

    // Start is called before the first frame update
    void Start()
    {
        if (MeshContainerPrefab == null)
        {
           myMesh = GetComponent<MeshRenderer>();
            if (myMesh != null)
                Debug.Log("Mesh obtained");
            else
                Debug.Log("failed to get mesh");
        }
        HumanBeastButtonsParent = ButtonsParent.GetComponent<TheChoiceBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealObject()
    {
        if (myAudioClips[0] != null)
        {
            myAudioSource.PlayOneShot(myAudioClips[0]); //reveal ditty
        }

        if (myMesh == null)
        {
            MeshContainerPrefab.SetActive(true);
        }
        else
        {
            myMesh.enabled = true;
        }



    }

    public void TakeObject()
    {
        if (myMesh == null)
        {
            MeshContainerPrefab.SetActive(false);
        }
        else
        {
            myMesh.enabled = false;
        }

    }

    public void PlayAudioStory()
    {
        StartCoroutine(AudioWaiter());
        
    }

    IEnumerator AudioWaiter()
    {
        yield return new WaitForSeconds(3f);
        if (myAudioClips[1] != null)
        {
            myAudioSource.PlayOneShot(myAudioClips[1]);
            StartCoroutine(HumanBeast(myAudioClips[1].length));
        }
    }

    IEnumerator HumanBeast(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (HumanBeastButtonsParent != null)
        {   
            
            HumanBeastButtonsParent.RevealChoiceUI();
        }
        else Debug.Log("Unable to get the buttons");

    }
}
