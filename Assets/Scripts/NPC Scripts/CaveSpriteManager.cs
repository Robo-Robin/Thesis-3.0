using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CaveSpriteManager : MonoBehaviour
{
    private AudioSource CaveSpriteAudioSource;


    public List<AudioClip> CaveSpriteClips;

    public UnityEvent itsButtonTime;
    

    // Start is called before the first frame update
    void Start()
    {
        CaveSpriteAudioSource = gameObject.GetComponent<AudioSource>();    

        StartCoroutine(PlaySeq1Clips());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySpecificClip(int clipindex)
    {

        CaveSpriteAudioSource.PlayOneShot(CaveSpriteClips[clipindex]);
    }

    public void PlaySeq2()
    {
        StartCoroutine(PlaySeq2Clips());
    }

    public void PlaySeq3(bool eaten)
    {

        StartCoroutine(PlaySeq3Clips(eaten));
       
    }

    IEnumerator PlaySeq1Clips()
    {
        yield return new WaitForSeconds(2f);
        CaveSpriteAudioSource.PlayOneShot(CaveSpriteClips[0]);
        yield return new WaitForSeconds(CaveSpriteClips[0].length + 0.2f);

        CaveSpriteAudioSource.PlayOneShot(CaveSpriteClips[1]);
        yield return new WaitForSeconds(CaveSpriteClips[1].length + 0.3f);

        yield return new WaitForSeconds(2f);
        StartCoroutine(PlayBabysFirstSteps());
        yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0));
        /*StopCoroutine(PlayBabysFirstSteps());*/

        yield return new WaitForSeconds(0.7f);

        CaveSpriteAudioSource.PlayOneShot(CaveSpriteClips[3]);
        yield return new WaitForSeconds(CaveSpriteClips[3].length + 4f);
        
        CaveSpriteAudioSource.PlayOneShot(CaveSpriteClips[4]);
        yield return new WaitForSeconds(CaveSpriteClips[3].length);

    }

    IEnumerator PlayBabysFirstSteps()
    {
        yield return new WaitForSeconds(7f);
        CaveSpriteAudioSource.PlayOneShot(CaveSpriteClips[2]);
        yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0));
        CaveSpriteAudioSource.Stop();
    }

    IEnumerator PlaySeq2Clips()
    {
        CaveSpriteAudioSource.PlayOneShot(CaveSpriteClips[5]);
        yield return new WaitForSeconds(CaveSpriteClips[5].length + 0.3f);
        
        CaveSpriteAudioSource.PlayOneShot(CaveSpriteClips[6]);

    }

    IEnumerator PlaySeq3Clips(bool eaten)
    {
        if (eaten == false)
        {
            yield return new WaitForSeconds(23f);
            CaveSpriteAudioSource.PlayOneShot(CaveSpriteClips[8]);
            yield return new WaitForSeconds(CaveSpriteClips[8].length + 0.3f);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }

        CaveSpriteAudioSource.PlayOneShot(CaveSpriteClips[9]);
        yield return new WaitForSeconds(CaveSpriteClips[9].length + 0.3f);

        itsButtonTime.Invoke();
        //after this we're gonna go ahead and make it do the rest of the stuff, i think right now we have to build
        // 01/07/2025


    }


}
