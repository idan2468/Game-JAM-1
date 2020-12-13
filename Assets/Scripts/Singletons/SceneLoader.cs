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
        switch ((int) scene)
        {
            case 0:
                MusicController.Instance.PlayMenuBGM();
                break;
            case 1:
                MusicController.Instance.PlayGameBGM();
                break;
            case 2:
                MusicController.Instance.PlayGameBGM();
                break;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene((int)scene);
    }
    public void moveToScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Menu":
                MusicController.Instance.PlayMenuBGM();
                break;
            case "Main Game Scene":
                MusicController.Instance.PlayGameBGM();
                break;
            case "End_Game_Screen":
                MusicController.Instance.PlayGameBGM();
                break;
        }
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void exitGame()
    {
        Application.Quit();
    }
    
}
