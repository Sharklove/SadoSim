using UnityEngine;

public static class InputManager
{
    public static Vector2 GetMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y);
    }

    public static bool IsSprint()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public static bool IsCrouchPressed()
    {
        return Input.GetKeyDown(KeyCode.C);
    }

    public static bool IsMouseLook()
    {
        return Input.GetMouseButton(1);
    }

    // 必要に応じて他の入力も追加
}