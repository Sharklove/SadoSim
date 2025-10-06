using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    public ObjectSelector selector;  // ObjectSelectorをアサイン
    public Transform newParent;      // 例えば PlayerHand など

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Transform selection = selector.CurrentSelection;
            if (selection != null)
            {
                // 親の位置にスナップして親子化
                selection.SetParent(newParent);
                selection.localPosition = Vector3.zero;
                selection.localRotation = Quaternion.identity;

                Debug.Log($"{selection.name} を {newParent.name} の位置にスナップしました。");
            }
        }
    }
}
