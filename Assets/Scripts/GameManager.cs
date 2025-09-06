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

    public GameObject CoinPrefat;
    public CoinSpawner coinSpawner;
    public BulletSpawner BulletSpawner;
    public UIManager uiManager;
    public EnemySpawner enemySpawner;
    private int score = 0;

    private int coin = 0;


    private void Start()
    {
        SetScore(0);
        enemySpawner.gameObject.SetActive(false);
    }

    public int AddScore(int add)
    {
        score += add;
        uiManager.ShowScore(score);
        uiManager.PlayScoreActiveAnimation();
        return score;
    }

    public void setCoin(int coin)
    {
        this.coin = coin;
        uiManager.ShowCoin(coin);
    }

    public int AddCoin(int add)
    {
        coin += add;
        uiManager.ShowCoin(coin);
        return coin;
    }


    public void SetScore(int score)
    {
        this.score = score;
        uiManager.ShowScore(this.score);
    }

    public void OnGameStart()
    {
        player.PlayerHp = player.maxHp;
        SetScore(0);
        setCoin(0);
        enemySpawner.gameObject.SetActive(true);
        uiManager.HiddenGameOverBtu();
    }

    public void GameOver()
    {
        uiManager.txtGameOver.gameObject.SetActive(true);
        enemySpawner.gameObject.SetActive(false);
        uiManager.btnReStartGame.gameObject.SetActive(true);
        uiManager.ClearEnemyHpBars();
        enemySpawner.KillAllEnemies();
        coinSpawner.ClearnCoinList();
    }
}
