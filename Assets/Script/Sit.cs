// using UnityEngine;

// [RequireComponent(typeof(CharacterController))]
// public class Sit : MonoBehaviour
// {
//     private CharacterController controller;
//     private float originalHeight = 1f;
//     public float crouchHeight = 0.55f;
//     public static bool isCrouching = false;

//     void Start()
//     {
//         controller = GetComponent<CharacterController>();
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.C))
//         {
//             isCrouching = !isCrouching; // 状態を反転させる
//         }
        
//         // オブジェクトのスケールを更新
//         Vector3 newScale = transform.localScale;
//         newScale.y = isCrouching ? crouchHeight : originalHeight;
//         transform.localScale = newScale;
//     }
// }
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Sit : MonoBehaviour
{
    private CharacterController controller;
    private float originalHeight = 1f;
    public float crouchHeight = 0.55f;
    public float crouchSpeed = 5f; // しゃがむ速度
    public static bool isCrouching = false;

    private float currentHeight = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHeight = originalHeight;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching; // 状態を反転
        }

        // 目標の高さを決定
        float targetHeight = isCrouching ? crouchHeight : originalHeight;
        // 毎フレーム補間してスムーズに変化
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * crouchSpeed);

        // オブジェクトの見た目のスケールを更新
        Vector3 newScale = transform.localScale;
        newScale.y = currentHeight;
        transform.localScale = newScale;
    }
}