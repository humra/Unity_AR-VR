using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AndroidAR/ObjectsData", fileName = "MyObjects")]
public class PlacementObjectData : ScriptableObject
{
    public GameObject[] myObjects;

    static PlacementObjectData instance;
    public static PlacementObjectData getInstance()
    {
        if(instance == null)
        {
            instance = Resources.Load<PlacementObjectData>("Data/MyObjects");
        }
        return instance;
    }

    public GameObject getRandomObject()
    {
        return myObjects[Random.Range(0, myObjects.Length)];
    }
}
