using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public GameObject audioStoryContainer;
    private GameObject audioStoryContainerObject;

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

        playerController.GetComponent<PlayerStatsBehavior>().AddArtefactToTakenList(triggeringArtefact.artefactName);

        if (triggeringArtefact.a_Type == ArtefactSO.StoryType.Audio)
        {
            audioStoryContainerObject = Instantiate(audioStoryContainer, myCanvas.transform);
            audioStoryContainerObject.GetComponent<AudioStoryBehavior>().triggeringArtefact = triggeringArtefact;
            audioStoryContainerObject.GetComponent<AudioSource>().PlayOneShot(triggeringArtefact.artefactAudioStory);
        }
        else if (triggeringArtefact.a_Type == ArtefactSO.StoryType.Text)
        {
            choiceButtonsParent = Instantiate(ChoiceButtonsPrefab, myCanvas.transform);
            choiceButtonsParent.GetComponent<ChoiceButtonsBehavior>().UnlockCursorforStory();

            List<Button> choiceButtons = new List<Button>();
            choiceButtonsParent.GetComponentsInChildren(choiceButtons);
            choiceButtons[0].GetComponentInChildren<TMP_Text>().SetText(triggeringArtefact.humanButtonText);
            choiceButtons[1].GetComponentInChildren<TMP_Text>().SetText(triggeringArtefact.animalButtonText);

            textStoryContainerObject = Instantiate(textStoryContainer, myCanvas.transform);

            textStoryContainerObject.GetComponentInChildren<TMP_Text>().SetText(triggeringArtefact.artefactTextStory.text);
            
        }
        else if (triggeringArtefact.a_Type == ArtefactSO.StoryType.HTML)
        {
            choiceButtonsParent = Instantiate(ChoiceButtonsPrefab, myCanvas.transform);
            choiceButtonsParent.GetComponent<ChoiceButtonsBehavior>().UnlockCursorforStory();

            List<Button> choiceButtons = new List<Button>();
            choiceButtonsParent.GetComponentsInChildren(choiceButtons);
            choiceButtons[0].GetComponentInChildren<TMP_Text>().SetText(triggeringArtefact.humanButtonText);
            choiceButtons[1].GetComponentInChildren<TMP_Text>().SetText(triggeringArtefact.animalButtonText);
            Debug.Log("this has gone through to this point");
            Application.OpenURL(Application.streamingAssetsPath + "/" + triggeringArtefact.artefactHTML_path);
            Debug.Log(Application.streamingAssetsPath + "/" + triggeringArtefact.artefactHTML_path);
        }
    }
    public void EatObject()
    {
        SimplerPlayerController playerController = FindObjectOfType<SimplerPlayerController>();
        playerController.GetComponent<PlayerStatsBehavior>().AddArtefactToEatenList(triggeringArtefact.artefactName);

        UIAudio.PlayOneShot(UIClips[0]);
        StartCoroutine(UIAudioPlay(UIClips[2]));

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
