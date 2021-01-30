// Player movement: https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys
using UnityEngine;

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
    }
}
