using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    private List<GameObject> CoinList = new List<GameObject>();
    public void CreatNewCoin(Vector3 position)
    {
        GameObject Coin = Instantiate(GameManager.Instance.CoinPrefat);
        Coin.transform.position = position;
        Coin.transform.SetParent(transform);
        CoinList.Add(Coin);
    }
    public void ClearnCoinList()
    {
        for (int i = CoinList.Count - 1; i >= 0; --i)
        {
            GameObject b = CoinList[i];
            Destroy(b.gameObject);
            CoinList.RemoveAt(i);
        }
    }
}
