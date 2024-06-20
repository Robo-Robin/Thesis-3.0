using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public float movementAcceleration = 375.0f;
    public float curSpeedX = 0f;
    public float curSpeedY = 0f;

    [HideInInspector]
    public bool isRunning;

    public Camera playerCamera;
    public float lookSpeed = 1.0f;
    public float lookUpwardsLimit = -45f;
    public float lookDownwardsLimit = 90f;

    public float lookUpSnapBounds = 15f;
    private float snapTimer = 0f;
    public float snapTimerLimit = 3f;

    public float snapSpeed = 0.3f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    public float smoothingSpeed = 0f;
    public float smoothingTime = 0f;

    float wantedXRotation;
    [HideInInspector] public float currentXRotation;

    float wantedYRotation;
    float currentYRotation;

    [HideInInspector]
    public bool canMove = true;

    //pretty much anything past here can be cut out if you want just a fps controller
    public float digMinimumAngle = 55f;
    RaycastHit hit;
    [HideInInspector]
    public bool canDig;
    public float digCooldown;
    float digCooldownTimer;
    bool isDigging;

    public GameObject digObject;
    [HideInInspector] public AudioSource playerAudioSource;

    //0 is dig, 1 is howl, 2 is sniff
    public List<AudioClip> playerOneshots = new List<AudioClip>();
    bool canHowl;
    [SerializeField] float howlCooldown;
    float howlCooldownTimer;
    bool isHowling = false;

    public float sniffMinAngle;
    [HideInInspector] public bool isSniffing;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isRunning = false;

        playerAudioSource = GetComponent<AudioSource>();
        canHowl = true;
        isHowling = false;
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }*/

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        isRunning = Input.GetKey(KeyCode.LeftShift);
        float wantedSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float wantedSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        curSpeedX = Mathf.MoveTowards(curSpeedX, wantedSpeedX, movementAcceleration * Time.deltaTime);
        curSpeedY = Mathf.MoveTowards(curSpeedY, wantedSpeedY, movementAcceleration * Time.deltaTime);


        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        
        // Player and Camera rotation
        if (canMove)
        {
            wantedXRotation += -Input.GetAxis("Mouse Y") * lookSpeed;
            wantedXRotation = Mathf.Clamp(wantedXRotation, lookUpwardsLimit, lookDownwardsLimit);

            //smoothing simple
            if (wantedXRotation != currentXRotation)
            {
                smoothingSpeed = Mathf.Abs(wantedXRotation - currentXRotation) / smoothingTime;
                currentXRotation = Mathf.MoveTowards(currentXRotation, wantedXRotation, smoothingSpeed);
            }

            playerCamera.transform.localRotation = Quaternion.Euler(currentXRotation, 0, 0);

            //player y rotation
            wantedYRotation = Input.GetAxis("Mouse X") * lookSpeed;
            if (wantedYRotation != currentYRotation)
            {
                smoothingSpeed = Mathf.Abs(wantedYRotation - currentYRotation) / smoothingTime;
                currentYRotation = Mathf.MoveTowards(currentYRotation, wantedYRotation, smoothingSpeed);
            }

            transform.rotation *= Quaternion.Euler(0, currentYRotation, 0);
            
            //snapping bounds from above to middle
            if(currentXRotation < -lookUpSnapBounds)
            {
                UpdateSnapTimer();
            }
            else
            {
                snapTimer = 0f;
            }
        }

        //also take out if you just want the whole fps controller bit
        if(currentXRotation >= digMinimumAngle && isDigging == false && canMove)
        {
            canDig = true;
            
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100f))
            {
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 100.0f, Color.red);
            }

            if (hit.collider.gameObject.tag == "Object")
            {
                hit.transform.gameObject.GetComponent<Interactable>().isRaycastedOn = true;
            }
        }
        else
        {
            canDig = false;
        }

        if(canDig == true && Input.GetKeyDown(KeyCode.Mouse0))
        {

            isDigging = true;
            
            StartCoroutine(DigCooldown());
            
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canHowl == true && isHowling == false && canMove)
        {
            float resetLimit = lookUpwardsLimit;
            lookUpwardsLimit = -90f;
            wantedXRotation = -75f;
            howlCooldownTimer = 0f;
            StartCoroutine(HowlCooldown(resetLimit));
            isHowling = true;
        }

        if (currentXRotation > sniffMinAngle && isSniffing == false && canMove)
        {
            isSniffing = true;
            playerAudioSource.clip = playerOneshots[2];
            playerAudioSource.loop = true;
            if(playerAudioSource.isPlaying == false)
            {
                playerAudioSource.Play();
            }
        }
        else if(currentXRotation < sniffMinAngle && isSniffing == true && canMove)
        {
            playerAudioSource.Stop();
            isSniffing = false;
            playerAudioSource.loop = false;
            playerAudioSource.clip = playerOneshots[0];

        }
    }
    void UpdateSnapTimer()
    {
        snapTimer += Time.deltaTime;
        if (snapTimer >= snapTimerLimit)
        {
/*            playerCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);*/
            wantedXRotation = 0;
            snapTimer = 0f;
        }
    }

    IEnumerator DigCooldown()
    {
        Vector3 meshDistance;
        playerAudioSource.clip = playerOneshots[0];
        playerAudioSource.volume = 0.8f;
        playerAudioSource.PlayOneShot(playerAudioSource.clip);
        while (digCooldownTimer <= digCooldown)
        {
            digCooldownTimer += Time.deltaTime;
            canDig = false;
            canMove = false;
            yield return new WaitForEndOfFrame();
        }

        if(digCooldownTimer >= digCooldown)
        {
            isDigging = false;
            if (hit.collider.gameObject.tag == "Object")
            {
                meshDistance = new Vector3(0f, 0.5f, 0f);
            }
            else
            {
                meshDistance = new Vector3(0f, 0f, 0f);
                canMove = true;
                canDig = true;
            }
            Instantiate(digObject, hit.point - meshDistance, Quaternion.Euler(-90, 0, 0));
            digCooldownTimer = 0f;
            yield return new WaitForEndOfFrame();
        }
        
    }

    IEnumerator HowlCooldown(float resetLimit)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        canMove = false;
        playerAudioSource.clip = playerOneshots[1];
        playerAudioSource.PlayOneShot(playerAudioSource.clip);

        while (howlCooldownTimer <= howlCooldown)
        {
            howlCooldownTimer += Time.deltaTime;
            canHowl = false;
            canMove = false;
            yield return new WaitForEndOfFrame();
        }

        if (howlCooldownTimer > howlCooldown)
        {
            canMove = true;
            canHowl = true;
            isHowling = false;
            playerAudioSource.clip = playerOneshots[0];
            yield return new WaitForEndOfFrame();
        }
        lookUpwardsLimit = resetLimit;
        wantedXRotation = 0f;

    }

    public void PlayerLock()
    {
        canMove = false;
        canDig = false;
    }

    public void PlayerUnlock()
    {
        canMove = true;
        canDig = true;
    }
}
