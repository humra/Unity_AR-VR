using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class UIManager : MonoBehaviour
{
    public void buttonTest()
    {
        Debug.Log("Button clicked!");
    }

    public void MainMenu()
    {
        XRSettings.enabled = false;

        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        XRSettings.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
