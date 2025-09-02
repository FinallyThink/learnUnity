using UnityEngine;
public class PlayerController : MonoBehaviour, IHittable
{
    [Header("移动与属性")]
    public float moveSpeed = 3f;
    public int PlayerHp = 10;
    public int maxHp = 10;

    private Rigidbody2D rigidbody2D;

    

    [Header("受伤保护")]
    public float injuryProtectionCd = 0.1f;
    private float injuryProtectionTimer = 0f;

    [Header("射击相关")]
    public Transform firePoint;       // 开火位置（空物体挂在角色前方）
    public float bulletSpeed = 8f;    // 子弹速度
    public int bulletDamage = 8;      // 子弹伤害
    public float fireRate = 0.2f;     // 射速（秒）
    private float fireTimer = 0f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void OnHit(int scope)
    {
        PlayerHp -= scope;
        Debug.Log("Player Hit: " + scope + ", Current HP: " + PlayerHp);
        if (PlayerHp <= 0)
        {
            Debug.Log("Game Over");
            // 这里可以加游戏结束逻辑
        }
        injuryProtectionTimer = injuryProtectionCd;
    }

    void Update()
    {
        // ---------------- 移动 ----------------
        float h = Input.GetAxisRaw("Horizontal"); // A,D  
        float v = Input.GetAxisRaw("Vertical");   // W,S 
        Vector3 move = new Vector3(h, v, 0).normalized;
        rigidbody2D.position += new Vector2(move.x, move.y) * moveSpeed * Time.deltaTime;

        // ---------------- 朝向鼠标 ----------------
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 faceToMouse = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(faceToMouse.y, faceToMouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // ---------------- 射击 ----------------
        fireTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && fireTimer <= 0f) // 鼠标左键发射
        {
            Shoot(faceToMouse);
            fireTimer = fireRate;
        }

        // ---------------- 无敌时间倒计时 ----------------
        injuryProtectionTimer = Mathf.Max(0, injuryProtectionTimer - Time.deltaTime);
    }

    private void Shoot(Vector3 direction)
    {
        // 检查 prefab 和发射点是否存在
        if (GameManager.Instance.PlayerBulletPrefab == null || firePoint == null) return;

        // 实例化子弹
        GameObject bulletObj = Instantiate(GameManager.Instance.PlayerBulletPrefab, firePoint.position, firePoint.rotation);

        // 获取 Bullet 脚本
        PlayerBullet bullet = bulletObj.GetComponent<PlayerBullet>();
        if (bullet != null)
        {
            // 初始化子弹
            bullet.Init(

                direction: direction,
                speed: bulletSpeed,
                damage: bulletDamage
            );
        }
    }


    private void OnCollisionStay2D(Collision2D coll)
    {   
        EnemyController enemy = coll.gameObject.GetComponent<EnemyController>();
        if (injuryProtectionTimer <= 0 && enemy != null)
        {
            OnHit(1);
        }
    }
}
