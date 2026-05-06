using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool isGrounded;
    public static bool resate = false;

    public Camera playerCamera;  // دوربین رو در Inspector به Main Camera وصل کن

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // اگر دوربین رو در Inspector نزاشتی، خودکار پیدا کن
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        // گرفتن ورودی
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // گرفتن جهت دوربین (نادیده گرفتن زاویه عمودی برای حرکت افقی صاف)
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        // صفر کردن ارتفاع برای حرکت روی زمین
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // محاسبه حرکت نسبت به دوربین
        Vector3 moveDirection = (cameraForward * vertical + cameraRight * horizontal);
        Vector3 move = moveDirection * moveSpeed;

        // اعمال سرعت (حفظ سرعت عمودی برای فیزیک پرش)
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // پرش
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RESTART();
        }
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
    void RESTART()
    {
        Time.timeScale = 1f;
        resate = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}