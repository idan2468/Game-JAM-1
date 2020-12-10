using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public enum Scene
    {
        StartScene = 0,
        GameScene = 1,
        EndScene = 2,
    };

    public void moveToScene(Scene scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((int)scene);
    }
    public void moveToScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void exitGame()
    {
        Application.Quit();
    }
    
}
