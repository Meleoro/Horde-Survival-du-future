using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public static int pointCount;
    public int levelCount = 0;

    //collision entre le joueur et le point d'exp
    //compteur +1

    void Update()
    { 
        if(pointCount == 3 && levelCount == 0)
        { 
            levelCount += 1;
            Debug.Log("Level up !");
        }
  
    }
    
}


