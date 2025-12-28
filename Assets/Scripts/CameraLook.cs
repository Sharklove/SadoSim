using UnityEngine;

/// <summary>
/// カメラの回転を制御するクラス（代替実装）
/// 注意: 現在はCameraControllerが使用されています。このクラスは未使用の可能性があります。
/// </summary>
[System.Obsolete("CameraControllerが使用されています。このクラスは削除を検討してください。")]
public class CameraLook : MonoBehaviour
{
    [Tooltip("マウス感度")]
    public float sensitivity = 2f;
    
    [Tooltip("下方向の最大角度")]
    public float minY = -90f;
    
    [Tooltip("上方向の最大角度")]
    public float maxY = 90f;

    private float rotationY = 0f;

    void Update()
    {
        if (UIManager.IsUIActive)
        {
            return;
        }

        // マウスの入力を取得
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 左右の回転は親オブジェクトで処理
        if (transform.parent != null)
        {
            transform.parent.Rotate(Vector3.up * mouseX * sensitivity);
        }

        // 上下の回転を計算
        rotationY += -mouseY * sensitivity;

        // 回転角度を制限
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // カメラを回転
        transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    }
}