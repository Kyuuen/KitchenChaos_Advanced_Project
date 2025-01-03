using UnityEngine;

public class PrefabIdentifier : MonoBehaviour
{
    public string prefabID;

    private void Awake()
    {
        if (string.IsNullOrEmpty(prefabID))
        {
            prefabID = System.Guid.NewGuid().ToString(); // Assign a new ID if missing
        }
    }
}