using System;
using UnityEngine;

public class EnemyController : MonoBehaviour, IHittable
{
    [Header("基础属性")]
    public float moveSpeed = 1f;
    public int HP = 10;

    [Header("攻击属性")]
    public float fireRate = 2f;
    private float fireTimer = 0f;
    public Transform firePoint;

    private Rigidbody2D rb;
    private Transform player;

    // 初始化方法（外部调用）
    public void Init(float size, float moveSpeed, Color color)
    {
        transform.localScale = Vector3.one * size;
        this.moveSpeed = moveSpeed *2;
        GetComponent<SpriteRenderer>().color = color;
    }

    // IHittable 接口实现
    public void OnHit(int scope)
    {
        Debug.Log("Enemy Hit: " + scope + ", Current HP: " + HP);
        HP -= scope;
        if (HP <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.AddScore(10);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.player.transform;

        // 冻结旋转避免物理干扰
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (player == null) return;

        // ---------------- 朝向玩家 ----------------
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90f;

        // ---------------- 射击 ----------------
        fireTimer -= Time.deltaTime;
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance < 5f && fireTimer <= 0f)
        {
            fireTimer = fireRate;
            Shoot(direction);
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // ---------------- 移动 ----------------
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance >= 5f)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 movement = direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    private void Shoot(Vector2 direction)
    {
        if (GameManager.Instance.enemyBulletPrefab == null || firePoint == null) return;

        GameObject bulletObj = Instantiate(GameManager.Instance.enemyBulletPrefab, firePoint.position, firePoint.rotation);

        EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();
        if (bullet != null)
        {
            bullet.Init(
                direction: direction,
                speed: 3,
                damage: 1
            );
        }
    }
}
