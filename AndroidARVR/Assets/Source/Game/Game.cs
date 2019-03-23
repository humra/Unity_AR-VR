using UnityEngine;

public class Game : MonoBehaviour
{
    public static ControlType currentControlType;

    public Transform spawnPoint;

    Control currentControl;
    GameObject objectToPlace;

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

        //objectToPlace = Resources.Load<GameObject>("GamePiece");
        objectToPlace = Resources.Load<GameObject>("Projectile");

        initTools();
    }

    public void Update()
    {
        var placementPose = currentControl.getPlacementPose();
        if(placementPose != null)
        {
            //This will instantiate the object if it is valid
            //Instantiate(objectToPlace, placementPose.pose.position, placementPose.pose.rotation);
            Instantiate(objectToPlace, Camera.main.transform.position + Camera.main.transform.forward * 1f, Camera.main.transform.rotation);
        }
    }

    void initTools()
    {
        var debugControls = gameObject.AddComponent<VREditorControls>();
        debugControls.target = currentControl.transform;
    }
}

public enum ControlType
{
    VR,
    AR,
}