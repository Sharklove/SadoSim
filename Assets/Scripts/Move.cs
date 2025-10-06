using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour
{
    public float moveSpeed = 2f;    // 移動速度
    public float gravity = -9.81f;  // 重力加速度

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (UIManager.IsUIActive) return;
        if (Sit.isSiting) return; // しゃがんでいるときは移動を無効化
        // WASD 入力を取得（x=横移動, z=前後移動）
        float h = Input.GetAxisRaw("Horizontal"); // A,Dキー or ←,→
        float v = Input.GetAxisRaw("Vertical");   // W,Sキー or ↑,↓

        Vector3 move = transform.right * h + transform.forward * v;

        // 移動処理（x,z方向）
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 地面に接しているときに落下速度をリセット
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 少し押し付けることで地面に張り付く
        }

        // 重力を加算（y方向）
        velocity.y += gravity * Time.deltaTime;

        // 移動処理（y方向）
        controller.Move(velocity * Time.deltaTime);
    }
}
