using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeUI : MonoBehaviour
{
    [HideInInspector]
    public bool isPaused;

    public SimplerPlayerController myPlayerController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            //added these lines to return the player to the same state pre and post pause

            StartPauseMenu();
            isPaused = true;


        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {

            if (!myPlayerController.playerIsLocked)
            {
                myPlayerController.PlayerUnlock();
            }
            EndPauseMenu();
            isPaused = false;

        }


    }

    public void StartPauseMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void EndPauseMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        isPaused = false;
        Time.timeScale = 1;
        
        if (!myPlayerController.playerIsLocked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
            

        
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
