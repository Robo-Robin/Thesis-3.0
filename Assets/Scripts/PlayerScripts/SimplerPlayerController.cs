using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplerPlayerController : MonoBehaviour
{
    //changing these fields to private because they don't need to be edited
    //player controls (private)
    private float walkingSpeed = 3f;
    private float runningSpeed = 5f;
    private float gravity = 20.0f;

    private float movementAcceleration = 375.0f;
    private float curSpeedX = 0f;
    private float curSpeedY = 0f;

    private bool isRunning;

    private Vector3 moveDirection = Vector3.zero;

    private RaycastHit hit; //unsure if making this private will break things

    //camera controls (private)
    private float lookSpeed = 2f;

    private float snapTimer = 0f;
    private float snapTimerLimit = 2f;
    private float snapSpeed = 0.5f;

    private float smoothingSpeed = 3f;
    private float smoothingTime = 20f;

    private float wantedXRotation;
    private float currentXRotation;

    private float wantedYRotation;
    private float currentYRotation;

    //public fields so we can assign them/access them

    public Camera playerCamera;
    public CharacterController characterController;

    //camera controls (public)
    public float lookUpwardsLimit = -35f;
    public float lookDownwardsLimit = 75f;
    public float lookUpSnapBounds = 15f;

    //player controlls (public)
    public bool playerIsLocked; //formerly was 'canMove' - generally if you cannot move you shouldnt do other things either
                                //also made public so other things can lock the player

    //leaving out digging and audio right now, possibly in favor of
    //making them separate classes, if only for organization and compartmentalization


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isRunning = false;
        playerIsLocked = false;

        //also omitting audio here
    }

    // Update is called once per frame
    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        isRunning = Input.GetKey(KeyCode.LeftShift);
        float wantedSpeedX = !playerIsLocked ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float wantedSpeedY = !playerIsLocked ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        curSpeedX = Mathf.MoveTowards(curSpeedX, wantedSpeedX, movementAcceleration * Time.deltaTime);
        curSpeedY = Mathf.MoveTowards(curSpeedY, wantedSpeedY, movementAcceleration * Time.deltaTime);


        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

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
        if (!playerIsLocked)
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
            if (currentXRotation < -lookUpSnapBounds)
            {
                UpdateSnapTimer();
            }
            else
            {
                snapTimer = 0f;
            }
        }

        //locks all on rclick. works to test
        /*if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(TempLockPlayer());
        }*/
    }

    void UpdateSnapTimer()
    {
        snapTimer += Time.deltaTime;
        if (snapTimer >= snapTimerLimit)
        {
            wantedXRotation = 0;
            snapTimer = 0f;
        }
    }

    public void PlayerLock()
    {
        playerIsLocked = true;
    }

    public IEnumerator TempLockPlayer()
    {
        playerIsLocked = true;
        yield return new WaitForSecondsRealtime(0.6f);//using a set amount of time for testing. update this with other methods. 
        playerIsLocked = false;
    }

    public void PlayerUnlock()
    {
        playerIsLocked = false;
    }

}
