using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorAttractor : MonoBehaviour
{
    public bool attractionActive;

    public SimplerPlayerController myPlayerController;
    Vector3 playerPosition;

    [Range(0f, 0.3f)]
    public float LookAtSpeed; //0.05 is default - a lil slow but pog for beginning, fricking 0.2 is like, fast

    public AudioSource sniffAudioSource;
    public List<AudioClip> sniffClips;

    public GameObject CurrentTarget;

    List<GameObject> AllObjInRadius = new List<GameObject>(); //we're not going to be updating distances - essentially we're doing first in first out

    private SphereCollider mySmellCollider; //maybe fun: notes in journal - higher beast levels = higher radius temp
    float smellRadius;

    public GameObject SniffUIParent;
    public GameObject SniffIndicator;

    bool CanSniff;
    public bool isSniffing;

    public PlayerHowl myPlayerHowl;
    public PlayerDigBehavior myPlayerDig;




    void Start()
    {
        mySmellCollider = GetComponent<SphereCollider>(); //we'll update this radius around as we get more/less coyote
        smellRadius = mySmellCollider.radius;
    }

    // Update is called once per frame
    void Update()
    {

        if (myPlayerController.playerIsLocked)
        {
            CanSniff = false;
        }
        else if(myPlayerHowl.isHowling || myPlayerDig.isDigging)
        {
            CanSniff = false;
        }
        else
        {
            CanSniff = true;
        }


        if (AllObjInRadius.Count > 0)
        {
            attractionActive = true;
        }
        else
        {
            attractionActive = false;
        }


        if (CanSniff)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                int rClip = Random.Range(0, sniffClips.Count);
                sniffAudioSource.clip = sniffClips[rClip];

                isSniffing = true;
                StartCoroutine(ActivateSniffUI());

                
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                StopCoroutine(ActivateSniffUI());
                isSniffing = false;
                DeactivateSniffUI();
            }


            if (attractionActive && Input.GetKey(KeyCode.Mouse0)) //eventually put an OR statement here where the beast is > some percentage
            {
                //added line to fix sniff while button active = freeze
                if (myPlayerController.playerIsLocked)
                {
                    DeactivateSniffUI();
                }

                Vector3 playerForward = myPlayerController.transform.TransformDirection(Vector3.forward);
                Vector3 playerLeft = myPlayerController.transform.TransformDirection(Vector3.left);

                CurrentTarget = AllObjInRadius[0];
                Vector3 lookatDirection = (CurrentTarget.transform.position - myPlayerController.transform.position).normalized;

                float forwardComparitor = Vector3.Dot(playerForward, lookatDirection);

                float leftComparitor = Vector3.Dot(playerLeft, lookatDirection);

                /*myPlayerController.wantedYRotation -= LookAtSpeed * leftComparitor;*/

                myPlayerController.gameObject.transform.Rotate(new Vector3(0, -leftComparitor * LookAtSpeed, 0));

            }
        }
    }

    IEnumerator ActivateSniffUI()
    {
        sniffAudioSource.Play();
        Dictionary<Transform, GameObject> itemPairUI = new Dictionary<Transform, GameObject>();

        if (AllObjInRadius.Count > 0)
        {

            Debug.Log(AllObjInRadius.Count);
            for(int i = 0; i < AllObjInRadius.Count; i++)
            {
                Debug.Log(AllObjInRadius[i].name);

                GameObject newIndicator = Instantiate(SniffIndicator, SniffUIParent.transform);

                if (!itemPairUI.ContainsKey(AllObjInRadius[i].transform))
                {
                    itemPairUI.Add(AllObjInRadius[i].transform, newIndicator);
                }
                else
                {
                    continue;
                }
            }

            while (Input.GetKey(KeyCode.Mouse0) && AllObjInRadius.Count > 0)
            {
                //added line to fix sniff while button active = freeze
                if (myPlayerController.playerIsLocked)
                {
                    break;
                }


                if (AllObjInRadius.Count < 0)
                    break;

                for(int i = 0; i < AllObjInRadius.Count; i++)
                {
                    Vector2 vecToItem = new Vector2(transform.position.x - AllObjInRadius[i].transform.position.x, transform.position.z - AllObjInRadius[i].transform.position.z);
                    Vector2 playerForward = new Vector2(transform.TransformDirection(Vector3.forward).x, transform.TransformDirection(Vector3.forward).z).normalized;

                    float correctangle = Vector2.SignedAngle(playerForward, vecToItem);

                    float distanceStrength = Mathf.Clamp(1f - (vecToItem.magnitude / mySmellCollider.radius) - 0.15f, 0f, 1f);


                    if (itemPairUI.ContainsKey(AllObjInRadius[i].transform) && SniffUIParent.transform.childCount > 0)
                    {
                        itemPairUI[AllObjInRadius[i].transform].GetComponent<Image>().color = new Color(1f, 1f, 1f, distanceStrength); //changing the alpha

                        itemPairUI[AllObjInRadius[i].transform].transform.eulerAngles = new Vector3(-180f, -180f,correctangle);

                    }
                    else
                    {
                        continue;
                    }
                }
                yield return new WaitForEndOfFrame();
            }

        }

    }
    
    public void DeactivateSniffUI()
    {
        sniffAudioSource.Stop();

        foreach (Transform child in SniffUIParent.transform)
        {
            Destroy(child.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "SpObject" || other.tag == "Object") && !AllObjInRadius.Contains(other.gameObject))
        {
            AllObjInRadius.Add(other.gameObject);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SpObject" || other.tag == "Object")
        {
            AllObjInRadius.Remove(other.gameObject);
        }
    }

}
