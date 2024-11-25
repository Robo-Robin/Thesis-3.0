using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAttractor : MonoBehaviour
{
    public bool attractionActive;

    public SimplerPlayerController myPlayerController;
    Vector3 playerPosition;

    [Range(0.0f, 100f)]
    public float LookAtSpeed;


    public GameObject CurrentTarget;

    List<GameObject> AllObjInRadius = new List<GameObject>(); //we're not going to be updating distances - essentially we're doing first in first out

    private SphereCollider mySmellRadius;


    void Start()
    {
        mySmellRadius = GetComponent<SphereCollider>(); //we'll update this radius around as we get more/less coyote

    }

    // Update is called once per frame
    void Update()
    {
        if (AllObjInRadius.Count > 0)
        {
            attractionActive = true;
        }
        else
        {
            attractionActive = false;
        }

        if (attractionActive)
        {
            Vector3 playerForward = myPlayerController.transform.TransformDirection(Vector3.forward);   
            Vector3 playerLeft = myPlayerController.transform.TransformDirection(Vector3.left);

            CurrentTarget = AllObjInRadius[0];
            Vector3 lookatDirection = (CurrentTarget.transform.position - myPlayerController.transform.position).normalized;

            float forwardComparitor = Vector3.Dot(playerForward, lookatDirection);

            float leftComparitor = Vector3.Dot(playerLeft, lookatDirection);

            myPlayerController.wantedYRotation -= LookAtSpeed * leftComparitor;


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SpObject" || other.tag == "Object")
        {
            AllObjInRadius.Add(other.gameObject);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SpObject" || other.tag == "Object")
        {
            AllObjInRadius.Remove(other.gameObject);
        }
    }

}
