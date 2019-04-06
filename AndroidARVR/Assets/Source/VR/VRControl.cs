using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;

public class VRControl : Control
{
    bool canPlace = false;
    Vector3 currentHit;
    float placeCooldown = 0;
    EventSystem eventSystem;
    GraphicRaycaster UIRaycast;
    float startTime;
    bool readsInput = false;

    public void Start()
    {
        StartCoroutine(LoadDevice("cardboard"));
        UIRaycast = GameObject.FindObjectOfType<GraphicRaycaster>();    //Every Canvas has its own GraphicRaycaster
        eventSystem = GameObject.FindObjectOfType<EventSystem>();       //There is one EventSystem for every scene
        startTime = Time.time;
    }

    IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = true;
    }

    public override PlacementPose getPlacementPose()
    {
        //if (canPlace && placeCooldown <= 0 && currentHit != Vector3.zero)
        //{
        //    placeCooldown = 2f;
        //    return new PlacementPose()
        //    {
        //        pose = new Pose(currentHit, Math.getBearing(Camera.main.transform.forward))
        //    };
        //}

        //return null;
        if(canPlace && placeCooldown <= 0)
        {
            placeCooldown = 2f;

            return new PlacementPose()
            {
                pose = new Pose(Camera.main.transform.position, Quaternion.identity)
            };
        }

        return null;
    }

    public void Update()
    {
        var camera = Camera.main;

        if(Time.time > startTime + 3f)
        {
            readsInput = true;
        }
        
        //This reads the tilt
        if (Mathf.Abs(Mathf.DeltaAngle(camera.transform.eulerAngles.z, 0)) > 25 && readsInput)
        {
            canPlace = true;
        }
        else
        {
            canPlace = false;
        }

        if (placeCooldown > 0)
        {
            placeCooldown -= Time.deltaTime;
        }

        bool success = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 1000);
        if (success)
        {
            currentHit = hit.point;

            indicator.transform.position = currentHit + hit.normal * 0.05f;
            indicator.transform.up = hit.normal;
            indicator.transform.localScale = Vector3.one * 2;

            Vector3 lookAt = Vector3.Cross(-hit.normal, Camera.main.transform.right);
            lookAt = lookAt.y < 0 ? -lookAt : lookAt;
            indicator.transform.rotation = Quaternion.LookRotation(hit.point + lookAt, hit.normal);
        }
        else
        {
            currentHit = Vector3.zero;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Mathf.Abs(Mathf.DeltaAngle(camera.transform.eulerAngles.z, 0)) > 25) && readsInput)    //Useful in Unity editor, needs to change in build 
        {
            var pointerData = new PointerEventData(eventSystem);
            pointerData.position = new Vector2(XRSettings.eyeTextureWidth * 0.5f, XRSettings.eyeTextureHeight * 0.5f);  //Set here the pixel position of where you need to click

            List<RaycastResult> results = new List<RaycastResult>();
            UIRaycast.Raycast(pointerData, results);    //Raycasts

            foreach (RaycastResult result in results)
            {
                Button btn = result.gameObject.GetComponent<Button>();  //If raycasted button, click it
                if (btn != null)
                {
                    btn.onClick.Invoke();
                }
            }
        }
    }
}