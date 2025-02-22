using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DriftEndFinalStage : MonoBehaviour
{
    private GameObject _myCanvas;

    public GameObject textStorySpecial;
    public GameObject audStorySpecial;

    public TextAsset LetGoTxt;

    private void Start()
    {
        _myCanvas = FindObjectOfType<Canvas>().gameObject;

    }
    public void OpenLetGoStory()
    {
        GameObject letGoObj = Instantiate(textStorySpecial, _myCanvas.transform);
        letGoObj.GetComponentInChildren<TMP_Text>().SetText(LetGoTxt.text);
    }

    public void OpenClawForwardStory()
    {
        Instantiate(audStorySpecial, _myCanvas.transform);
    }
}
