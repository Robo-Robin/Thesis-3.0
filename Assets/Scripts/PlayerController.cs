using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public float movementAcceleration = 375.0f;
    float curSpeedX = 0f;
    float curSpeedY = 0f;

    public Camera playerCamera;
    public float lookSpeed = 1.0f;
    public float lookUpwardsLimit = -45f;
    public float lookDownwardsLimit = 90f;

    //was used in try 1
    public float lookingAcceleration = 150f;

  
    public float lookUpSnapBounds = 15f;
    private float snapTimer = 0f;
    public float snapTimerLimit = 3f;

    public float snapSpeed = 0.3f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    //used in try 2 and 3
    [SerializeField]
    private AnimationCurve lookCurve;
    public float smoothMinAngle = 15f;
    float timetolooktimer = 0f;
    float timeInCurve = 0f;

    public float smoothingTime = 5f;

    float wantedRotationX = 0;
    float currentXRotation;

    public float digMinimumAngle = 55f;
    RaycastHit hit;
    [HideInInspector]
    public bool canDig;
    bool isDigging;

    public GameObject digObject;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
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
            wantedRotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            wantedRotationX = Mathf.Clamp(wantedRotationX, lookUpwardsLimit, lookDownwardsLimit);
            if(Input.GetAxis("Mouse Y") != 0)
            {
                timetolooktimer = smoothingTime / 2;
            }

            //try 1 - smoothdamp requires velocity vectors and not a solid float value - will not work
            /* currentXRotation = Mathf.SmoothDamp(currentXRotation, wantedRotationX, lookingAcceleration * Time.deltaTime, snapSpeed, lookSpeed);*/

            //currently below does the exact opposite of what i want - its fast on the ends and slow in the middle. 
            /*float rotationDifferenceX = Mathf.Abs(wantedRotationX - currentXRotation);
            if (currentXRotation != wantedRotationX)
            {
                smoothingTime = Mathf.Abs(wantedRotationX - currentXRotation) / lookSpeed;
                timetolooktimer += Time.deltaTime;
                timeInCurve = lookCurve.Evaluate(timetolooktimer / smoothingTime);
                currentXRotation = Mathf.Lerp(currentXRotation, wantedRotationX, timeInCurve);
            }
            else
            {
                timetolooktimer = 0f;
                timeInCurve = 0f;
            }*/

            //try 3
            //note for later when youre working on this again - the speed at which you move should scale with distance/deltaAngle
            //and the curve should only really take effect at the ends. not super sure how to do this
            //because the player can just swing their mouse around wildly
            if(wantedRotationX!= currentXRotation)
            {
                timetolooktimer += Time.deltaTime * 30f;
                timeInCurve = lookCurve.Evaluate(timetolooktimer / smoothingTime);
                Debug.Log(timeInCurve);
                currentXRotation = Mathf.Lerp(currentXRotation, wantedRotationX, timeInCurve);

            }

            playerCamera.transform.localRotation = Quaternion.Euler(currentXRotation, 0, 0);
            
            //player y rotation
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            
            //snapping bounds from above to middle
            /*if(currentXRotation < -lookUpSnapBounds)
            {
                UpdateSnapTimer();
            }
            else
            {
                snapTimer = 0f;
            }*/
        }

        if(currentXRotation >= digMinimumAngle)
        {
            canDig = true;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100f))
            {
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 100.0f, Color.red);
            }
        }

        if(canDig == true && Input.GetKeyDown(KeyCode.Space))
        {
            isDigging = true;
            Instantiate(digObject, hit.point, Quaternion.identity);
        }
    }
    void UpdateSnapTimer()
    {
        snapTimer += Time.deltaTime;
        if (snapTimer >= snapTimerLimit)
        {
/*            playerCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);*/
            wantedRotationX = 0;
            snapTimer = 0f;
        }
    }
}
