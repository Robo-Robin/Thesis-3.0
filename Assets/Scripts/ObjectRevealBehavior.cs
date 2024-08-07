using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRevealBehavior : MonoBehaviour
{

    MeshRenderer myMesh;

    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myMesh = GetComponent<MeshRenderer>();
        if (myMesh != null)
            Debug.Log("Mesh obtained");
        else
            Debug.Log("failed to get mesh");

        myAudioSource = GetComponent<AudioSource>();
        
    }

    public void RevealObject()
    {
        myMesh.enabled = true;
        myAudioSource.PlayOneShot(myAudioSource.clip);
    }

}
