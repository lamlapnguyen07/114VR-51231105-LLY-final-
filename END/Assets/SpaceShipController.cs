using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShipController : MonoBehaviour
{
    [Header("移動速度")]
    public float forwardSpeed = 50f;
    public float strafeSpeed = 30f; // 左右平移
    public float hoverSpeed = 30f;  // 上下平移

    [Header("滑鼠轉向")]
    public float lookRateSpeed = 90f;
    private float yaw;   // 左右偏航
    private float pitch; // 上下俯仰

    [Header("戰鬥系統")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start()
    {
        // 1. 隱藏滑鼠並鎖定
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 按下 Esc 可以釋放/重新鎖定滑鼠
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ToggleCursor();
        }

        HandleRotation();
        HandleMovement();

        // 空白鍵發射子彈
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void HandleRotation()
    {
        // 如果滑鼠目前沒被鎖定，就不旋轉鏡頭 (方便操作 UI)
        if (Cursor.lockState != CursorLockMode.Locked) return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * lookRateSpeed * Time.deltaTime;
        pitch -= mouseDelta.y * lookRateSpeed * Time.deltaTime;

        // 限制仰角，防止 360 度翻轉導致混亂
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    void HandleMovement()
    {
        var keyboard = Keyboard.current;
        
        // 前後 (W/S)
        float forwardInput = (keyboard.wKey.isPressed ? 1 : 0) - (keyboard.sKey.isPressed ? 1 : 0);
        // 左右平移 (A/D)
        float strafeInput = (keyboard.dKey.isPressed ? 1 : 0) - (keyboard.aKey.isPressed ? 1 : 0);
        // 上下升降 (Q/E)
        float hoverInput = (keyboard.eKey.isPressed ? 1 : 0) - (keyboard.qKey.isPressed ? 1 : 0);

        Vector3 moveDirection = (transform.forward * forwardInput * forwardSpeed) + 
                                (transform.right * strafeInput * strafeSpeed) +
                                (transform.up * hoverInput * hoverSpeed);

        transform.position += moveDirection * Time.deltaTime;
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void ToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}