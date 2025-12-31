using UnityEngine;
using UnityEngine.InputSystem; // 使用新版 Input System

public class bb : MonoBehaviour
{
    public GameObject bulletPrefab; // 把子彈 Prefab 拖進來
    public Transform firePoint;     // 子彈發射的位置（飛船前方的一個空物件）

    void Update()
    {
        // 檢查是否按下空白鍵 (新版 Input System 寫法)
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 生成子彈
        // 參數：物件, 位置, 角度
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}