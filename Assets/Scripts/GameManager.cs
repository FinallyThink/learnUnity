using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoSingleton<GameManager>
{
    // Start is called before the first frame update
    public PlayerController player;
    public GameObject enemyPrefab;
    public GameObject PlayerBulletPrefab;
    public GameObject enemyBulletPrefab;

    public GameObject enemyHPbarPrefab;
    public UIManager uiManager;
    public EnemySpawner enemySpawner;

    private int score = 0;

    private void Start()
    {
        SetScore(0);
       enemySpawner.gameObject.SetActive(false);
    }

    public int AddScore(int add)
    {
        score += add;
        uiManager.ShowScore(score);
        return score;
    }

    public void SetScore(int score)
    {
        this.score = score;
        uiManager.ShowScore(this.score);
    }

    public void OnGameStart()
    {
        enemySpawner.gameObject.SetActive(true);
    }

}
