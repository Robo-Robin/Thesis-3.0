using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StatBlockSO", menuName = "ScriptableObjects/StatBlockSO", order = 1)]
public class StatBlockSO : ScriptableObject
{
    [Range(-100, 100)]
    public int pathProgress; //-100 is human, 0 is the start, 100 is coyote

    public string pathPercentageDisplay;//should display path progress - always positive
    public int itemsFound = 0;

    public string pathChosen;

    public int timeInInterrimMinutes;

    public float timeInbetween;
    

    public int InterrimLimitInMinutes;

    public List<string> artefactNamesTaken = new List<string>();
    public List<string> artefactNamesEaten = new List<string>();
    public List<GameObject> artefactsFound = new List<GameObject>();

    public void ProgressPath(int changeby)
    {
        pathProgress += changeby;

        Mathf.Clamp(pathProgress, -100, 100);

        ChangePathMessage();
    }

    public void ChangePathMessage()
    {
        if (pathProgress <= -75)
        {
            pathChosen = "Human";
            pathPercentageDisplay = (-pathProgress / 100).ToString();
        }
        else if (pathProgress <= -25)
        {
            pathChosen = "Mostly Human";
            pathPercentageDisplay = (-pathProgress / 100).ToString();
        }
        else if (pathProgress > -25 && pathProgress < 25)
        {
            pathChosen = "Drifting";
            pathPercentageDisplay = ((int)(timeInInterrimMinutes / InterrimLimitInMinutes)).ToString();

        }
        else if (pathProgress >= 75)
        {
            pathChosen = "Beast";
            pathPercentageDisplay = (pathProgress / 100).ToString();
        }
        else if (pathProgress >= 25)
        {
            pathChosen = "Mostly Beast";
            pathPercentageDisplay = (pathProgress / 100).ToString();
        }



    }

    //eventually this needs to add the scriptable object for an artefact bc 
    //otherwise you cant access the stories again. 
    //02/16/2025 ^ this is still true


    /*public void AddArtefactToTakenList(string ArtefactName)
    {
        artefactNamesTaken.Add(ArtefactName);
    }
    public void AddArtefactToEatenList(string ArtefactName)
    {
        artefactNamesEaten.Add(ArtefactName);
    }*/
}
