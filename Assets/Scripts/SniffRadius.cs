using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniffRadius : MonoBehaviour
{

    public PlayerController myPlayerController;

    //2 is far, 3 is close
    public int whichSniff = 2;

    private List<Transform> ObjectsWithinRange = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        myPlayerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayerController.currentXRotation > myPlayerController.sniffMinAngle && myPlayerController.isSniffing == false)
        {
            myPlayerController.isSniffing = true;
            myPlayerController.playerAudioSource.clip = myPlayerController.playerOneshots[whichSniff];
            myPlayerController.playerAudioSource.loop = true;
            if (myPlayerController.playerAudioSource.isPlaying == false)
            {
                myPlayerController.playerAudioSource.Play();
            }
        }
        else if (myPlayerController.currentXRotation < myPlayerController.sniffMinAngle && myPlayerController.isSniffing == true)
        {
            myPlayerController.playerAudioSource.Stop();
            myPlayerController.isSniffing = false;
            myPlayerController.playerAudioSource.loop = false;
            myPlayerController.playerAudioSource.clip = myPlayerController.playerOneshots[0];

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Object")
        {
            ObjectsWithinRange.Add(other.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        float closestObjectDistance = 0f;
        Vector3 closestObjectVector = new Vector3(0f, 0f, 0f);
        if (ObjectsWithinRange.Count >= 1)
        {
            foreach (Transform T in ObjectsWithinRange)
            {
                closestObjectVector = T.position - transform.position;
                closestObjectDistance = closestObjectVector.magnitude;

            }
        }
        else
            closestObjectDistance = 0f;

        if (myPlayerController.isSniffing && closestObjectDistance != 0f)
        {
            myPlayerController.playerAudioSource.volume = 0.3f + (20f - closestObjectDistance) / 20f;
        }
        else if (myPlayerController.isSniffing == true && closestObjectDistance == 0f)
            myPlayerController.playerAudioSource.volume = 0.3f;



        if (closestObjectVector.magnitude > 0)
        {
            if (closestObjectDistance <= 7f)
            {
                Debug.Log("Object is close");
                if(whichSniff != 3 && myPlayerController.isSniffing == true)
                {
                    myPlayerController.playerAudioSource.Stop();
                    whichSniff = 3;
                    myPlayerController.playerAudioSource.clip = myPlayerController.playerOneshots[whichSniff];
                    if (myPlayerController.playerAudioSource.isPlaying == false)
                    {
                        myPlayerController.playerAudioSource.Play();
                    }
                }
            }
            else if (closestObjectDistance <= 17f)
            {
                Debug.Log("Object is far");
                if (whichSniff != 2 && myPlayerController.isSniffing == true)
                {
                    myPlayerController.playerAudioSource.Stop();
                    whichSniff = 2;
                    myPlayerController.playerAudioSource.clip = myPlayerController.playerOneshots[whichSniff];
                    if (myPlayerController.playerAudioSource.isPlaying == false)
                    {
                        myPlayerController.playerAudioSource.Play();
                    }
                    
                }
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        ObjectsWithinRange.Remove(other.transform);
    }
}
