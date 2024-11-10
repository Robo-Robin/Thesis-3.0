using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAttractor : MonoBehaviour
{
    public bool attractionActive;

    public SimplerPlayerController myPlayerController;
    Vector3 playerPosition;

    [Range(0.0f, 1f)]
    public float LookAtSpeed;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (attractionActive && myPlayerController != null)
        {
            Debug.Log("move that camera");
            playerPosition = myPlayerController.gameObject.transform.position;

            //reminder X = up and down CAMERA rotation
            //Y = left right ACTUAL Player rotation
            myPlayerController.wantedXRotation += (playerPosition - transform.position).normalized.y * LookAtSpeed;
            //ok i think i know why this doesnt work. currently this is taking an actual vector
            //and applying it every frame - it doesnt actually say "go to this position, its scaled along
            //WHERE the object is, not like. TO the object. honestly, you should test this with just
            //moving the camera and player directly rather than trying to get double work out of your
            //smoothing function

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        attractionActive = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        attractionActive = false;
    }
}
