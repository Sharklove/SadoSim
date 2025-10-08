using UnityEngine;

public enum ObjectType
{
    Pickable,
    Moveable,
    Door,
    Switch
}

public class InteractableObject : MonoBehaviour
{
    public ObjectType type;
}
