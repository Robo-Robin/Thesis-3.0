using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    bool doorMoving;

    AudioSource doorASource;

    [Range(100f, 200f)]
    public float doorOpenTime;
    
    // Start is called before the first frame update
    void Start()
    {
        doorASource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveDoor()
    {
        doorMoving = true;

        StartCoroutine(DoorMove());
    }

    IEnumerator DoorMove()
    {
        doorASource.Play();
        float doorTarget = 17f;
        float doorOriginal = transform.position.x;
        float timeSlerp = 0f;
        float distanceTraveled = 0f;

        while (doorMoving == true && distanceTraveled <= doorTarget - doorOriginal)
        {
            Vector3 newCurrentPosition = Vector3.Slerp(transform.position, new Vector3(doorTarget, transform.position.y, transform.position.z), timeSlerp);
            distanceTraveled = newCurrentPosition.x - doorOriginal;

            transform.position = newCurrentPosition;

            timeSlerp += Time.deltaTime / doorOpenTime;
            yield return new WaitForEndOfFrame();

            if (newCurrentPosition.x < 20 && newCurrentPosition.x > 19.98) //this is a catch to make sure that the slerp works
            {
                doorMoving = false;
            }

        }
        doorASource.Stop();
    }
}
