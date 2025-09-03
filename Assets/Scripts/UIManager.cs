using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text txtScore;
    public Button btnStart;

    public UIHpBar playerHpbar;
    public GameObject enemyHPbarPrefab;
    public UIHpBar enemyHpBarPrefab;

    public RectTransform enemyHpBarsTransFrom;
    private List<UIHpBar> enemyHpBars = new List<UIHpBar>();
    private void Start()
    {
        btnStart.gameObject.SetActive(true);

    }

    public void ShowScore(int socre)
    {
        txtScore.text = " 得分：" + socre;
    }

    public void OnClickStart()
    {
        Debug.Log("Start Game ");
        GameManager.Instance.OnGameStart();
        btnStart.gameObject.SetActive(false);
    }

    public void UpdateHpBar()
    {
        PlayerController player = GameManager.Instance.player;
        playerHpbar.SetVal(player.PlayerHp, player.maxHp);

        for (int i = enemyHpBars.Count - 1; i >= 0; --i)
        {
            UIHpBar b = enemyHpBars[i];
            if (b.follow != null)
            {
                EnemyController enemy = b.follow.GetComponent<EnemyController>();
                b.SetVal(enemy.HP, enemy.maxHp);
                b.DoFollow();
            }
            else
            {
                Destroy(b.gameObject);
                enemyHpBars.RemoveAt(i);
            }
        }
    }


    public void NewEnemyHpBar(GameObject follow)
    {
        GameObject obj = Instantiate(enemyHPbarPrefab);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.SetParent(enemyHpBarsTransFrom);
        rect.localScale = Vector3.one;
        UIHpBar hpBar = obj.GetComponent<UIHpBar>();
        hpBar.SetFollow(follow);
        enemyHpBars.Add(hpBar);

    }
    private void Update()
    {
        UpdateHpBar();
    }


}
