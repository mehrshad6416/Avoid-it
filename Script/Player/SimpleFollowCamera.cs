using UnityEngine;

public class SimpleFollowCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 5, -8);
    public float smoothSpeed = 8f;     // افزایش کمی سرعت
    public float rotationSpeed = 50f;

    private float currentAngle = 0f;
    private string saveKey = "CameraAngle";
    private Vector3 velocity = Vector3.zero;  // برای SmoothDamp

    void Start()
    {
        currentAngle = PlayerPrefs.GetFloat(saveKey, 0f);
    }

    void LateUpdate()
    {
        if (player == null) return;

        // چرخش با Q و E
        if (Input.GetKey(KeyCode.Q))
            currentAngle -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E))
            currentAngle += rotationSpeed * Time.deltaTime;

        // اعمال چرخش
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 rotatedOffset = rotation * offset;
        Vector3 targetPosition = player.position + rotatedOffset;

        // استفاده از SmoothDamp برای حرکت نرم‌تر و بدون لرزش
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / smoothSpeed);

        // نگاه به پلیر (با نرمی کمتر برای جلوگیری از لرزش)
        Vector3 lookTarget = player.position + Vector3.up * 1.5f;
        Quaternion targetRotation = Quaternion.LookRotation(lookTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }

    void OnDestroy()
    {
        PlayerPrefs.SetFloat(saveKey, currentAngle);
        PlayerPrefs.Save();
    }
}