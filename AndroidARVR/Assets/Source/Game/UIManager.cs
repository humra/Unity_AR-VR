using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class UIManager : MonoBehaviour
{

    public void MainMenu()
    {
        XRSettings.enabled = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        XRSettings.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        XRSettings.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
