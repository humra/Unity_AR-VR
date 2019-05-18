using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ARContol : Control
{
    ARSessionOrigin arOrigin;
    Pose placementPose;
    bool placementPoseIsValid;
    float placeCooldown = 0;
    float startTime;
    bool readsInput = false;
    bool shoot = false;

    void Start()
    {
        XRSettings.LoadDeviceByName("");
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        startTime = Time.time;
    }

    public override PlacementPose getPlacementPose()
    {
        if (placeCooldown <= 0 && shoot)
        {
            placeCooldown = 2f;
            shoot = false;

            return new PlacementPose()
            {
                pose = new Pose(Camera.main.transform.position, Quaternion.identity)
            };
        }

        return null;
    }

    public void Update()
    {

        if (Time.time > startTime + 3f)
        {
            readsInput = true;
        }

        if (placeCooldown > 0)
        {
            placeCooldown -= Time.deltaTime;
        }

        if(Input.touchCount > 0 && readsInput && placeCooldown <= 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                shoot = true;
            }
        }
    }
}
