using UnityEngine;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{
    public Transform targetPosition; // 移動先
    public float duration = 2f;      // 移動時間

    public void MoveToTarget()
    {
        if (targetPosition != null)
        {
            transform.DOMove(targetPosition.position, duration)
                     .SetEase(Ease.InOutSine);
        }
        else
        {
            Debug.LogWarning("ターゲット位置が設定されていません！");
        }
    }
}