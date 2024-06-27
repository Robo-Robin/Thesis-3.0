using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    //This is pretty much for every NPC, so assign in editor and then create prefab from it when it's assigned. 
    public PlayerController myPlayer;

    public GameObject DialogueUI;

    //this field might be useful for testing dialogue/feeding into a csv reader. 
    public int npcID;

    //change to private later
    public bool interactable;
    public bool interacting;

    private void OnEnable()
    {
        interactable = false;
        //this is where we'll need to instantiate most things. we'll create the echo of this in "start" for now
    }

    private void Start()
    {
        
    }

    public void StartDialogue()
    {
        for (int i = 0; i < DialogueUI.transform.childCount; i++)
        {
            DialogueUI.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void EndDialogue()
    {
        for (int i = 0; i < DialogueUI.transform.childCount; i++)
        {
            DialogueUI.transform.GetChild(i).gameObject.SetActive(false);
        }
        interacting = false;
    }

    private void TalkToMe()
    {
        myPlayer.PlayerLock();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactable = false;
        }
    }
}
