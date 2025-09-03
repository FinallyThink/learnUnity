using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 10;
    public float cd = 10f;

    public float timer = 0;

    private List<GameObject> Enemies = new List<GameObject>();
    // Update is called once per frame
    private void Update()
    {
        timer = Mathf.Max(0, timer - Time.deltaTime);
        if (timer <= 0)
        {
            timer = cd;
            GameObject obj = Instantiate(GameManager.Instance.enemyPrefab);
            float angle = Random.value * 360;
            float rad = Mathf.Deg2Rad * angle;
            obj.transform.position = transform.position + radius * new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
            obj.GetComponent<EnemyController>().Init(Random.Range(0.5f, 1.5f), Random.Range(1f, 3f), Random.ColorHSV(0, 1, 0.5f, 1, 1, 1));
            obj.transform.SetParent(transform);
            Enemies.Add(obj);
            GameManager.Instance.uiManager.NewEnemyHpBar(obj);
        }
    }

    public void KillAllEnemies()
    {
        for (int i = Enemies.Count - 1; i >= 0; --i)
        {
            GameObject b = Enemies[i];
            Destroy(b.gameObject);
            Enemies.RemoveAt(i);
        }

    }
}
