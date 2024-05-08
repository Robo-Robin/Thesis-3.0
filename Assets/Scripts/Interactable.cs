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

    float revealingtimer;
    float revealingCooldown = 6f;

    public GameObject indicatorObject;
    MeshRenderer indicatorMesh;
    bool isIndicated;

    // Start is called before the first frame update
    void Start()
    {
        indicatorMesh = indicatorObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRaycastedOn && Input.GetKeyDown(interactionKey) && !isRevealed)
        {
            StartCoroutine(RevealInteraction());
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
        while(revealingtimer <= revealingCooldown)
        {
            revealingtimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        interactAction.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isRaycastedOn = false;
        }
    }
}
