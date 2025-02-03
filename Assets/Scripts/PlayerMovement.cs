using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f; // Speed when sprinting
    public float stamina = 100f; // Optional: Add stamina later

    void Update()
    {
        // Get input axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Check if sprinting (Left Shift + moving forward)
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && vertical > 0;
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        // Calculate movement direction relative to player's rotation
        Vector3 moveDirection = (transform.forward * vertical + transform.right * horizontal).normalized;
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
    }
}