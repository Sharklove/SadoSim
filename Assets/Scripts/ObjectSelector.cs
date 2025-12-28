using UnityEngine;

/// <summary>
/// レイキャストを使用してオブジェクトを選択し、ハイライト表示するクラス
/// </summary>
public class ObjectSelector : MonoBehaviour
{
    [Tooltip("選択可能なオブジェクトのタグ")]
    public string selectableTag = "Selectable";
    
    [Tooltip("ハイライト表示用のマテリアル")]
    public Material highlightMaterial;
    
    [Tooltip("レイキャストの最大距離")]
    public float rayLength = 2f;

    /// <summary>
    /// 最後にヒットした位置
    /// </summary>
    public Vector3 LastHitPosition { get; private set; } = Vector3.zero;

    private Transform _selection;
    private Material _originalMaterial;

    /// <summary>
    /// 現在選択中のオブジェクト
    /// </summary>
    public Transform CurrentSelection => _selection;

    private Camera mainCamera;

    void Awake()
    {
        // メインカメラを取得（毎フレーム取得するのを避けるため）
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogWarning("ObjectSelector: メインカメラが見つかりません。");
        }
    }

    void Update()
    {
        // カメラが設定されていない場合は処理をスキップ
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                return;
            }
        }

        // 前回の選択を解除
        if (_selection != null)
        {
            var renderer = _selection.GetComponent<Renderer>();
            if (renderer != null && _originalMaterial != null)
            {
                renderer.material = _originalMaterial;
            }
            _selection = null;
            _originalMaterial = null;
        }

        // 画面中央からレイを発射
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            LastHitPosition = hit.point;
            var selection = hit.transform;
            
            // 選択可能なタグかどうかを確認
            if (selection != null && selection.CompareTag(selectableTag))
            {
                var renderer = selection.GetComponent<Renderer>();
                if (renderer != null && highlightMaterial != null)
                {
                    // 元のマテリアルを保存してハイライトマテリアルに変更
                    _originalMaterial = renderer.material;
                    renderer.material = highlightMaterial;
                }
                _selection = selection;
            }
            else
            {
                // 選択可能なオブジェクトでない場合は位置のみ保存
                LastHitPosition = hit.point;
            }
        }
        else
        {
            // ヒットしなかった場合はゼロベクトルを設定
            LastHitPosition = Vector3.zero;
        }
    }

    void OnDisable()
    {
        // 無効化されたときにハイライトを解除
        if (_selection != null && _originalMaterial != null)
        {
            var renderer = _selection.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = _originalMaterial;
            }
        }
    }
}
