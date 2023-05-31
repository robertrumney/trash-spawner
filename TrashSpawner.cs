using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrashConfiguration
{
    public List<GameObject> trashPrefabs;
    public int trashCount;
}

public class TrashSpawner : MonoBehaviour
{
    public Vector3 size = Vector3.one;
    public List<TrashConfiguration> trashConfigs;
}
