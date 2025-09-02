using System;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IHittable
{
    private float speed;
    private int damage;

    public float lifeTime = 10f;   // 子弹存在时间、

    public Vector3 direction;

    public void Init(Vector2 direction, float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
        this.direction = direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        // 自动销毁子弹
        Destroy(gameObject, lifeTime);
    }
    public void OnHit(int scope)
    {
        // 子弹被击中时的处理逻辑（如果需要）
        Destroy(gameObject);
    }
    private void Update()
    {

        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layerMask = LayerMask.GetMask("EnemyBullet", "Enemy");
        if ((layerMask & (1 << other.gameObject.layer)) == 0)
            return; // 没勾选的 Layer，不触发 Destroy

        IHittable hittable = other.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittable.OnHit(damage);
            Destroy(gameObject);
        }
    }
}
