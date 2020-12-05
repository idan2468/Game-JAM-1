using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private static SceneLoader instance = null;
    public enum Scene
    {
        StartScene = 0,
        HowToPlayScene = 1,
        GameScene = 2,
        EndScene = 3,
    };


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }


    public static SceneLoader getInstance()
    {
        return instance;
    }

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


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
