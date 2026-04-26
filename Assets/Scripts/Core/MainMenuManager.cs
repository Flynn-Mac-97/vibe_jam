using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理命名空间

public class MainMenuManager : MonoBehaviour
{
    // 绑定到“开始游戏”按钮
    public void StartGame()
    {
        // 加载你的游戏主场景，这里必须和你截图中的场景名称完全一致
        SceneManager.LoadScene("VibeJam Game");
    }

    // 绑定到“结束游戏”按钮
    public void QuitGame()
    {
        // 这个 Log 只是为了在 Unity 编辑器里测试时能看到效果
        Debug.Log("游戏已退出！");

        // 打包成执行文件后，这行代码才会真正关闭游戏
        Application.Quit();
    }
}