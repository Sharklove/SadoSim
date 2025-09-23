using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float sensitivity = 2f;
    public float minY = -90f; // 下方向の最大角度
    public float maxY = 90f;  // 上方向の最大角度

    private float rotationY = 0f;

    void Update()
    {
        if (UIManager.IsUIActive) return;
        // マウスの上下の動きを取得
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 左右の回転は親オブジェクトで処理
        transform.parent.Rotate(Vector3.up * mouseX * sensitivity);

        // 上下の回転を計算
        rotationY += -mouseY * sensitivity;

        // 回転角度を制限して見上げすぎ、見下ろしすぎを防ぐ
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // カメラを回転
        transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    }
}