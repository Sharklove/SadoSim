using UnityEngine;

/// <summary>
/// カメラの上下回転を制御するクラス
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("選択可能なオブジェクトのタグ")]
    public string selectableTag = "Selectable";
    
    [Tooltip("ハイライト表示用のマテリアル")]
    public Material highlightMaterial;
    
    [Tooltip("レイキャストの最大距離")]
    public float rayLength = 2f;

    private float rotationY = 0f;

    /// <summary>
    /// 最後にヒットした位置
    /// </summary>
    public Vector3 LastHitPosition { get; private set; } = Vector3.zero;

    /// <summary>
    /// 現在選択中のオブジェクト
    /// </summary>
    public Transform CurrentSelection { get; private set; } = null;

    private Material _originalMaterial;
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
        // 前回の選択を解除
        if (CurrentSelection != null)
        {
            var renderer = CurrentSelection.GetComponent<Renderer>();
            if (renderer != null && _originalMaterial != null)
            {
                renderer.material = _originalMaterial;
            }
            CurrentSelection = null;
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
                CurrentSelection = selection;
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
        if (CurrentSelection != null && _originalMaterial != null)
        {
            var renderer = CurrentSelection.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = _originalMaterial;
            }
        }
    }

    /// <summary>
    /// カメラの角度を変更
    /// </summary>
    /// <param name="angle">変更する角度（マウス入力）</param>
    /// <param name="minY">最小角度（下方向の制限）</param>
    /// <param name="maxY">最大角度（上方向の制限）</param>
    public void ChangeAngles(float angle, float minY, float maxY)
    {
        rotationY = Mathf.Clamp(rotationY + -angle, minY, maxY);
        transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    }

    /// <summary>
    /// カメラの回転角度をリセット
    /// </summary>
    public void ResetRotation()
    {
        rotationY = 0f;
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
