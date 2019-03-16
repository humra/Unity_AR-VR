using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;
using UnityEngine.Experimental.XR;
using System;

public class ARContol : MonoBehaviour
{
    ARSessionOrigin arOrigin;
    Pose placementPose;
    bool placementPoseIsValid;
    GameObject indicator;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        indicator = Instantiate(Resources.Load<GameObject>("PlacementIndicator"));
    }

    void Update()
    {
        updatePlacementPose();
        updatePlacementIndicator();
        updateInput();
    }

    void updatePlacementIndicator()
    {
        if(placementPoseIsValid)
        {
            indicator.SetActive(true);
            indicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    void updatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if(placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            placementPose.rotation = Math.getBearing(Camera.current.transform.forward);
        }
    }

    void updateInput()
    {
        if(placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Instantiate(PlacementObjectData.getInstance().getRandomObject(), placementPose.position, placementPose.rotation);
        }
    }
}