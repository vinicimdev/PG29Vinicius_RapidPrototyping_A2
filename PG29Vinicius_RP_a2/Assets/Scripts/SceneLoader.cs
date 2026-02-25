using UnityEngine;
using UnityEngine.SceneManagement;

class SceneLoader : MonoBehaviour
{
    public void LoadSpecificScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}