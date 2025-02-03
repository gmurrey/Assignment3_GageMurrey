using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    // Head Bobbing
    public float walkBobbingSpeed = 0.18f; // Speed while walking
    public float walkBobbingAmount = 0.2f; // Intensity while walking
    public float sprintBobbingSpeed = 0.25f; // Faster bobbing when sprinting
    public float sprintBobbingAmount = 0.3f; // More intense when sprinting

    private float defaultYPos; // Default camera height
    private float timer = 0f;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        defaultYPos = transform.localPosition.y; // Save initial Y position
    }

    void Update()
    {
        // --- Mouse Look ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        // --- Head Bobbing ---
        bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W);

        // Adjust bobbing speed/amount based on sprinting
        float currentBobbingSpeed = isSprinting ? sprintBobbingSpeed : walkBobbingSpeed;
        float currentBobbingAmount = isSprinting ? sprintBobbingAmount : walkBobbingAmount;

        if (isMoving)
        {
            // Oscillate the camera's Y position using a sine wave
            timer += Time.deltaTime * currentBobbingSpeed;
            float newYPos = defaultYPos + Mathf.Sin(timer) * currentBobbingAmount;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                newYPos,
                transform.localPosition.z
            );
        }
        else
        {
            // Smoothly reset to default position when not moving
            timer = 0f;
            float newYPos = Mathf.Lerp(transform.localPosition.y, defaultYPos, Time.deltaTime * walkBobbingSpeed);
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                newYPos,
                transform.localPosition.z
            );
        }
    }
}
