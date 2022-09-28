using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public static int pointCount;
    public int levelCount = 0;

    public ExpBar other;

    //collision entre le joueur et le point d'exp
    //compteur +1

    void Start()
    {
        other.SetStartExp();
    }

    void Update()
    {
        if(pointCount == 3 && levelCount == 0)
        { 
            levelCount += 1;
            Debug.Log("Level up !");
        }

        /*if (levelCount == 1 && other.currentXp == 10)
        {
            other.currentXp = 0;
            levelCount += 1;
        }*/
        
        other.UpdateExp();
  
    }
    
}


