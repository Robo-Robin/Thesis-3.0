using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpObjectBehavior : MonoBehaviour
{
    MeshRenderer myMesh;

    public GameObject MeshContainerPrefab;

    AudioSource myAudioSource;

    AudioClip myAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        if (MeshContainerPrefab == null)
        {
           myMesh = GetComponent<MeshRenderer>();
            if (myMesh != null)
                Debug.Log("Mesh obtained");
            else
                Debug.Log("failed to get mesh");
        }


        myAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealObject()
    {
        if(myMesh == null)
        {
            MeshContainerPrefab.SetActive(true);
        }
        else
        {
            myMesh.enabled = true;
        }
    }

    public void TakeObject()
    {
        if (myMesh != null)
        {
            MeshContainerPrefab.SetActive(false);
        }
        else
        {
            myMesh.enabled = false;
        }
    }
}
