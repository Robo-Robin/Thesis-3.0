using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFireDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetAllActive());
    }
    
    IEnumerator SetAllActive()
    {
        yield return new WaitForSeconds(35f);
        for (int i = 0; i <= transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);

        }
    }
}
