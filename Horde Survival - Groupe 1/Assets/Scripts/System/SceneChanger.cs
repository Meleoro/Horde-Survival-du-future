using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void NextScene() 
    { 
        SceneManager.LoadScene("Terri_Scene"); 
    }
}
