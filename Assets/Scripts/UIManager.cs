using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text txtScore;
    public Button btnStart;

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
}
