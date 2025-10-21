using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    public ObjectSelector selector;  // ObjectSelectorをアサイン
    public Transform newParent;      // 例えば PlayerHand など

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Transform selection = selector.CurrentSelection;
            var boj = selection?.GetComponent<InteractableObject>();
            var type = boj?.type;
            if (type == ObjectType.Moveable) {
                // アタッチされているDotweenを実行
                var mover = selection.GetComponent<DotweenMover>();
                if (mover != null)
                {
                    mover.MoveToTarget();
                }
            } else {
                if (LeftHand.isGrabbing && selector.LastHitPosition != Vector3.zero) {
                    // 左手に物を持っていたら、視線の先に左手に持っているものを置く
                    Transform heldObject = newParent.GetChild(2);
                    heldObject.SetParent(null);
                    heldObject.localRotation = Quaternion.identity; // 回転をリセット
                    heldObject.localPosition = selector.LastHitPosition; // 視線の先に配置

                    LeftHand.isGrabbing = false;

                    Debug.Log($"{LeftHand.isGrabbing}/{heldObject}を{selector.LastHitPosition}に置いた");
                } else {
                    if (selection != null) {
                        // 親の位置にスナップして親子化
                        selection.SetParent(newParent);
                        selection.localPosition = Vector3.zero;
                        selection.localRotation = Quaternion.identity;

                        LeftHand.isGrabbing = true;

                        Debug.Log($"{LeftHand.isGrabbing}/{selection.name}を{newParent.name}で持った！");
                    }
                }
            }
        }
    }
}