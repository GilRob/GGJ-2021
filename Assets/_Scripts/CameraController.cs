// Camera shake: https://gist.github.com/nbrew/52df13e93e3170f73080
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed;
    public Transform player;

    public float xRotate = 0f;

    private float timer = 0.0f;
    public float walkShakingSpeed;
    public float walkShakingAmount;
    public float runShakingSpeed;
    public float runShakingAmount;
    public float midp = 2.0f;

    public float shakingSpeed = 0.18f;
    public float shakingAmount = 0.2f;
    public float crouchReducedHeight = 0.7f;

    public bool canRotate = true;
    float originalHeight;

    [HideInInspector]
    public float x;
    [HideInInspector]
    public float y;

    void Start()
    {
        xRotate = 0f;
        originalHeight = midp;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (player.gameObject.GetComponent<PlayerController>().isDied)
        {
            //this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
             if(canRotate == true)
             {
                // rotate camera with mouse & controller
                x = Input.GetAxis("Mouse X") * rotateSpeed;
                y = Input.GetAxis("Mouse Y") * rotateSpeed;
             }

            xRotate -= y;
            xRotate = Mathf.Clamp(xRotate, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
            player.Rotate(Vector3.up * x);

            // make camera shake when moving
            if (player.GetComponent<PlayerController>().isRunning)
            {
                shakingSpeed = runShakingSpeed;
                shakingAmount = runShakingAmount;
                midp = originalHeight;
            }
            else if (player.GetComponent<PlayerController>().isCrouching)
            {
                midp = originalHeight - crouchReducedHeight;
            }
            else
            {
                shakingSpeed = walkShakingSpeed;
                shakingAmount = walkShakingAmount;
                midp = originalHeight;
            }

            float waveslice = 0.0f;
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (Mathf.Abs(h) == 0 && Mathf.Abs(v) == 0)
            {
                timer = 0.0f;
            }
            else
            {
                waveslice = Mathf.Sin(timer);
                timer = timer + shakingSpeed;
                if (timer > Mathf.PI * 2)
                {
                    timer = timer - (Mathf.PI * 2);
                }
            }

            Vector3 tranlateLocation = transform.localPosition;

            if (waveslice != 0)
            {
                float translateChange = waveslice * shakingAmount;
                float totalAxes = Mathf.Abs(h) + Mathf.Abs(v);
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                translateChange = totalAxes * translateChange;
                tranlateLocation.y = midp + translateChange;
            }
            else
            {
                tranlateLocation.y = midp;
            }

            transform.localPosition = tranlateLocation;
        }
    }
}
