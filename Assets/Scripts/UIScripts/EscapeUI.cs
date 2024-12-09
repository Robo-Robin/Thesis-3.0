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
            myPlayerController.PlayerLock();
            StartPauseMenu();
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        myPlayerController.PlayerUnlock();
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
