using UnityEngine;
using DG.Tweening;

public class DotweenMover : MonoBehaviour
{
    // public Transform targetTransform; // オブジェクトを指定する場合
    // public bool useTransform = false;  // どちらを使うか選択
    public Vector3 originalPosition;  // 元の座標
    public Vector3 targetPosition;    // 目的地の座標
    public float duration = 2f;
    public bool moveParent = false; // 親オブジェクトを動かすかどうか
    public bool moved = false;

    public void MoveToTarget()
    {
        // Vector3 destination = useTransform ? targetTransform.position : targetPosition;
        Transform target = moveParent ? transform.parent : transform;
        if (moved)
        {
            
            target.DOMove(originalPosition, duration)
                  .SetEase(Ease.InOutSine);
            
            moved = false;
        } else {
            target.DOMove(targetPosition, duration)
                  .SetEase(Ease.InOutSine);
            
            moved = true;
        }
    }
}
