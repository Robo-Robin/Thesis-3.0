using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneManagerBehavior : MonoBehaviour
{

    public GameObject eventManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

    public void CaveExitToDesert()
    {
        StartCoroutine(LoadDesert());
    }

    IEnumerator LoadDesert()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TheDesert", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        UnloadPreviousSceneManager();
    }
    
    public void MainMenuToCave()
    {
        StartCoroutine(LoadCave());
    }

    IEnumerator LoadCave()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("EntryCave", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        AsyncOperation asyncUnLoad = SceneManager.UnloadSceneAsync("MenuScene");
    }

    private void UnloadPreviousSceneManager()
    {
        Destroy(eventManager);
        Destroy(this);
    }


}
