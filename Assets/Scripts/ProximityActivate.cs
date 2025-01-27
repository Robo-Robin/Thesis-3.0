using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProximityActivate : MonoBehaviour
{
    public UnityEvent PlayCaveAudioPartTwo;

    private bool ProxTrig1 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player" && ProxTrig1 == false)
       {
            ProxTrig1 = true;
            PlayCaveAudioPartTwo.Invoke();
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                
                StartCoroutine(LightFadeIn());
            }
       }
    }

    IEnumerator LightFadeIn()
    {
        Light[] childlights;

        childlights = GetComponentsInChildren<Light>();

        float lightsIntensity = 0f;
        
        while (lightsIntensity < 0.6)
        {
            lightsIntensity += Time.deltaTime;

            foreach (Light l in childlights)
            {
                
                l.intensity = lightsIntensity;
                
            }

            yield return new WaitForSeconds(0.02f);
        }


        
    }
}
