using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EXP : MonoBehaviour, EXP.ICollectible
{

    public static event Action OnExCollected;

    public void Collect()
    {
        Debug.Log("EXP Collected");
        Destroy(gameObject);
        OnExCollected?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public interface ICollectible
    {
        public void Collect();
        
    }
}
