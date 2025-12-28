using UnityEngine;
using System;

/// <summary>
/// プレイヤーの設定を保持するクラス
/// </summary>
[Serializable]
public class PlayerSettings
{
    [Tooltip("プレイヤーGameObject")]
    public GameObject Player;

    [Tooltip("開始位置")]
    public Vector3 startPosition;
    
    [Tooltip("移動速度")]
    public float moveSpeed = 2f;
    
    [Tooltip("座った時の高さ")]
    public float sitHeight = 1f;
    
    [Tooltip("座る速度")]
    public float sitSpeed = 1f;
    
    [Header("Camera設定")]
    [Tooltip("マウス感度左右")]
    public float rotationSpeed = 200f;
    
    [Tooltip("マウス感度上下")]
    public float sensitivity = 200f;
    
    [Tooltip("カメラ角度上限")]
    public float maxY = 90f;
    
    [Tooltip("カメラ角度下限")]
    public float minY = -90f;
}

/// <summary>
/// ゲーム全体の制御を行うメインコントローラー
/// </summary>
public class GameController : MonoBehaviour
{
    [Tooltip("プレイヤー設定")]
    public PlayerSettings playerSettings;

    private Player player;
    private CameraController cameraController;

    void Awake()
    {
        // プレイヤー設定が正しく設定されているか確認
        if (playerSettings == null || playerSettings.Player == null)
        {
            Debug.LogError("GameController: PlayerSettingsまたはPlayerが設定されていません。");
            enabled = false;
            return;
        }

        // Playerコンポーネントを取得
        player = playerSettings.Player.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError($"GameController: {playerSettings.Player.name}にPlayerコンポーネントが見つかりません。");
            enabled = false;
            return;
        }

        // CameraControllerコンポーネントを取得
        cameraController = playerSettings.Player.GetComponentInChildren<CameraController>();
        if (cameraController == null)
        {
            Debug.LogWarning($"GameController: {playerSettings.Player.name}の子オブジェクトにCameraControllerが見つかりません。");
        }
    }

    void Start()
    {
        if (player != null)
        {
            player.Teleportation(playerSettings.startPosition);
        }
    }

    void Update()
    {
        // 必要なコンポーネントがない場合は処理をスキップ
        if (player == null)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // プレイヤーの回転
        player.Rotate(mouseX * playerSettings.rotationSpeed * Time.deltaTime);

        // カメラの角度変更
        if (cameraController != null)
        {
            cameraController.ChangeAngles(
                mouseY * playerSettings.sensitivity * Time.deltaTime,
                playerSettings.minY,
                playerSettings.maxY
            );
        }

        // プレイヤーの移動
        player.Move(h, v, playerSettings.moveSpeed);

        // インタラクション中は座る/立つ操作を無効化
        if (Player.IsInteraction)
        {
            return;
        }

        // 座る/立つ操作（Cキー）
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Player.isSitting)
            {
                player.Stand(playerSettings.sitSpeed);
            }
            else
            {
                player.Sit(playerSettings.sitHeight, playerSettings.sitSpeed);
            }
        }
    }
}