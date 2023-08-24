using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 为了使用 Unity 的 UI 模块
public class Main : MonoBehaviour
{
    // 开始游戏按钮
    [SerializeField] private Button beginButton;
    // Start is called before the first frame update
    void Start()
    {
        beginButton = GetComponent<Button>();
        beginButton.onClick.AddListener(Next);
    }
    void Next()
    {
        Time.timeScale = 1f;
        int currLV = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currLV + 1);
    }
}