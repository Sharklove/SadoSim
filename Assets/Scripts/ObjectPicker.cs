using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    public ObjectSelector selector;  // ObjectSelectorをアサイン
    public Transform newParent;      // 例えば PlayerHand など

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (LeftHand.isGrabbing && selector.LastHitPosition != Vector3.zero) {
                Transform heldObject = newParent.GetChild(2);
                heldObject.SetParent(null);
                heldObject.localPosition = selector.LastHitPosition;

                LeftHand.isGrabbing = false;

                Debug.Log($"{LeftHand.isGrabbing}/{heldObject}を{selector.LastHitPosition}に置いた");
            } else {
                Transform selection = selector.CurrentSelection;
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