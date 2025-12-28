using UnityEngine;
using DG.Tweening;

/// <summary>
/// DOTweenを使用してオブジェクトを移動させるクラス
/// MoveableタイプのInteractableObjectで使用
/// </summary>
public class DotweenMover : MonoBehaviour
{
    [Tooltip("元の座標")]
    public Vector3 originalPosition;
    
    [Tooltip("目的地の座標")]
    public Vector3 targetPosition;
    
    [Tooltip("移動にかかる時間（秒）")]
    public float duration = 2f;
    
    [Tooltip("親オブジェクトを動かすかどうか")]
    public bool moveParent = false;

    /// <summary>
    /// 現在移動済みかどうか
    /// </summary>
    private bool moved = false;

    /// <summary>
    /// オブジェクトを目標位置と元の位置の間で移動させる
    /// </summary>
    public void MoveToTarget()
    {
        Transform target = moveParent ? transform.parent : transform;
        
        if (target == null)
        {
            Debug.LogWarning("DotweenMover: 移動対象のTransformが見つかりません。");
            return;
        }

        // 既存のTweenを停止
        target.DOKill();

        if (moved)
        {
            // 元の位置に戻す
            target.DOMove(originalPosition, duration)
                  .SetEase(Ease.InOutSine);
            moved = false;
        }
        else
        {
            // 目標位置に移動
            target.DOMove(targetPosition, duration)
                  .SetEase(Ease.InOutSine);
            moved = true;
        }
    }

    void OnDestroy()
    {
        // オブジェクトが破棄される際にTweenを停止
        Transform target = moveParent ? transform.parent : transform;
        if (target != null)
        {
            target.DOKill();
        }
    }
}
