using UnityEngine;

/// <summary>
/// インタラクティブオブジェクトのタイプ
/// </summary>
public enum ObjectType
{
    /// <summary>拾えるオブジェクト</summary>
    Pickable,
    /// <summary>移動可能なオブジェクト</summary>
    Moveable,
    /// <summary>ドア（未実装）</summary>
    Door,
    /// <summary>スイッチ（未実装）</summary>
    Switch
}

/// <summary>
/// インタラクション可能なオブジェクトにアタッチするコンポーネント
/// </summary>
public class InteractableObject : MonoBehaviour
{
    [Tooltip("オブジェクトのタイプ")]
    public ObjectType type;
}
