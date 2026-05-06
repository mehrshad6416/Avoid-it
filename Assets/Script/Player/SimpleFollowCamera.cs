using UnityEngine;

public class SimpleFollowCamera : MonoBehaviour
{
    public Transform player;           // در Inspector به Player وصل کن
    public Vector3 offset = new Vector3(0, 5, -8);  // فاصله از پلیر (X, Y, Z)
    public float smoothSpeed = 5f;     // نرمی حرکت

    void LateUpdate()
    {
        if (player == null) return;

        // موقعیت هدف = موقعیت پلیر + افست
        Vector3 targetPosition = player.position + offset;

        // حرکت نرم به سمت هدف
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}