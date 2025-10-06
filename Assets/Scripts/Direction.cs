using UnityEngine;

public class Direction : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        if (UIManager.IsUIActive) return;
        // マウスのX軸の動きを取得
        float mouseX = Input.GetAxis("Mouse X");

        // オブジェクトのY軸を中心に回転
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);
    }
}