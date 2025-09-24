using UnityEngine;

public class Sit : MonoBehaviour // しゃがみ動作を制御するクラス
{
    private float originalHeight = 1f; // 元の高さ
    public float crouchHeight = 0.625f; // しゃがんだときの高さ
    public float crouchSpeed = 1f; // しゃがむ速度
    public static bool isCrouching = false; // しゃがんでいるかどうか

    private float currentHeight = 1f; // 現在の高さ

    void Start()
    {
        currentHeight = originalHeight; // 初期化
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // Cキーが押されたとき
        {
            isCrouching = !isCrouching; // 状態を反転
        }

        float targetHeight = isCrouching ? crouchHeight : originalHeight; // 目標の高さを決定

        float previousHeight = currentHeight; // 前の高さを保存
        
        // currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * crouchSpeed);
        currentHeight = Mathf.MoveTowards(currentHeight, targetHeight, Time.deltaTime * crouchSpeed); // 毎フレーム補間してスムーズに変化

        // オブジェクトの見た目のスケールを更新
        Vector3 newScale = transform.localScale;
        newScale.y = currentHeight;
        transform.localScale = newScale;

        if (!isCrouching)
        {
            // スケールの変化分を位置に反映
            float heightDifference = currentHeight - previousHeight;
            transform.position += new Vector3(0, heightDifference * 2, 0);
        }
    }
}