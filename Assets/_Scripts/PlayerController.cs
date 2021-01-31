﻿// Player movement: https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed;
    public float runningSpeed;
    public float crouchingSpeed;
    public float staminaReduceSpeed;
    public float staminaBackupWalking;
    public float staminaBackupStop;
    public Transform groundCheck;
    public float fallingSpeed = -18f;
    public float jumpHeight = 4f;

    CharacterController controller;
    Vector3 velocity;
    float groundDis = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public bool isRunning = false;
    public bool isCrouching = false;
    public bool isStop;

    public Image fill;
    public Image bar;

    RaycastHit hitInfo;
    GameObject currentItem;
    /////////UI////////////////////
    public Image cursor;
    public Sprite baseCursor;
    public Sprite InteractCursor;
    public Sprite InvestigateCursor;
    //////////////////////////////////
    public Transform holdPosition;
    bool currentlyHolding = false;
    Rigidbody itemRB;
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        itemRB = new Rigidbody();
    }

    void Update()
    {
        // check if player on ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // move with keyboard & controller, reduce stamina when running
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direct = transform.right * x + transform.forward * y;

        if (Input.GetButtonDown("Crouch"))
        {
            if (isCrouching)
                isCrouching = false;
            else
                isCrouching = true;
        }

        if (Input.GetButton("Run") && (x > 0 || y > 0) && !isCrouching)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (fill.fillAmount <= 0.01f)
        {
            isRunning = false;
        }
        if (isRunning)
        {
            fill.fillAmount -= staminaReduceSpeed * Time.deltaTime;
            bar.transform.localPosition = new Vector3((fill.fillAmount - 0.5f) * 2 * 228, 0, 0);
        }
        else
        {
            if (x > 0 || y > 0)
            {
                fill.fillAmount += staminaBackupWalking * Time.deltaTime;
            }
            else
            {
                fill.fillAmount += staminaBackupStop * Time.deltaTime;
            }
            bar.transform.localPosition = new Vector3((fill.fillAmount - 0.5f) * 2 * 228, 0, 0);
        }

        if (isRunning)
        {
            controller.Move(direct * runningSpeed * Time.deltaTime);
        }
        else if(isCrouching)
        {
            controller.Move(direct * crouchingSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(direct * walkingSpeed * Time.deltaTime);
        }

        if (x > 0 || y > 0)
            isStop = false;
        else
            isStop = true;

        // jump with keybboard & controller
        if(Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * fallingSpeed);
        }

        velocity.y += fallingSpeed * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // player interact object
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 100.0f))
        {

            if(hitInfo.collider.tag == "Interactable")
            {
                cursor.rectTransform.sizeDelta = new Vector2(23,23);
                cursor.sprite = InteractCursor;
            }
            else if(hitInfo.collider.tag == "Investigate")
            {
                cursor.rectTransform.sizeDelta = new Vector2(23, 23);
                cursor.sprite = InvestigateCursor;
            }
            else
            {
                cursor.rectTransform.sizeDelta = new Vector2(5, 5);
                cursor.sprite = baseCursor;
            }

            if(Input.GetButtonDown("Interact") && hitInfo.collider.tag == "Interactable" && currentlyHolding == false)
            {
                currentItem = hitInfo.transform.gameObject;
                currentlyHolding = true;
                //Rigidbody itemRB = currentItem.AddComponent<Rigidbody>();
                currentItem.GetComponent<Rigidbody>().useGravity = false;
                currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            
        }

        if(currentItem != null && currentlyHolding == true)
        {
            currentItem.transform.position = holdPosition.position;
            currentItem.transform.rotation = holdPosition.rotation;
        }

        if(Input.GetButtonDown("Fire1") && currentlyHolding == true)
        {
            //currentItem.transform.position = ;
            currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            currentItem.GetComponent<Rigidbody>().useGravity = true;
            currentItem.GetComponent<Rigidbody>().AddForce(new Vector3(currentItem.transform.forward.x * 200f, currentItem.transform.forward.y * 100f, currentItem.transform.forward.z * 200f));
            currentlyHolding = false;
            Debug.Log("Drop");
        }
    }
}
