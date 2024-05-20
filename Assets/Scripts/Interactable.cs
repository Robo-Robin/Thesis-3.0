using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public KeyCode interactionKey;
    public bool isRaycastedOn;
    bool isRevealed;
    public UnityEvent interactAction;

    public UnityEvent alreadyRevealed;

    float revealingtimer;
    float revealingCooldown = 6f;

    public GameObject indicatorObject;
    MeshRenderer indicatorMesh;
    bool isIndicated;

    public int itemID;

    public ObjectManagerBehavior myObjectManager;

    // Start is called before the first frame update
    void Start()
    {
        indicatorMesh = indicatorObject.GetComponent<MeshRenderer>();

        myObjectManager = gameObject.GetComponentInParent<ObjectManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRaycastedOn && Input.GetKeyDown(interactionKey) && !isRevealed)
        {
            StartCoroutine(RevealInteraction());
        }
        else if (isRaycastedOn && Input.GetKeyDown(interactionKey) && isRevealed)
        {
            revealingtimer = 0f;
            StartCoroutine(AlreadyRevealedInteraction());
        }

        if(isRaycastedOn && !isIndicated)
        {
            isIndicated = true;
            indicatorMesh.enabled = true;
        }
        else if (!isRaycastedOn && isIndicated)
        {
            isIndicated = false;
            indicatorMesh.enabled = false;
        }

    }

    IEnumerator RevealInteraction()
    {
        isRevealed = true;
        while(revealingtimer <= revealingCooldown)
        {
            revealingtimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetCurrentItemFound();
        interactAction.Invoke();
    }
    
    IEnumerator AlreadyRevealedInteraction()
    {
        while(revealingtimer <= revealingCooldown)
        {
            revealingtimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        alreadyRevealed.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isRaycastedOn = false;
        }
    }

    public void SetCurrentItemFound()
    {
        myObjectManager.currentItemID = itemID;
    }
}
