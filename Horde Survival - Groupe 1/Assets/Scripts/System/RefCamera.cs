using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefCamera : MonoBehaviour
{
    public static RefCamera Instance;

    public Camera camera;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        else
            Destroy(gameObject);
    }
}
