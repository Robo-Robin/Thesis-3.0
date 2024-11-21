using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDigBehavior : MonoBehaviour
{
    private float _XRotation;

    private SimplerPlayerController simplerPlayerController;
    private Camera playerCamera;

    float digMinimumAngle = 41.6f;
    RaycastHit hit;

    bool canDig;
    float digCooldown = 6;
    float digCooldownTimer;
    bool isDigging;

    // Start is called before the first frame update
    void Start()
    {
        simplerPlayerController = gameObject.GetComponent<SimplerPlayerController>();
        playerCamera = Camera.main;
        _XRotation = simplerPlayerController.currentXRotation;
    }

    // Update is called once per frame
    void Update()
    {
        _XRotation = simplerPlayerController.currentXRotation;

        if (_XRotation >= digMinimumAngle && isDigging == false && !simplerPlayerController.playerIsLocked)
        {
            canDig = true;

            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100f))
            {
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 100.0f, Color.red);

                if (hit.collider.gameObject.tag == "Object")
                {
                    hit.transform.gameObject.GetComponent<Interactable>().isRaycastedOn = true;
                }
                else if (hit.collider.gameObject.tag == "SpObject")
                {
                    hit.transform.gameObject.GetComponent<SpInteractable>().isRaycastedOn = true;
                }
            }
        }
        else
        {
            canDig = false;
        }

    }
}
