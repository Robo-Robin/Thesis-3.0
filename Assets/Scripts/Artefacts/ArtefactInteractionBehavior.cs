using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ArtefactInteractionBehavior : MonoBehaviour
{
    MeshRenderer myMesh;

    public GameObject ArtefactMeshContainerPrefab;
    private GameObject ArtefactMesh;

    private GameObject myCanvas;
    public GameObject InteractUIParentPrefab;
    private GameObject interactButtonsParent;

    private SimplerPlayerController myPlayerController;

    public bool isRaycastedOn;

    public GameObject indicatorObject;
    MeshRenderer indicatorMesh;
    bool isIndicated;

    public bool diggable;

    public GameObject moundMesh;
    public ParticleSystem destructionParticles;

    public ArtefactSO containedArtefact;

    public UnityEvent interactAction;

    private AudioSource ArtefactAudioSource;
    public AudioClip findJingle;

    // Start is called before the first frame update
    void Start()
    {
        ArtefactAudioSource = GetComponent<AudioSource>();
        
        if (ArtefactMeshContainerPrefab == null)
        {
            myMesh = GetComponent<MeshRenderer>();
            if (myMesh != null)
                Debug.Log("Mesh obtained");
            else
                Debug.Log("failed to get mesh");
        }
        indicatorMesh = indicatorObject.GetComponent<MeshRenderer>();
        isIndicated = false;
        diggable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRaycastedOn && !isIndicated)
        {
            isIndicated = true;
            indicatorMesh.enabled = true;
        }
        else if (!isRaycastedOn && isIndicated)
        {
            isIndicated = false;
            indicatorMesh.enabled = false;
        }

        if (diggable == false)
        {
            gameObject.tag = "Undiggable";
        }
    }

    public void TakeObject()
    {
        if (myMesh != null)
        {
            ArtefactMeshContainerPrefab.SetActive(false);
        }
        
    }

    public void EatObject()
    {
        DigUp();
        StartCoroutine(DestroyObjectWithParticles());

    }

    IEnumerator DestroyObjectWithParticles()
    {
        destructionParticles.Play();
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    public void DigUp()
    {
        moundMesh.SetActive(false); //make this a coroutine with a timer
        /*RevealObject();*/ // same with this, possibly just make them the same coroutine. 
        indicatorMesh.enabled = false;
        diggable = false;
    }

    public void RevealObject()
    {
        ArtefactMesh = Instantiate(ArtefactMeshContainerPrefab, Vector3.zero + gameObject.transform.position, Quaternion.Euler(45f, 45f, 0f), gameObject.transform);
        ArtefactMesh.gameObject.tag = "Undiggable";
        ArtefactMesh.transform.SetParent(null);

        ArtefactAudioSource.loop = false;
        ArtefactAudioSource.PlayOneShot(findJingle);
    }

    public void MakeInteractionButtons()
    {
        myCanvas = FindFirstObjectByType<Canvas>().gameObject;


        interactButtonsParent = Instantiate(InteractUIParentPrefab, myCanvas.transform);

        interactButtonsParent.GetComponent<InteractButtonsBehavior>().triggeringArtefact = containedArtefact;

        GameObject description = GameObject.Find("ArtefactText");

        description.GetComponent<TMP_Text>().fontSize = 30;
        description.GetComponent<TMP_Text>().SetText(containedArtefact.artefactDescription);

        List<Button> interactionButtons = new List<Button>();
        interactButtonsParent.GetComponentsInChildren(interactionButtons);
        interactionButtons[0].GetComponentInChildren<TMP_Text>().SetText(containedArtefact.takeButtonText);
        interactionButtons[1].GetComponentInChildren<TMP_Text>().SetText(containedArtefact.eatButtonText);

        CursorAttractor sniffer = FindAnyObjectByType<CursorAttractor>();
        sniffer.OnTriggerExit(gameObject.GetComponent<Collider>());

        StartCoroutine(DestroyAfterSeconds());

    }
    
    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public void DestroyInteractionButtons()
    {
        Destroy(interactButtonsParent);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isRaycastedOn = false;
        }
    }
}
