using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ObjectData
{
    public string prefabID;  // Use prefabID instead of prefab name
    public Vector3 position;
    public Quaternion rotation;
}

[System.Serializable]
public class SceneData
{
    public List<ObjectData> objects = new List<ObjectData>();
}

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance { get; private set; }
    private const string CURRENT_LEVEL = "CurrentLevel";
    private string sceneSavePath;

    [Header("Save Settings")]
    public Transform parentObject; // Parent object to organize loaded objects
    public List<GameObject> spawnablePrefabs; // List of spawnable prefabs (assign in Inspector)


    private void Awake()
    {
        Instance = this;
        sceneSavePath = Application.persistentDataPath + "/sceneData.json";
    }

    public void SaveScene()
    {
        SceneData sceneData = new SceneData();

        foreach (Transform obj in parentObject)
        {
            // Get the PrefabIdentifier component
            PrefabIdentifier identifier = obj.GetComponent<PrefabIdentifier>();
            if (identifier != null)
            {
                sceneData.objects.Add(new ObjectData
                {
                    prefabID = identifier.prefabID, // Save prefabID
                    position = obj.position,
                    rotation = obj.rotation
                });
            }
        }

        string json = JsonUtility.ToJson(sceneData, true);
        File.WriteAllText(sceneSavePath, json);
        Debug.Log("Scene saved to " + sceneSavePath);
    }

    public void LoadScene()
    {
        if (File.Exists(sceneSavePath))
        {
            string json = File.ReadAllText(sceneSavePath);
            SceneData sceneData = JsonUtility.FromJson<SceneData>(json);

            // Clear all current children
            foreach (Transform child in parentObject)
            {
                Destroy(child.gameObject);
            }

            foreach (var data in sceneData.objects)
            {
                // Find prefab by prefabID
                GameObject prefab = spawnablePrefabs.Find(p =>
                {
                    PrefabIdentifier id = p.GetComponent<PrefabIdentifier>();
                    return id != null && id.prefabID == data.prefabID;
                });

                if (prefab != null)
                {
                    // Spawn a new object and set its position and rotation
                    GameObject newObject = Instantiate(prefab, data.position, data.rotation, parentObject);
                }
                else
                {
                    Debug.LogWarning($"Prefab with ID {data.prefabID} not found!");
                }
            }

            //Debug.Log("Scene loaded from " + sceneSavePath);
        }
        else
        {
            //Debug.LogWarning("No save file found at " + sceneSavePath);
        }
    }

    public void ResetData()
    {
        //Debug.Log("Deleting save file");
        File.Delete(sceneSavePath);
    }

    public bool IsSaveExist()
    {
        return File.Exists(sceneSavePath);
    }

    public void SaveLevel(int currentLevel)
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL, currentLevel);  
    }

    public int LoadLevel()
    {
        return PlayerPrefs.GetInt(CURRENT_LEVEL);
    }

    public int ResetLevel()
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL, 1);
        return PlayerPrefs.GetInt(CURRENT_LEVEL);
    }
}

