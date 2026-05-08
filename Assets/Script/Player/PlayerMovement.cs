using UnityEngine;
using UnityEngine.SceneManagement;

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

        // ✅ مهم: فعال کردن اینترپلیشن برای نرمی حرکت فیزیک
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        // فقط پرش رو توی Update نگه دار (برای دریافت دقیق Input)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()  // ✅ حرکت رو از Update به FixedUpdate منتقل کن
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

        // تنظیم سرعت در FixedUpdate برای هماهنگی با فیزیک
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}