using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public string selectableTag = "Selectable";
    public Material highlightMaterial;
    public float rayLength = 2f;
    public Vector3 LastHitPosition { get; private set; } = Vector3.zero;

    private Transform _selection;
    private Material _originalMaterial;

    // 現在選択中のオブジェクトを他スクリプトが参照できるように
    public Transform CurrentSelection => _selection;

    void Update()
    {
        // 前回の選択解除
        if (_selection != null)
        {
            var renderer = _selection.GetComponent<Renderer>();
            if (renderer != null && _originalMaterial != null)
            {
                renderer.material = _originalMaterial;
            }
            _selection = null;
        }

        // Rayを発射
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            LastHitPosition = hit.point; // 最後にヒットした位置を保存
            // Debug.Log($"Raycast hit: {hit.transform.name} at {LastHitPosition}");
            var selection = hit.transform;
            if (selection != null && selection.CompareTag(selectableTag))
            {
                var renderer = selection.GetComponent<Renderer>();
                if (renderer != null && highlightMaterial != null)
                {
                    _originalMaterial = renderer.material;
                    renderer.material = highlightMaterial;
                }
                _selection = selection;
            }
        }
        else
        {
            LastHitPosition = Vector3.zero; // ヒットしなかった場合はゼロベクトルを設定
        }
    }
}
