using UnityEngine;
using DG.Tweening;

public class DotweenMover : MonoBehaviour
{
    public Transform targetTransform; // オブジェクトを指定する場合
    public Vector3 targetPosition;    // 座標で指定する場合
    public bool useTransform = false;  // どちらを使うか選択
    public float duration = 2f;

    public void MoveToTarget()
    {
        Vector3 destination = useTransform ? targetTransform.position : targetPosition;

        transform.DOMove(destination, duration)
                 .SetEase(Ease.InOutSine);
    }
}
