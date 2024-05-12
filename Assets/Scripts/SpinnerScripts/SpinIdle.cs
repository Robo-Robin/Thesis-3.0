using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinIdle : MonoBehaviour
{
    public float rotation;
    float rotationMultiplier = 1f;

    public bool playerSpin;
    public bool waitingForClick;

    Image spinnerSprite;
    public List<Sprite> spinnerSpriteOptions = new List<Sprite>();

    //we might have to sub these in via scriptable object later
    //for now these just add up to 10;
    public float humanOdds = 5f;
    public float beastOdds = 5f;


    private void OnEnable()
    {
        spinnerSprite = GetComponent<Image>();
        spinnerSprite.sprite = spinnerSpriteOptions[0];
        playerSpin = false;
        waitingForClick = false;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rotation = 0f;
        rotationMultiplier = 1f;

    }
    // Update is called once per frame
    void Update()
    {
        if (!playerSpin && !waitingForClick)
        {
            IdleSpin();
        }
        else if(playerSpin && waitingForClick)
        {
            ActiveSpin();
        }
    }

    void IdleSpin()
    {
        rotation -= 0.1f;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        if (rotation <= -360f)
        {
            rotation = 0f;
        }
    }

    public void SpinButtonPressed()
    {
        if (!waitingForClick && !playerSpin)
        {
            StartCoroutine(StartPlayerSpin());
        }
        playerSpin = true;
    }

    void ActiveSpin()
    {

        rotation -= 10f;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        if (rotation <= -360f)
        {
            rotation = 0f;
        }
    }

    IEnumerator StartPlayerSpin()
    {
        rotation = -15f;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        while (rotation <= 0f)
        {
            rotation += 0.1f;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            yield return new WaitForEndOfFrame();
        }

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rotation = 0f;

        while (rotation <= 10f)
        {
            rotation += 0.1f;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            yield return new WaitForEndOfFrame();
        }

        while (rotationMultiplier < 30f)
        {
            rotationMultiplier += 0.1f;
            rotation -= 0.6f * rotationMultiplier;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            yield return new WaitForEndOfFrame();
        }

        spinnerSprite.sprite = spinnerSpriteOptions[1];
        waitingForClick = true;
    }
}
