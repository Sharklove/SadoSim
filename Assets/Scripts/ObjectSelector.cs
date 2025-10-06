using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public string selectableTag = "Selectable";
    public Material highlightMaterial;
    public float rayLength = 2f;

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
                // 🌟 座標の取得はここで行う 🌟
                Vector3 hitPosition = hit.point;
                
                // デバッグ出力
                Debug.Log("Rayがオブジェクトに当たったワールド座標: " + hitPosition.ToString());

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
    }
}
