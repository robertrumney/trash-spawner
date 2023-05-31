#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrashSpawner))]
public class TrashSpawnerEditor : Editor
{
    void OnSceneGUI()
    {
        TrashSpawner trashSpawner = (TrashSpawner)target;
        
        // draw spawn area with handle
        trashSpawner.size = Handles.ScaleHandle(trashSpawner.size, trashSpawner.transform.position, Quaternion.identity, HandleUtility.GetHandleSize(trashSpawner.transform.position));

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TrashSpawner trashSpawner = (TrashSpawner)target;
        if(GUILayout.Button("Spawn Trash"))
        {
            foreach(var trashConfig in trashSpawner.trashConfigs)
            {
                SpawnTrash(trashSpawner, trashConfig);
            }
        }
    }

    private void SpawnTrash(TrashSpawner trashSpawner, TrashConfiguration trashConfig)
    {
        for (int i = 0; i < trashConfig.trashCount; i++)
        {
            GameObject trashPrefab = trashConfig.trashPrefabs[Random.Range(0, trashConfig.trashPrefabs.Count)];
            Vector3 spawnPosition = GetRandomPositionInArea(trashSpawner);
            Ray ray = new Ray(spawnPosition + Vector3.up * 10, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject trash = Instantiate(trashPrefab, hit.point, Quaternion.identity);
                Undo.RegisterCreatedObjectUndo(trash, "Spawned Trash");
            }
        }
    }

    private Vector3 GetRandomPositionInArea(TrashSpawner trashSpawner)
    {
        Vector3 halfSize = trashSpawner.size / 2;
        Vector3 randomPositionWithinArea = new Vector3(
            Random.Range(-halfSize.x, halfSize.x),
            Random.Range(-halfSize.y, halfSize.y),
            Random.Range(-halfSize.z, halfSize.z));

        return trashSpawner.transform.position + randomPositionWithinArea;
    }
}
#endif
