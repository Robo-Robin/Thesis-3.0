using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleUseTextChanger : MonoBehaviour
{
    public TMP_Text errorText;
    public void ChangeTextChatty()
    {
        errorText.SetText("Wow, feeling chatty are we?");
        StartCoroutine(CloseErrorBox());
    }
    
    public void ChangeTextEllipses()
    {
        errorText.SetText(". . .");
        StartCoroutine(CloseErrorBox());
    }

    IEnumerator CloseErrorBox()
    { 
        yield return new WaitForSeconds(5f);
        FindObjectOfType<DriftEndManager>().UnpauseTimer();
        Destroy(gameObject);
    }
}
