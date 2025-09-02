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

}
