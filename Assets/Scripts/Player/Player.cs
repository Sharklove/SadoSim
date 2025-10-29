using UnityEngine;

public class Player : MonoBehaviour
{
    public void Teleportation(Vector3 direction)
    {
        transform.position = direction;
    }
    public void Rotate(float angle)
    {
        transform.Rotate(0, angle, 0);
    }
}