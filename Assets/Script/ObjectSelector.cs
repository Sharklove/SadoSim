using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public string selectableTag = "Selectable";
    public Material highlightMaterial;
    public float rayLength = 2f;

    private Transform _selection;
    private Material _originalMaterial;

    void Update()
    {
        // 前回選択していたオブジェクトをリセット
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            if (selectionRenderer != null && _originalMaterial != null)
            {
                selectionRenderer.material = _originalMaterial;
            }
            _selection = null;
        }

        // 中心からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            var selection = hit.transform;
            if (selection != null && selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null && highlightMaterial != null)
                {
                    _originalMaterial = selectionRenderer.material;
                    selectionRenderer.material = highlightMaterial;
                }
                _selection = selection;

                // クリックされたら DotweenTest を動かす
                if (Input.GetMouseButtonDown(0))
                {
                    var mover = selection.GetComponent<DotweenTest>();
                    if (mover != null)
                    {
                        mover.MoveToTarget();
                    }
                }
            }
        }
    }
}
