using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float rotationY = 0f;
    public void ChangeAngles(float angle, float minY, float maxY)
    {
        rotationY = Mathf.Clamp(rotationY + -angle, minY, maxY);
        transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    }
}
