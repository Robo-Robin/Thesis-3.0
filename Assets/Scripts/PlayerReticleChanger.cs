using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReticleChanger : MonoBehaviour
{
    public GameObject reticle;
    private Image reticleImage;
    private Sprite baseReticleSprite;

    //0 is base, 1 is dig, 2 is speak generally (for now, change when can do human or animal detection)
    public List<Sprite> reticleImages = new List<Sprite>();

    PlayerController myPlayerController;

    // Start is called before the first frame update
    void Start()
    {
        reticleImage = reticle.GetComponent<Image>();
        baseReticleSprite = reticleImage.sprite;
        myPlayerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myPlayerController.canDig)
        {
            baseReticleSprite = reticleImages[1];
            reticleImage.sprite = baseReticleSprite;
        }
        else if (myPlayerController.canSpeak)
        {
            baseReticleSprite = reticleImages[2];
            reticleImage.sprite = baseReticleSprite;
        }
        else if (!myPlayerController.canDig || !myPlayerController.canSpeak)
        {
            baseReticleSprite = reticleImages[0];
            reticleImage.sprite = baseReticleSprite;
        }
    }
}
