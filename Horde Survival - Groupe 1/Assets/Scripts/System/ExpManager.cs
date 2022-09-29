using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements.Experimental;

public class ExpManager : MonoBehaviour
{
    public static int pointCount;
    public int levelCount = 0;

    public ExpBar other;

    //collision entre le joueur et le point d'exp
    //compteur +1

    //public Variables expLevel = Array(1, 2, 3, 4, 5);
    
    void Start()
    {
        other.SetStartExp();
    }

    void Update()
    {
        if(pointCount == 10 && levelCount == 0)
        { 
            levelCount += 1;
            Debug.Log("Level up !");
        }

        if (levelCount == 1 && ExpBar.currentXp == 10)
        {
            ExpBar.currentXp = 0;
        }
        
        other.UpdateExp();
  
    }
    
}


