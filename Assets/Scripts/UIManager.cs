using UnityEngine;

public class UIManager : MonoBehaviour
{
    // シングルトンインスタンス(ゲームオーバー時など、ボタン以外でToggleUI()にアクセスしたい場合は、有効にする)
    // public static UIManager Instance { get; private set; }
    
    // UIが表示されているかどうかの状態
    public static bool IsUIActive { get; private set; } = true;

    // UIパネルを割り当てるための変数
    [SerializeField] private GameObject UIPanel;

    // private void Awake()
    // {
    //     Instance = this;
    // }

    public void Start()
    {
        IsUIActive = UIPanel.activeSelf;
    }

    // UIの表示を切り替えるメソッド
    public void ToggleUI(bool isVisible)
    {
        UIPanel.SetActive(isVisible);
        IsUIActive = isVisible;
    }
}