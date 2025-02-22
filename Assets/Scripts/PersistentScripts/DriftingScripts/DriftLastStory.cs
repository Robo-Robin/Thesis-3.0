using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftLastStory : MonoBehaviour
{
    public GameObject closeButton;

    public GameObject errorWindow;

    private GameObject _myCanvas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeCloseButton());
        _myCanvas = FindObjectOfType<Canvas>().gameObject;
    }

    IEnumerator MakeCloseButton()
    {
        yield return new WaitForSeconds(20f);
        closeButton.SetActive(true);
    }

    public void CloseButtonAction()
    {
        Application.Quit();
    }
   
    public void FakeCloseButtonErrors()
    {
        StartCoroutine(FakeErrorsStart());
    }

    IEnumerator FakeErrorsStart()
    {
        GameObject Error = Instantiate(errorWindow, _myCanvas.transform);
        yield return new WaitForSeconds(2f);
        Error = Instantiate(errorWindow, _myCanvas.transform);
        Error.transform.position = new Vector2(Random.Range(0f, 600f), Random.Range(0f, 270f));

        for (int i = 0; i < 40; i++)
        {
            yield return new WaitForSeconds(2f/(i+1));
            Error = Instantiate(errorWindow, _myCanvas.transform);
            Error.transform.position = new Vector2(Random.Range(0f, 600f), Random.Range(0f, 270f));
        }

        CloseButtonAction();
    }
}
