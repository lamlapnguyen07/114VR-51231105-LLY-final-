using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public float forwardSpeed = 25f;   // 前進速度
    public float turnSpeed = 60f;      // 旋轉速度

    void Update()
    {
        // 1. 處理前進與後退 (按 W/S 或 向上/下 鍵)
        float forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * forwardInput * forwardSpeed * Time.deltaTime);

        // 2. 處理左右旋轉 (按 A/D 或 向左/右 鍵)
        float turnInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
    }
}