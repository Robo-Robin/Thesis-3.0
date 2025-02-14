using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButtonsBehavior : MonoBehaviour
{

    [HideInInspector]
    public ArtefactSO triggeringArtefact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChoiceMadeHuman()
    {
        SimplerPlayerController playerController = FindObjectOfType<SimplerPlayerController>();

        playerController.gameObject.GetComponent<PlayerStatsBehavior>().BeMoreHuman(25);

        playerController.RelockCursor();
    }
    public void ChoiceMadeAnimal()
    {
        SimplerPlayerController playerController = FindObjectOfType<SimplerPlayerController>();

        playerController.gameObject.GetComponent<PlayerStatsBehavior>().BeMoreAnimal(25);

        playerController.RelockCursor();
    }

    public void RevealChoiceUI()
    {
        SimplerPlayerController playerController = FindObjectOfType<SimplerPlayerController>();
        playerController.PlayerLock();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }

    public void UnlockCursorforStory()
    {
        SimplerPlayerController playerController = FindObjectOfType<SimplerPlayerController>();
        playerController.PlayerLock();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ButtonRunning()
    {

    }
}
