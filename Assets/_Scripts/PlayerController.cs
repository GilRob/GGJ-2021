// Player movement: https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys
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
    Vector3 startPos;
    Quaternion startRot;
    float groundDis = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public bool isRunning = false;
    public bool isCrouching = false;
    public bool isJumping = false;
    public bool isStop;
    public bool isDied = false;
    [HideInInspector]
    public bool canMove = true;

    public Image fill;
    public Image bar;

    [HideInInspector]
    public RaycastHit hitInfo;
    [HideInInspector]
    public GameObject currentItem;
    /////////UI////////////////////
    public Image cursor;
    public Sprite baseCursor;
    public Sprite InteractCursor;
    public Sprite InvestigateCursor;
    public GameObject youDiedScreen;
    //////////////////////////////////
    public Transform axePosition;
    public Transform bucketPosition;
    public Transform eggPosition;
    public Transform GasPosition;
     public Transform broomPosition;
    public Transform rakePosition;
    public Transform brushPosition;
    [HideInInspector]
    public Transform holdPosition;

    [HideInInspector]
    public bool currentlyHolding = false;
    Rigidbody itemRB;
    private Vector3 mLastPosition;
    public GameObject stamina;
    private bool recovering;
    private bool inWheat;
    private AudioSource wheatSource;
    public AudioClip wheat;
    private int layerMask = ~(1 << 9);

    public DialogueWindow dialogues;

    Vector3 direct;
    float x;
    float y;
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        startPos = this.transform.position;
        startRot = this.transform.rotation;
        currentItem = new GameObject();
        currentItem.name = "Empty";
        itemRB = new Rigidbody();
        holdPosition = new GameObject().transform;
        wheatSource = GetComponent<AudioSource>();
        isDied = false;
        youDiedScreen.SetActive(false);
    }

    void Update()
    {
        if (isDied)
        {
            if(transform.eulerAngles.x <= 85)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + Time.deltaTime*100, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Jump"))
            {
                isDied = false;
                youDiedScreen.SetActive(false);
                
                controller.enabled = false;
                this.transform.position = startPos;
                this.transform.rotation = startRot;
                controller.enabled = true;
            }
        }
        else
        {
            float speed = (transform.position - this.mLastPosition).magnitude / Time.deltaTime;
            this.mLastPosition = transform.position;

            //Debug.Log(speed);

            // check if player on ground
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if(canMove == true)
            {
            // move with keyboard & controller, reduce stamina when running
                x = Input.GetAxis("Horizontal");
                y = Input.GetAxis("Vertical");

                direct = transform.right * x + transform.forward * y;
            }

            if (Input.GetButtonDown("Crouch"))
            {
                if (isCrouching)
                    isCrouching = false;
                else
                    isCrouching = true;
            }
            if (Input.GetButton("Run") && (x != 0 || y != 0) && !isCrouching)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            if (recovering == false)
                stamina.active = false;
            else
                stamina.active = true;

            if (fill.fillAmount <= 0.01f)
            {
                isRunning = false;
            }

            if (fill.fillAmount >= 1)
                recovering = false;
            else if (fill.fillAmount < 1)
                recovering = true;

            if (isRunning)
            {
                fill.fillAmount -= staminaReduceSpeed * Time.deltaTime;
                bar.transform.localPosition = new Vector3((fill.fillAmount - 0.5f) * 2 * 228, 0, 0);
            }
            else
            {
                if (x != 0 || y != 0)
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
            else if (isCrouching)
            {
                controller.Move(direct * crouchingSpeed * Time.deltaTime);
            }
            else
            {
                controller.Move(direct * walkingSpeed * Time.deltaTime);
            }

            if (x != 0 || y != 0)
                isStop = false;
            else
                isStop = true;

            // jump with keybboard & controller
            if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * fallingSpeed);
                isJumping = true;
            }

            velocity.y += fallingSpeed * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            Interact();
        }
    }

    void Interact()
    {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20.0f, layerMask))
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

            if(Input.GetButtonDown("Interact") && hitInfo.collider.tag == "Interactable" && currentlyHolding == false && hitInfo.collider.name == "Axe")
            {
                currentItem = hitInfo.transform.gameObject;
                
                currentlyHolding = true;
                currentItem.GetComponent<Rigidbody>().useGravity = false;
                currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            else if(Input.GetButtonDown("Interact") && hitInfo.collider.tag == "Interactable" && currentlyHolding == false && hitInfo.collider.name == "bucket" && dialogues.activeTask == 3)
            {
                currentItem = hitInfo.transform.gameObject;
                currentlyHolding = true;
                currentItem.GetComponent<Rigidbody>().useGravity = false;
                currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            else if(Input.GetButtonDown("Interact") && hitInfo.collider.tag == "Interactable" && currentlyHolding == false && hitInfo.collider.name == "toiletBrush" && dialogues.activeTask == 1)
            {
                currentItem = hitInfo.transform.gameObject;
                currentlyHolding = true;
                currentItem.GetComponent<Rigidbody>().useGravity = false;
                currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            else if(Input.GetButtonDown("Interact") && hitInfo.collider.tag == "Interactable" && currentlyHolding == false && hitInfo.collider.name == "pitchFork" && dialogues.activeTask == 6)
            {
                currentItem = hitInfo.transform.gameObject;
                currentlyHolding = true;
                currentItem.GetComponent<Rigidbody>().useGravity = false;
                currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            else if(Input.GetButtonDown("Interact") && hitInfo.collider.tag == "Interactable" && currentlyHolding == false && hitInfo.collider.name == "broom" && dialogues.activeTask == 2)
            {
                currentItem = hitInfo.transform.gameObject;
                currentlyHolding = true;
                currentItem.GetComponent<Rigidbody>().useGravity = false;
                currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            else if(Input.GetButtonDown("Interact") && hitInfo.collider.tag == "Interactable" && currentlyHolding == false && hitInfo.collider.name == "Garb" && dialogues.activeTask == 7)
            {
                currentItem = hitInfo.transform.gameObject;
                currentlyHolding = true;
                currentItem.GetComponent<Rigidbody>().useGravity = false;
                currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
            else if(Input.GetButtonDown("Interact") && hitInfo.collider.tag == "Interactable" && currentlyHolding == false && hitInfo.collider.name == "GasCan" && dialogues.activeTask == 7)
            {
                currentItem = hitInfo.transform.gameObject;
                currentlyHolding = true;
                currentItem.GetComponent<Rigidbody>().useGravity = false;
                currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }
        }
        
        if(hitInfo.collider == null)
        {
            cursor.rectTransform.sizeDelta = new Vector2(5, 5);
            cursor.sprite = baseCursor;
        }

        if(currentItem != null && currentlyHolding == true)
        {
            if(currentItem.name == "Axe")
            {
                holdPosition.position = axePosition.position;
                holdPosition.rotation = axePosition.rotation;
            }
            else if(currentItem.name == "bucket")
            {
                holdPosition.position = bucketPosition.position;
                holdPosition.rotation = bucketPosition.rotation;
            }
            else if(currentItem.name == "Egg(Clone)")
            {
                holdPosition.position = eggPosition.position;
                holdPosition.rotation = eggPosition.rotation;
            }
            else if(currentItem.name == "GasCan")
            {
                holdPosition.position = GasPosition.position;
                holdPosition.rotation = GasPosition.rotation;
            }
            else if(currentItem.name == "broom")
            {
                holdPosition.position = broomPosition.position;
                holdPosition.rotation = broomPosition.rotation;
            }
            else if(currentItem.name == "pitchFork")
            {
                holdPosition.position = rakePosition.position;
                holdPosition.rotation = rakePosition.rotation;
            }
             else if(currentItem.name == "toiletBrush")
            {
                holdPosition.position = brushPosition.position;
                holdPosition.rotation = brushPosition.rotation;
            }

            currentItem.transform.position = holdPosition.position;
            currentItem.transform.rotation = holdPosition.rotation;
        }

        if(Input.GetButtonDown("Fire1") && currentlyHolding == true)
        {
            currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            currentItem.GetComponent<Rigidbody>().useGravity = true;
            currentItem.GetComponent<Rigidbody>().AddForce(new Vector3(currentItem.transform.forward.x * 200f, currentItem.transform.forward.y * 100f, currentItem.transform.forward.z * 200f));
            currentlyHolding = false;
            currentItem = new GameObject();
            currentItem.name = "Empty";
        }

        if (inWheat == true)
        {
            wheatSource.clip = wheat;
            wheatSource.loop = true;
            wheatSource.Play();
        }
        else
        {
            wheatSource.Stop();
        }
        //wheat.volume = Mathf.Clamp(speed, 0, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wheat")
        {
            inWheat = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wheat")
        {
            inWheat = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            isDied = true;
            youDiedScreen.SetActive(true);
        }
    }
}
