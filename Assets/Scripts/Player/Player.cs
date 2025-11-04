using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private bool isSitting = false;
    private float originalHeight = 1.6f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Teleportation(Vector3 direction)
    {
        transform.position = direction;
    }

    public void Rotate(float angle)
    {
        transform.Rotate(0, angle, 0);
    }

    public void Move(float h, float v, float moveSpeed)
    {
        if (UIManager.IsUIActive) return;

        Vector3 move = transform.right * h + transform.forward * v;

        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y -= 1f * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    // public void Sit(float sitHeight, float sitSpeed)
    // {
    //     isSitting = true;
    //     float t = sitSpeed * Time.deltaTime;
    //     controller.height = Mathf.MoveTowards(originalHeight, sitHeight, t);
    //     controller.center = new Vector3(0, controller.height / 2, 0);
    // }
    public void Sit()
    {
        isSitting = true;
    }
    void Update()
    {
        if (isSitting && controller.height > 1.0f)
        {
            controller.height = Mathf.Lerp(controller.height, originalHeight, Time.deltaTime * 1f);
            controller.center = new Vector3(0, controller.height / 2, 0);
        }
    }
}