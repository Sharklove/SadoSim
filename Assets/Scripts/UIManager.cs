using UnityEngine;

public class UIManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static UIManager Instance { get; private set; }
    
    // UIが表示されているかどうかの状態
    public static bool IsUIActive { get; private set; } = true;

    // UIパネルを割り当てるための変数
    [SerializeField] private GameObject UIPanel;

    void Awake()
    {
        // インスタンスがなければ自身を代入
        if (Instance == null)
        {
            Instance = this;
            // シーンを跨いでインスタンスを保持したい場合は以下を使う
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // UIの表示を切り替えるメソッド
    public void ToggleUI(bool isVisible)
    {
        UIPanel.SetActive(isVisible);
        IsUIActive = isVisible;
    }
}