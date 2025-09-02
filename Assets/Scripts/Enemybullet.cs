using UnityEngine;

public class Enemybullet : MonoBehaviour
{
    private float speed;
    private int damage;

    public float lifeTime = 10f;   // 子弹存在时间
    private int ownerLayer; // 子弹所属的 Layer

    public void Init(Vector3 direction, float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;

        // 设置子弹自身的 Layer 和方向
        gameObject.layer = ownerLayer;
        transform.up = direction.normalized;

        // 自动销毁子弹
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController hittable = other.gameObject.GetComponent<PlayerController>();

        if (hittable != null)
        {
            hittable.OnHit(damage);
            Destroy(gameObject); // 命中后销毁子弹
        }
    }
}
