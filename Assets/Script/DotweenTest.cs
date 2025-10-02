using UnityEngine;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{
    void Start()
    {
        // 2秒かけて右に移動
        transform.DOMove(new Vector3(3, 0, 0), 2f)
                 .SetEase(Ease.InOutQuad);
    }
}