using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startARGame()
    {
        Game.currentControlType = ControlType.AR;
        loadARScene();
    }

    public void startVRGame()
    {
        Game.currentControlType = ControlType.VR;
        loadVRScene();
    }

    void loadVRScene()
    {
        SceneManager.LoadScene("Level01");
    }

    void loadARScene()
    {
        SceneManager.LoadScene("Level01AR");
    }
}