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
        // ゲーム開始時の初期化処理
        playerSettings.Player.transform.position = playerSettings.startPosition;
    }

    void Update()
    {
        // ゲームのメインループ処理
    }
}