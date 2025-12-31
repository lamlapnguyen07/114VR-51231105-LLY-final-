using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    [Header("旋轉速度設定")]
    public Vector3 rotationSpeed = new Vector3(0, 50, 0); // 預設繞著 Y 軸轉

    [Header("座標系選擇")]
    public Space space = Space.Self; // Space.Self 是自身座標，Space.World 是世界座標

    void Update()
    {
        // 每一幀根據旋轉速度進行旋轉
        // 使用 Time.deltaTime 確保在不同幀率下旋轉速度一致
        transform.Rotate(rotationSpeed * Time.deltaTime, space);
    }
}