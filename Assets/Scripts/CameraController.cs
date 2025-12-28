using UnityEngine;

/// <summary>
/// カメラの上下回転を制御するクラス
/// </summary>
public class CameraController : MonoBehaviour
{
    private float rotationY = 0f;

    /// <summary>
    /// カメラの角度を変更
    /// </summary>
    /// <param name="angle">変更する角度（マウス入力）</param>
    /// <param name="minY">最小角度（下方向の制限）</param>
    /// <param name="maxY">最大角度（上方向の制限）</param>
    public void ChangeAngles(float angle, float minY, float maxY)
    {
        rotationY = Mathf.Clamp(rotationY + -angle, minY, maxY);
        transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    }

    /// <summary>
    /// カメラの回転角度をリセット
    /// </summary>
    public void ResetRotation()
    {
        rotationY = 0f;
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
