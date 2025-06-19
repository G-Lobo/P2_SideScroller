using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadMenuScene()
    {
        Debug.Log("Loading menu scene");
        SceneManager.LoadScene(0);
    }
    
    public void LoadGameScene()
    {
        Debug.Log("Loading game scene");
        SceneManager.LoadScene(1);
    }

    public void LoadAboutScene()
    {
        Debug.Log("Loading about scene");
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        QuitGame();
    }
}