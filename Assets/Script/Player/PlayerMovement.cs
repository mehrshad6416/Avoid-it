using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded;
    public Camera playerCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (playerCamera == null)
            playerCamera = Camera.main;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGroundedCheck())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * vertical + cameraRight * horizontal);
        Vector3 move = moveDirection * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    bool IsGroundedCheck()
    {
        // روش ساده: یه کپسول زیر پلیر چک کن
        float extraHeight = 0.1f;
        RaycastHit hit;

        // یه پرتو از پایین پلیر به پایین
        Vector3 rayStart = transform.position + Vector3.down * 0.5f;

        if (Physics.Raycast(rayStart, Vector3.down, out hit, 0.2f))
        {
            Debug.Log("برخورد با: " + hit.collider.gameObject.name);
            return true;
        }

        return false;
    }
}