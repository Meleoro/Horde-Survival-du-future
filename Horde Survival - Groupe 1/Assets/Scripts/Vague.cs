using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vague", menuName = "Vague")]
public class Vague : ScriptableObject
{
    public float duration;
    public float spawnRate;

    [Serializable]
    public class KeyValuePair {
        public GameObject key;
        public int val;
    }
 
    public List<KeyValuePair> spawnFrequency = new List<KeyValuePair>();
    Dictionary<GameObject, int> myDict = new Dictionary<GameObject, int>();
 
    void Awake() {
        foreach (var kvp in spawnFrequency) {
            myDict[kvp.key] = kvp.val;
        }
    }
}