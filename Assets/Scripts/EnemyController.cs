using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyController : MonoBehaviour,IHittable
{
    public float moveSpeed = 1;
    public int HP = 10;

    public float fireRate = 2f;
    private float fireTimer = 0f;

    public Transform firePoint;
    public void Init(float size, float moveSpeed, Color color)
    {
        transform.localScale = Vector3.one * size;
        this.moveSpeed = moveSpeed;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void OnHit(int scope)
    {
        Debug.Log("Enemy Hit: " + scope + ", Current HP: " + HP);
        this.HP -= scope;
        if (this.HP <= 0)
        {
            Destroy(gameObject);
        }
    }



    void Update()
    {

        Vector3 direction = (GameManager.Instance.player.transform.position - transform.position).normalized;
        float distance = (GameManager.Instance.player.transform.position - transform.position).magnitude;

        // ---------------- 朝向玩家 ----------------
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        // ---------------- 射击 ----------------
        fireTimer -= Time.deltaTime;
        if (distance < 5 && fireTimer <= 0f)
        {
            fireTimer = fireRate;
            Shoot(direction);
        }
        else if (distance >= 5)
        {
            // ---------------- 移动 ----------------
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void Shoot(Vector3 direction)
    {
        // 检查 prefab 和发射点是否存在
        if (GameManager.Instance.enemyBulletPrefab == null || firePoint == null) return;

        // 实例化子弹
        GameObject bulletObj = Instantiate(GameManager.Instance.enemyBulletPrefab, firePoint.position, firePoint.rotation);

        // 获取 Bullet 脚本
        EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();
        if (bullet != null)
        {
            // 初始化子弹
            bullet.Init(
                direction: direction,
                speed: 3,
                damage: 1
            );
        }
    }

}
