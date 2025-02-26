using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioStoryBehavior : MonoBehaviour
{
    [HideInInspector]
    public ArtefactSO triggeringArtefact;
    
    public GameObject ChoiceButtonsPrefab;
    private GameObject choiceButtonsParent;
    private GameObject myCanvas;
    private AudioSource currentAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        currentAudioSource = GetComponent<AudioSource>();
        myCanvas = FindFirstObjectByType<Canvas>().gameObject;

        StartCoroutine(ChoiceButtonWaiter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChoiceButtonWaiter()
    {
        yield return new WaitForSeconds(2f);

        yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.T) || currentAudioSource.isPlaying == false));

        choiceButtonsParent = Instantiate(ChoiceButtonsPrefab, myCanvas.transform);
        choiceButtonsParent.GetComponent<ChoiceButtonsBehavior>().UnlockCursorforStory();

        List<Button> choiceButtons = new List<Button>();
        choiceButtonsParent.GetComponentsInChildren(choiceButtons);
        choiceButtons[0].GetComponentInChildren<TMP_Text>().SetText(triggeringArtefact.humanButtonText);
        choiceButtons[1].GetComponentInChildren<TMP_Text>().SetText(triggeringArtefact.animalButtonText);

        Destroy(gameObject);
    }
}
