using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Start"); 
    }

    public void Tutorial1()
    {
        SceneManager.LoadScene("Tutorial1");
    }

    public void Tutorial2()
    {
        SceneManager.LoadScene("Tutorial2");
    }

    public void Tutorial3()
    {
        SceneManager.LoadScene("Tutorial3");
    }

    public void Test()
    {
        SceneManager.LoadScene("Test");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
