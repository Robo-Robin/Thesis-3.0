using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtefactSO", menuName = "Artefacts/ArtefactSO", order = 1)]
public class ArtefactSO : ScriptableObject
{
    public int artefactID;
    public string artefactName;

    [TextArea]
    public string artefactDescription;

    public int humanSplit;
    public int animalSplit;

    public string takeButtonText;
    public string eatButtonText;

    [System.Serializable]
    public enum StoryType
    { 
        Audio, 
        Text, 
        HTML, //twine stories
        Image
        /*possible types
         Webpage, //link to books etc. 
        ZIPFile, //zip files that require passwords
        */
    }
    public StoryType a_Type;

    public string humanButtonText;
    public string animalButtonText;

    public AudioClip artefactAudioStory;

    public TextAsset artefactTextStory; //reference to a text file supposedly

    public string artefactHTML_path; //this should be a file path

    public Sprite artefactImageStory;
}
