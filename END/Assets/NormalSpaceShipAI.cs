using UnityEngine;

public class NormalSpaceShipAI : MonoBehaviour
{
    [Header("移動速度")]
    public float speed = 15f;
    public float rotationSpeed = 2f;

    [Header("隨機巡航")]
    public float forwardDistance = 50f; // 往前方多遠處找目標
    public float randomRadius = 20f;    // 目標點隨機偏移的半徑
    public float changeInterval = 4f;   // 每幾秒換一次目標

    private Vector3 targetPoint;
    private float timer;

    void Start()
    {
        SetNewForwardTarget();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            SetNewForwardTarget();
            timer = 0;
        }

        // 1. 旋轉邏輯
        Vector3 dir = targetPoint - transform.position;
        if (dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            // 加入平滑旋轉
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // 2. 前進邏輯：始終朝前方飛行
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void SetNewForwardTarget()
    {
        // 關鍵修正：目標點 = 飛船前方一段距離 + 隨機偏移
        // 這樣它會一直想「往前飛」，而不是想往「地圖中心」飛
        Vector3 forwardPos = transform.position + transform.forward * forwardDistance;
        Vector3 randomOffset = Random.insideUnitSphere * randomRadius;
        
        targetPoint = forwardPos + randomOffset;

        // 強制防止過度俯衝：確保目標點不會比飛船低太多
        if (targetPoint.y < transform.position.y - 5f) 
        {
            targetPoint.y = transform.position.y + 2f; 
        }
    }

    // 在 Scene 視窗畫出黃線幫助觀察
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, targetPoint);
        Gizmos.DrawWireSphere(targetPoint, 1f);
    }
}