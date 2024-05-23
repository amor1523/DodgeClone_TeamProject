using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{

    public void StartGameBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif


    }
}
