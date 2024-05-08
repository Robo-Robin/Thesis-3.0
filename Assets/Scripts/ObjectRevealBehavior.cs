using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRevealBehavior : MonoBehaviour
{

    MeshRenderer myMesh;



    // Start is called before the first frame update
    void Start()
    {
        myMesh = GetComponent<MeshRenderer>();
        if (myMesh != null)
            Debug.Log("Mesh obtained");
        else
            Debug.Log("failed to get mesh");

        
    }

    public void RevealObject()
    {
        myMesh.enabled = true;

    }

    public void IndicatorBehavior()
    {
        
    }
}
