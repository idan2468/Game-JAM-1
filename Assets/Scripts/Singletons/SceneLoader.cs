using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private void Start()
    {
        MusicController.Instance.PlayMenuBGM();
    }

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

        SceneManager.LoadScene((int)scene);
        // Time.timeScale = 1f;
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

        SceneManager.LoadScene(sceneName);
        // Time.timeScale = 1f;
    }
    
    public void exitGame()
    {
        Application.Quit();
    }
    
}
