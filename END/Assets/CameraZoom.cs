using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 5f; // 滾輪縮放速度
    public float minFOV = 15f;   // 最小視野 (Zoom In)
    public float maxFOV = 60f;   // 最大視野 (Zoom Out)

    private Camera cam;

    void Start()
    {
        // 取得附加在同一個 GameObject 上的 Camera 組件
        cam = GetComponent<Camera>();
        
        if (cam == null)
        {
            Debug.LogError("找不到 Camera 組件！請將此腳本掛載在 Main Camera 上。");
        }
    }

    void Update()
    {
        // 使用 Input.GetAxis 取得滾輪數值
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            // 目標 FOV
            float targetFOV = cam.fieldOfView - (scroll * zoomSpeed * 100f * Time.deltaTime);
            // 平滑插值並限制範圍
            cam.fieldOfView = Mathf.Clamp(targetFOV, minFOV, maxFOV);
        }
    }
}