using UnityEngine;

/// <summary>
/// オブジェクトを選択して拾う/置く機能を提供するクラス
/// </summary>
public class ObjectPicker : MonoBehaviour
{
    [Tooltip("オブジェクト選択用のObjectSelectorコンポーネント")]
    public ObjectSelector selector;
    
    [Tooltip("オブジェクトを保持する親Transform（例: PlayerHand）")]
    public Transform newParent;

    [Tooltip("保持するオブジェクトの子インデックス（デフォルト: 2）")]
    public int heldObjectChildIndex = 2;

    void Update()
    {
        // 必要なコンポーネントが設定されていない場合は処理をスキップ
        if (selector == null || newParent == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Transform selection = selector.CurrentSelection;
            
            // 選択されたオブジェクトがない場合は処理をスキップ
            if (selection == null)
            {
                return;
            }

            var obj = selection.GetComponent<InteractableObject>();
            var type = obj?.type;

            if (type == ObjectType.Moveable)
            {
                // Moveableタイプの場合はDotweenを実行
                var mover = selection.GetComponent<DotweenMover>();
                if (mover != null)
                {
                    mover.MoveToTarget();
                }
            }
            else
            {
                // 左手に物を持っている場合
                if (LeftHand.isGrabbing && selector.LastHitPosition != Vector3.zero)
                {
                    // 保持しているオブジェクトを取得（安全に）
                    if (newParent.childCount > heldObjectChildIndex)
                    {
                        Transform heldObject = newParent.GetChild(heldObjectChildIndex);
                        if (heldObject != null)
                        {
                            // 視線の先にオブジェクトを配置
                            heldObject.SetParent(null);
                            heldObject.localRotation = Quaternion.identity;
                            heldObject.position = selector.LastHitPosition;

                            LeftHand.isGrabbing = false;

                            Debug.Log($"{heldObject.name}を{selector.LastHitPosition}に置いた");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"ObjectPicker: newParentに子オブジェクトが{heldObjectChildIndex + 1}個以上ありません。");
                    }
                }
                else
                {
                    // オブジェクトを拾う
                    if (selection != null)
                    {
                        // 親の位置にスナップして親子化
                        selection.SetParent(newParent);
                        selection.localPosition = Vector3.zero;
                        selection.localRotation = Quaternion.identity;

                        LeftHand.isGrabbing = true;

                        Debug.Log($"{selection.name}を{newParent.name}で持った！");
                    }
                }
            }
        }
    }
}