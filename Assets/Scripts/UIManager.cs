using UnityEngine;

/// <summary>
/// UIの表示/非表示を管理するクラス
/// </summary>
public class UIManager : MonoBehaviour
{
    // シングルトンインスタンス(ゲームオーバー時など、ボタン以外でToggleUI()にアクセスしたい場合は、有効にする)
    // public static UIManager Instance { get; private set; }

    /// <summary>
    /// UIが表示されているかどうかの状態
    /// </summary>
    public static bool IsUIActive { get; private set; } = true;

    [Tooltip("UIパネルのGameObject")]
    [SerializeField] private GameObject UIPanel;

    // private void Awake()
    // {
    //     Instance = this;
    // }

    void Start()
    {
        if (UIPanel != null)
        {
            IsUIActive = UIPanel.activeSelf;
        }
        else
        {
            Debug.LogWarning("UIManager: UIPanelが設定されていません。");
            IsUIActive = false;
        }
    }

    /// <summary>
    /// UIの表示を切り替える
    /// </summary>
    /// <param name="isVisible">表示する場合はtrue、非表示にする場合はfalse</param>
    public void ToggleUI(bool isVisible)
    {
        if (UIPanel == null)
        {
            Debug.LogWarning("UIManager: UIPanelが設定されていません。");
            return;
        }

        UIPanel.SetActive(isVisible);
        IsUIActive = isVisible;
    }
}