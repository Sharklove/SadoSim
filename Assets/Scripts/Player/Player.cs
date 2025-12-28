using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーの移動、回転、座る/立つ動作を制御するクラス
/// </summary>
public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private float originalHeight = 1.6f;

    /// <summary>
    /// 座っているかどうか
    /// </summary>
    public static bool isSitting = false;

    /// <summary>
    /// インタラクション中かどうか（座る/立つ動作中など）
    /// </summary>
    public static bool IsInteraction = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError($"Player: {gameObject.name}にCharacterControllerコンポーネントが見つかりません。");
            enabled = false;
            return;
        }

        // 初期の高さを保存
        originalHeight = controller.height;
    }

    /// <summary>
    /// プレイヤーを指定位置にテレポート
    /// </summary>
    /// <param name="direction">テレポート先の位置</param>
    public void Teleportation(Vector3 direction)
    {
        if (controller != null)
        {
            controller.enabled = false;
            transform.position = direction;
            controller.enabled = true;
        }
        else
        {
            transform.position = direction;
        }
    }

    /// <summary>
    /// プレイヤーをY軸周りに回転
    /// </summary>
    /// <param name="angle">回転角度</param>
    public void Rotate(float angle)
    {
        transform.Rotate(0, angle, 0);
    }

    /// <summary>
    /// プレイヤーを移動
    /// </summary>
    /// <param name="h">水平方向の入力（-1～1）</param>
    /// <param name="v">垂直方向の入力（-1～1）</param>
    /// <param name="moveSpeed">移動速度</param>
    public void Move(float h, float v, float moveSpeed)
    {
        if (controller == null)
        {
            return;
        }

        // UIが表示されている場合は移動を無効化
        if (UIManager.IsUIActive)
        {
            return;
        }

        // 移動方向を計算
        Vector3 move = transform.right * h + transform.forward * v;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 簡易的な重力処理
        velocity.y -= 9.81f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // 地面に着地したら速度をリセット
        if (controller.isGrounded)
        {
            velocity.y = 0f;
        }
    }

    /// <summary>
    /// 座る動作を開始
    /// </summary>
    /// <param name="sitHeight">座った時の高さ</param>
    /// <param name="sitSpeed">座る速度</param>
    /// <returns>コルーチン</returns>
    public Coroutine Sit(float sitHeight, float sitSpeed)
    {
        if (controller == null)
        {
            return null;
        }

        isSitting = true;
        IsInteraction = true;
        return StartCoroutine(SitCoroutine(sitHeight, sitSpeed));
    }

    /// <summary>
    /// 立つ動作を開始
    /// </summary>
    /// <param name="sitSpeed">立つ速度</param>
    /// <returns>コルーチン</returns>
    public Coroutine Stand(float sitSpeed)
    {
        if (controller == null)
        {
            return null;
        }

        isSitting = false;
        IsInteraction = true;
        return StartCoroutine(SitCoroutine(originalHeight, sitSpeed));
    }

    /// <summary>
    /// 座る/立つ動作のコルーチン
    /// </summary>
    /// <param name="targetHeight">目標の高さ</param>
    /// <param name="speed">変化速度</param>
    private IEnumerator SitCoroutine(float targetHeight, float speed)
    {
        if (controller == null)
        {
            IsInteraction = false;
            yield break;
        }

        while (Mathf.Abs(controller.height - targetHeight) > 0.01f)
        {
            float newHeight = Mathf.MoveTowards(
                controller.height,
                targetHeight,
                Time.deltaTime * speed
            );

            // 立つ場合は位置を調整
            if (!isSitting)
            {
                float heightDifference = newHeight - controller.height;
                transform.position += new Vector3(0, heightDifference, 0);
            }

            controller.height = newHeight;
            controller.center = new Vector3(0, (originalHeight / 2f) + (originalHeight - newHeight) / 2f, 0);
            
            yield return null;
        }

        // 最終的な高さと中心を設定
        controller.height = targetHeight;
        controller.center = new Vector3(0, (originalHeight / 2f) + (originalHeight - targetHeight) / 2f, 0);
        
        IsInteraction = false;
    }
}