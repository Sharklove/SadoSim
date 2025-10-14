using UnityEngine;

public class GameController : MonoBehaviour
{
    // ゲーム全体の管理を行うクラス
    public GameObject Player;
    public Vector3 startPosition;
    void Start()
    {
        // ゲーム開始時の初期化処理
        Player.transform.position = startPosition;
    }

    void Update()
    {
        // ゲームのメインループ処理
    }
}