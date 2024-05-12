using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolveBehavior : MonoBehaviour
{
    public SpinIdle spinOddsGetter;
    float humanOdds;
    float beastOdds;

    float totalOdds;

    public StatsSO myStats;


    Image image;
    //0 is human, 1 is beast
    public List<Sprite> resolutionSprites = new List<Sprite>();

    public GameObject TextBox;
    private TMP_Text myText;

    string resolutionText;

    [HideInInspector] public bool spinnerIsResolved;


    private void OnEnable()
    {
        image = GetComponent<Image>();
        humanOdds = spinOddsGetter.humanOdds;
        beastOdds = spinOddsGetter.beastOdds;
        totalOdds = humanOdds + beastOdds;

        myText = TextBox.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resolve()
    {
        float choiceResolution = Random.Range(0f, totalOdds);


        if (choiceResolution <= humanOdds)
        {
            image.sprite = resolutionSprites[0];
            resolutionText = "You have become more of a \n HUMAN";
            //don't hardcode these, but do it later
            myStats.HumanityChanger(20f);
        }
        else
        {
            image.sprite = resolutionSprites[1];
            resolutionText = "You have become more of a \n BEAST";
            myStats.HumanityChanger(-20f);
        }

        myText.SetText(resolutionText);
        spinnerIsResolved = true;
    }
}
