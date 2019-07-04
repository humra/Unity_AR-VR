using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    int targetCount = 0;
    int shotsRemaining = 5;

    private void Start()
    {
        targetCount = GameObject.FindObjectsOfType<Target>().Length;
        shotsRemaining = 5;
    }

    public void TargetDestroyed()
    {
        targetCount--;
        CheckForLevelComplete();
    }

    public void CheckForLevelComplete()
    {
        if(targetCount == 0)
        {
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                //No clue
            }
        }
    }
}
