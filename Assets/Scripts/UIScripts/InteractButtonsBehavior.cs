using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractButtonsBehavior : MonoBehaviour
{

    private AudioSource UIAudio;

    public List<AudioClip> UIClips;
    //0 is a lil button click
    //1 is the take audio (shuffling into pockets)
    //2 is the eat audio (crompchy)

    public GameObject ChoiceButtonsPrefab;
    private GameObject choiceButtonsParent;
    private GameObject myCanvas;

    [HideInInspector]
    public ArtefactSO triggeringArtefact;

    public GameObject textStoryContainer;
    private GameObject textStoryContainerObject;

    // Start is called before the first frame update
    void Start()
    {
        UIAudio = GameObject.Find("UISoundManager").GetComponent<AudioSource>();

        myCanvas = FindFirstObjectByType<Canvas>().gameObject;
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

        if (triggeringArtefact.a_Type == ArtefactSO.StoryType.Audio)
        {

        }
        else if (triggeringArtefact.a_Type == ArtefactSO.StoryType.Text)
        {
            choiceButtonsParent = Instantiate(ChoiceButtonsPrefab, myCanvas.transform);
            choiceButtonsParent.GetComponent<ChoiceButtonsBehavior>().UnlockCursorforStory();

            textStoryContainerObject = Instantiate(textStoryContainer, myCanvas.transform);

            textStoryContainerObject.GetComponentInChildren<TMP_Text>().SetText(triggeringArtefact.artefactTextStory.text);
            
        }
    }
    public void EatObject()
    {
        UIAudio.PlayOneShot(UIClips[0]);
        StartCoroutine(UIAudioPlay(UIClips[2]));

        SimplerPlayerController playerController = FindObjectOfType<SimplerPlayerController>();
        playerController.RelockCursor();

    }

    public void EatChoice()
    {
        choiceButtonsParent = Instantiate(ChoiceButtonsPrefab, myCanvas.transform);


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
