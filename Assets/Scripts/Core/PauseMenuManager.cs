using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI 引用")]
    public GameObject pauseMenuUI; // 拖入你的 PauseMenuPanel

    private bool isPaused = false; // 记录当前游戏是否处于暂停状态

    void Update()
    {
        // 检测玩家是否按下了 P 键
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume(); // 如果已经暂停，则恢复游戏
            }
            else
            {
                Pause(); // 如果未暂停，则暂停游戏
            }
        }
    }

    // 回到游戏
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // 隐藏暂停菜单
        Time.timeScale = 1f;          // 恢复游戏时间流逝
        isPaused = false;             // 更新状态
    }

    // 暂停游戏
    void Pause()
    {
        pauseMenuUI.SetActive(true);  // 显示暂停菜单
        Time.timeScale = 0f;          // 将时间流逝设为0，冻结游戏逻辑
        isPaused = true;              // 更新状态
    }

    // 退出游戏
    public void QuitGame()
    {
        // 注意：退出前最好把时间恢复正常，避免下次加载场景时出现奇怪的Bug
        Time.timeScale = 1f;

        Debug.Log("正在关闭游戏...");
        Application.Quit(); // 打包后执行关闭应用
    }
}