// Player movement: https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed;
    public float runningSpeed;
    public float staminaReduceSpeed;
    public float staminaBackupSpeed;
    public Transform groundCheck;
    public float fallingSpeed = -18f;
    public float jumpHeight = 4f;

    CharacterController controller;
    Vector3 velocity;
    float groundDis = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public bool isRunning;

    public Image fill;
    public Image bar;


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

        // move with keyboard & controller, reduce stamina when running
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direct = transform.right * x + transform.forward * y;

        if (Input.GetButton("Run") && (x > 0 || y > 0))
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
            fill.fillAmount += staminaBackupSpeed * Time.deltaTime;
            bar.transform.localPosition = new Vector3((fill.fillAmount - 0.5f) * 2 * 228, 0, 0);
        }

        if (isRunning)
        {
            controller.Move(direct * runningSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(direct * walkingSpeed * Time.deltaTime);
        }

        // jump with keybboard & controller
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * fallingSpeed);
        }

        velocity.y += fallingSpeed * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);




    }
}
