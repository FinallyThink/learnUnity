using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed;
    private int damage;

    public float lifeTime = 10f;   // 子弹存在时间、

    public Vector3 direction;

    public void Init(Vector3 direction, float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
        this.direction = direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        // 自动销毁子弹
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
         transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 避免碰撞到自己所属 Layer 的对象
        Debug.Log("Enemy Bullet Hit: " + other);
        IHittable hittable = other.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittable.OnHit(damage);
        }
        Destroy(gameObject);

    }
}
