// Player movement: https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform groundCheck;
    public float fallingSpeed = -18f;
    public float jumpHeight = 4f;

    CharacterController controller;
    Vector3 velocity;
    float groundDis = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    RaycastHit hitInfo;

    public Image cursor;
    public Sprite baseCursor;
    public Sprite InteractCursor;
    public Sprite InvestigateCursor;
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        // check if player on ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // move with keyboard & controller
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direct = transform.right * x + transform.forward * y;
        controller.Move(direct * speed * Time.deltaTime);

        // jump with keybboard & controller
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * fallingSpeed);
        }

        velocity.y += fallingSpeed * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

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

            if(Input.GetButtonDown("Interact"))
            {
                Debug.Log("I was found");
            }
        }
    }
}
