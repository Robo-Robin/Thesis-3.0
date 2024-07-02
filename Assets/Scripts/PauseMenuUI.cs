using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenuUI : MonoBehaviour
{
    [HideInInspector]
    public bool isPaused;

    public StatsSO PlayerStats;
    public PlayerController myPlayer;

    public GameObject ItemCountObject;
    private TMP_Text itemCountText;

    void Start()
    {
        itemCountText = ItemCountObject.GetComponent<TMP_Text>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            myPlayer.PlayerLock();
            StartPauseMenu();
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            EndPauseMenu();
            isPaused = false;
            myPlayer.PlayerUnlock();
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
        SetItemsFound();
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

        myPlayer.PlayerUnlock();
    }

    void SetItemsFound()
    {
        itemCountText.SetText("# of Items Found: " + PlayerStats.itemsFound.ToString()) ;
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
