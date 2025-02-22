using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstErrorCloser : MonoBehaviour
{
    public void CloseThis()
    {
        FindObjectOfType<DriftEndManager>().UnpauseTimer();
        Destroy(gameObject);
    }
}
