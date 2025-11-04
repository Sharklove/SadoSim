using UnityEngine;
using System;

[Serializable]
public class PlayerSettings
{
    public GameObject Player;

    [Tooltip("開始位置")]
    public Vector3 startPosition;
    [Tooltip("移動速度")]
    public float moveSpeed = 2f;
    public float sitHeight = 1f;
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
public class GameController : MonoBehaviour
{
    public PlayerSettings playerSettings;

    private Player player;
    private CameraController cameraController;

    void Awake()
    {
        player = playerSettings.Player.GetComponent<Player>();
        cameraController = playerSettings.Player.GetComponentInChildren<CameraController>();
    }

    void Start()
    {
        player.Teleportation(playerSettings.startPosition);
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        player.Rotate(mouseX * playerSettings.rotationSpeed * Time.deltaTime);
        cameraController.ChangeAngles(mouseY * playerSettings.sensitivity * Time.deltaTime, playerSettings.minY, playerSettings.maxY);
        player.Move(h, v, playerSettings.moveSpeed);

        if (Input.GetKeyDown(KeyCode.C))
        {
            player.Sit();
        }
    }
}