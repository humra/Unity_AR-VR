using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARControl : MonoBehaviour
{
    private ARSessionOrigin sessionOrigin;
    private Pose placementPose;
    private bool isValidPose;

    private void Start()
    {
        sessionOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    private void Update()
    {
        UpdatePlacementPose();
        UpdateInput();
    }

    private void UpdatePlacementPose()
    {
        Vector3 origin = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        sessionOrigin.Raycast(origin, hits);

        if(hits.Count > 0)
        {
            isValidPose = true;
            placementPose = hits[0].pose;
        }
        else
        {
            isValidPose = false;
        }
    }

    private void UpdateInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            
        }
    }
}
