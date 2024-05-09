using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsUI : MonoBehaviour
{
    public ResolveBehavior myResolveBehavior;

    public PlayerController myPlayerController;

    // Update is called once per frame
    void Update()
    {
        if (myResolveBehavior.spinnerIsResolved && Input.GetKeyDown(KeyCode.E))
        {
            HideResults();
        }
    }

    public void ShowResults()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void HideResults()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        myPlayerController.canMove = true;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
