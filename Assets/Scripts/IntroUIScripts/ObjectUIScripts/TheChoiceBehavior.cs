using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheChoiceBehavior : MonoBehaviour
{

    public SimplerPlayerController myPlayerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevealChoiceUI()
    {

        myPlayerController.PlayerLock();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }

    public void ChoiceMade()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        myPlayerController.PlayerUnlock();
    }
}
