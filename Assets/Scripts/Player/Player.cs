using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private float originalHeight = 1.6f;

    public static bool isSitting = false;
    public static bool IsInteraction = false;

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

    public Coroutine Sit(float sitHeight, float sitSpeed)
    {
        isSitting = true;
        IsInteraction = true;
        return StartCoroutine(SitCoroutine(sitHeight, sitSpeed));
    }

    public Coroutine Stand(float sitSpeed)
    {
        isSitting = false;
        IsInteraction = true;
        return StartCoroutine(SitCoroutine(originalHeight, sitSpeed));
    }

    private IEnumerator SitCoroutine(float targetHeight, float speed)
    {
        while (controller.height != targetHeight)
        {
            float newHeight = Mathf.MoveTowards(
                controller.height,
                targetHeight,
                Time.deltaTime * speed
            );
            if (!isSitting)
            {
                float heightDifference = newHeight - controller.height;
                transform.position += new Vector3(0, heightDifference * 2, 0);
            }
            controller.height = newHeight;
            controller.center = new Vector3(0, (originalHeight / 2f) + (originalHeight - newHeight) / 2f, 0);
            yield return null;
        }
        IsInteraction = false;
    }
}