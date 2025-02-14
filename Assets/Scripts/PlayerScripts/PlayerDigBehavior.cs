using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDigBehavior : MonoBehaviour
{
    private float _XRotation;

    private SimplerPlayerController simplerPlayerController;
    public Camera playerCamera;

    float digMinimumAngle = 41.6f;
    RaycastHit hit;

    bool canDig;
    float digCooldown = 6;
    float digCooldownTimer;
    public bool isDigging;

    bool rayAtObject = false;

    public GameObject digObject;

    AudioSource playerAudioSource;
    public List<AudioClip> digAudClips;

    public PlayerHowl myPlayerHowl;
    public CursorAttractor myPlayerSniff;

    // Start is called before the first frame update
    void Start()
    {
        simplerPlayerController = gameObject.GetComponent<SimplerPlayerController>();
  /*      playerCamera = Camera.main;*/
        _XRotation = simplerPlayerController.currentXRotation;

        playerAudioSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        

        _XRotation = simplerPlayerController.currentXRotation;

        if (_XRotation >= digMinimumAngle && isDigging == false && !simplerPlayerController.playerIsLocked)
        {
            if (simplerPlayerController.playerIsLocked)
            {
                canDig = false;
            }
            else if (myPlayerSniff.isSniffing || myPlayerHowl.isHowling)
            {
                canDig = false;
            }
            else
            {
                canDig = true;
            }

            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100f))
            {
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 100.0f, Color.red);

                if (hit.collider.gameObject.tag == "Object")
                {
                    hit.transform.gameObject.GetComponent<Interactable>().isRaycastedOn = true;

                    rayAtObject = true;
                }
                else if (hit.collider.gameObject.tag == "SpObject")
                {
                    hit.transform.gameObject.GetComponent<SpInteractable>().isRaycastedOn = true;
                    rayAtObject = true;
                }
                else if(hit.collider.gameObject.tag == "Artefact")
                {
                    hit.transform.gameObject.GetComponent<ArtefactInteractionBehavior>().isRaycastedOn = true;

                    rayAtObject = true;
                }
                else if (hit.collider.gameObject.tag == "Undiggable")
                {
                    canDig = false;
                    //add a lil error sound if you click and cant dig
                }
            }
        }
        else
        {
            canDig = false;
        }

        if(Input.GetKey(KeyCode.Mouse1) && canDig)
        {

            isDigging = true;

            //currently only play the 1 clip for digging - we'll have a couple eventually i think
            if (digAudClips != null)
            {
                playerAudioSource.PlayOneShot(digAudClips[0]);
            }

            if (rayAtObject)
            {
                simplerPlayerController.UnlockCursor();
                StartCoroutine(CreateDigMesh(hit.transform.position.y - 0.9f));

                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100f) && hit.collider.gameObject.tag == "Artefact")
                {
                    hit.transform.gameObject.GetComponent<ArtefactInteractionBehavior>().interactAction.Invoke();
                }
            }
            else
            {
                StartCoroutine(simplerPlayerController.TempLockPlayer(4f));
                StartCoroutine(CreateDigMesh(0f));
            }
                
        }

    }

    IEnumerator CreateDigMesh(float height)
    {
        yield return new WaitForSeconds(3f);
        Instantiate(digObject, hit.point + new Vector3(0f, - height, 0f), Quaternion.Euler(-90, 0, 0));
        if (!rayAtObject)
        {
            simplerPlayerController.RelockCursor();
        }
        playerAudioSource.Stop();
        isDigging = false;
    }
}
