using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static ControlType currentControlType;
    public int targetsLeft;
    public Transform spawnPoint;

    Control currentControl;
    GameObject objectToPlace;
    int shotsLeft = 5;
    int lastLevelIndex = 2;
    Button endLevelButton;

    public void Awake()
    {
        if(currentControlType == ControlType.AR)
        {
            currentControl = Instantiate(Resources.Load<Control>("Controls/ARControl"));
        }
        else if(currentControlType == ControlType.VR)
        {
            currentControl = Instantiate(Resources.Load<Control>("Controls/VRControl"));
        }
        currentControl.transform.SetParent(spawnPoint);
        currentControl.transform.localPosition = Vector3.zero;
        currentControl.transform.localRotation = Quaternion.identity;
        currentControl.setIndicator(Instantiate(Resources.Load<GameObject>("PlacementIndicator")));

        objectToPlace = Resources.Load<GameObject>("Projectile");

        initTools();
    }

    private void Start()
    {
        var targets = FindObjectsOfType<Target>();
        targetsLeft = targets.Length;
        UpdateUI();
        endLevelButton = GameObject.FindGameObjectWithTag("NextLevelButton").GetComponent<Button>();
        endLevelButton.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("EndLevelText").GetComponent<Text>().text = "";
    }

    public void Update()
    {
        var placementPose = currentControl.getPlacementPose();
        if(placementPose != null && shotsLeft > 0 && targetsLeft > 0)
        {
            Instantiate(objectToPlace, Camera.main.transform.position + Camera.main.transform.forward * 1f, Camera.main.transform.rotation);
            shotsLeft--;
            UpdateUI();
        }

        if(targetsLeft == 0)
        {
            LevelComplete();
        }
        else if(shotsLeft == 0)
        {
            LevelFailed();
        }
    }

    private void initTools()
    {
        var debugControls = gameObject.AddComponent<VREditorControls>();
        debugControls.target = currentControl.transform;
    }

    public void TargetDestroyed()
    {
        targetsLeft--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        GameObject.FindGameObjectWithTag("ShotsLeftCounter").GetComponent<Text>().text = shotsLeft.ToString();
        GameObject.FindGameObjectWithTag("TargetsLeftCounter").GetComponent<Text>().text = targetsLeft.ToString();
    }

    private void LevelFailed()
    {
        GameObject.FindGameObjectWithTag("EndLevelText").GetComponent<Text>().text = "GAME OVER";
    }

    private void LevelComplete()
    {
        if(SceneManager.GetActiveScene().buildIndex == lastLevelIndex)
        {
            GameObject.FindGameObjectWithTag("EndLevelText").GetComponent<Text>().text = "GAME COMPLETE";
        }
        else
        {
            GameObject.FindGameObjectWithTag("EndLevelText").GetComponent<Text>().text = "LEVEL COMPLETE";
            if(SceneManager.GetActiveScene().buildIndex != lastLevelIndex)
            {
                endLevelButton.gameObject.SetActive(true);
            }
        }
    }
}

public enum ControlType
{
    VR,
    AR,
}