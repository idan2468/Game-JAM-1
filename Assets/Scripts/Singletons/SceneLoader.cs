using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public enum Scene
    {
        StartScene = 0,
        HowToPlayScene = 1,
        GameScene = 2,
        EndScene = 3,
    };

    public void moveToScene(Scene scene)
    {
        SceneManager.LoadScene((int)scene);
    }
    public void moveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void exitGame()
    {
        Application.Quit();
    }
    
}
