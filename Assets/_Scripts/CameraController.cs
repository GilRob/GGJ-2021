// Camera shake: https://gist.github.com/nbrew/52df13e93e3170f73080
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed;
    public Transform player;

    float xRotate = 0f;

    private float timer = 0.0f;
    public float shakingSpeed = 0.18f;
    public float shakingAmount = 0.2f;
    public float midp = 2.0f;

    void Start()
    {
        xRotate = 0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // rotate camera with mouse & controller
        float x = Input.GetAxis("Mouse X") * rotateSpeed;
        float y = Input.GetAxis("Mouse Y") * rotateSpeed;

        xRotate -= y;
        xRotate = Mathf.Clamp(xRotate, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
        player.Rotate(Vector3.up * x);

        // make camera shake when moving
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
