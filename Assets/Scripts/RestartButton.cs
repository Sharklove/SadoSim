using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームを再開するボタンの機能を提供するクラス
/// </summary>
public class RestartButton : MonoBehaviour
{
    /// <summary>
    /// 現在のシーンを再読み込みしてゲームを再開
    /// </summary>
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene != null)
        {
            SceneManager.LoadScene(currentScene.name);
        }
        else
        {
            Debug.LogError("RestartButton: 現在のシーンを取得できませんでした。");
        }
    }
}