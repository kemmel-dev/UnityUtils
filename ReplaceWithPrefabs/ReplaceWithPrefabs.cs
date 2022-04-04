using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplaceWithPrefabs : MonoBehaviour
{

    public List<TransformList> transformLists = new List<TransformList>();
    public List<GameObject> replacementPrefabs = new List<GameObject>();

    private void Start()
    {
        for (int replacementIndex = 0; replacementIndex < replacementPrefabs.Count; replacementIndex++)
        {
            GameObject replacementPrefab = replacementPrefabs[replacementIndex];
            foreach (Transform toBeReplaced in transformLists[replacementIndex].toBeReplaced)
            {
                // Get the Path to Prefab
                string prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(replacementPrefab);

                // Load Prefab Asset as Object from path
                UnityEngine.Object _newObject = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(UnityEngine.Object));

                //Instantiate the Prefab in the scene, as a child of the GO this script runs on
                GameObject _newPrefabInstance = PrefabUtility.InstantiatePrefab(_newObject) as GameObject;
                _newPrefabInstance.transform.position = toBeReplaced.transform.position;
                _newPrefabInstance.transform.rotation = toBeReplaced.transform.rotation;
                _newPrefabInstance.transform.localScale = toBeReplaced.transform.localScale;

                Destroy(toBeReplaced.gameObject);
            }
        }
    }

}

[Serializable]
public class TransformList
{
    public List<Transform> toBeReplaced;
}
