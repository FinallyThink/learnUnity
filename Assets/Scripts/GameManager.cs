using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    // Start is called before the first frame update
    public PlayerController player;
    void Start()
    {
        Debug.Log("GameManager.start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("GameManager.update");

    }
}
