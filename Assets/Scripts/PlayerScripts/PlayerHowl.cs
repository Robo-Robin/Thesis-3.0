using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHowl : MonoBehaviour
{
    private SimplerPlayerController myPlayerController;

    private bool isHowling;
    private bool canHowl;

    [Range(3f, 7f)] [SerializeField]
    float howlCooldown;
    float howlCooldownTimer = 0;

    AudioSource playerAudioSource;
    public List<AudioClip> howlingClips;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerController = transform.gameObject.GetComponent<SimplerPlayerController>();
        playerAudioSource = transform.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayerController.playerIsLocked == true)
        {
            canHowl = false;
        }
        else if (myPlayerController.playerIsLocked == false)
        {
            canHowl = true;
        }


        if (Input.GetKeyDown(KeyCode.Space) && canHowl == true && isHowling == false)
        {
            HowlStart();
        }


    }

    private void HowlStart()
    {
        isHowling = true;
        float resetLimit = myPlayerController.lookUpwardsLimit;
        myPlayerController.lookUpwardsLimit = -90f;
        myPlayerController.wantedXRotation = -80f;
        howlCooldownTimer = 0f;
        StartCoroutine(HowlCooldown(resetLimit));
    }

    IEnumerator HowlCooldown(float resetLimit)
    {
        yield return new WaitForSecondsRealtime(0.7f);
        myPlayerController.PlayerLock();
        playerAudioSource.PlayOneShot(howlingClips[Random.Range(0, howlingClips.Count)]);
        while (howlCooldownTimer <= howlCooldown)
        {
            howlCooldownTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        myPlayerController.PlayerUnlock();
        myPlayerController.lookUpwardsLimit = resetLimit;
        myPlayerController.wantedXRotation = 10f;
        isHowling = false;

    }

}
