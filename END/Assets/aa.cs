using UnityEngine;

public class aa : MonoBehaviour
{
    public float speed = 100f;    // 子彈速度
    public float lifeTime = 3f;   // 3秒後自動毀滅

    void Start()
    {
        // 也可以使用 Rigidbody 賦予速度
        // GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 讓子彈持續向前飛行
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // 撞到東西時觸發 (如果目標有 Collider)
    void OnTriggerEnter(Collider other)
    {
        // 這裡可以寫爆炸特效或扣血邏輯
        Destroy(gameObject); 
    }
}