using UnityEngine;
using System;

[Serializable]
public class PlayerSettings
{
    public GameObject Player;
    public Vector3 startPosition;
}
public class GameController : MonoBehaviour
{
    [Tooltip("プレイヤーの設定")]
    public PlayerSettings playerSettings;
    
    void Start()
    {
        playerSettings.Player.GetComponent<Player>().Move(playerSettings.startPosition);
    }

    void Update()
    {
        // ゲームのメインループ処理
    }
}