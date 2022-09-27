using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [Header("GestionVagues")]
    public List<Vague> vagues;
    private int currentVagueNumber;
    private Vague currentVague;
    private float vagueDuration;
    private float timerVague;
    
    
    [HideInInspector] public List<GameObject> ennemies;
    public GameObject test1;
    
    [Header("Paramètres")] 
    public float spawnFrequency;

    [Header("Autres")]
    private float timerSpawn;
    private bool spawnOnX;
    private float spawnX;
    private float spawnY;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(gameObject);

        currentVague = vagues[currentVagueNumber];
        timerVague = currentVague.duration;
    }


    void Update()
    {
        timerSpawn -= Time.deltaTime;
        timerVague -= Time.deltaTime;
        
        // CHANGEMENT DE VAGUE
        if (timerVague <= 0)
        {
            ChangeVague();
        }
        
        // SPAWN D'UN ENNEMIE
        if (timerSpawn <= 0)
        {
            timerSpawn = spawnFrequency;
            
            //SpawnEntity(test1);
        }
    }


    private void SelectSpawnEntity()
    {
        /*foreach (GameObject ennemy in currentVague.spawnRate.)
        {
            
        }*/
    }
    
    private void SpawnEntity(GameObject entity)
    {
        // Pour eviter que le spawn se fasse toujours en haut, a gauche, ect.
        spawnOnX = !spawnOnX;
            
        // A HAUT OU EN BAS
        if (!spawnOnX)
        {
            int up = Random.Range(0, 2);

            // EN HAUT
            if (up == 0)
            {
                spawnX = Random.Range(-17, 17);
                spawnY = Random.Range(10, 12);
            }

            // EN BAS
            else
            {
                spawnX = Random.Range(-17, 17);
                spawnY = Random.Range(-12, -10);
            }
        }
            
        // A GAUCHE OU A DROITE
        else
        {
            int left = Random.Range(0, 2);

            // A GAUCHE
            if (left == 0)
            {
                spawnX = Random.Range(-19, -17);
                spawnY = Random.Range(-10, 10);
            }

            // A DROITE
            else
            {
                spawnX = Random.Range(17, 19);
                spawnY = Random.Range(-10, 10);
            }
        }
            
        GameObject newEnnemy =  Instantiate(entity, CharacterControler.Instance.transform.position + new Vector3(spawnX, spawnY, 0), Quaternion.identity);
        
        ennemies.Add(newEnnemy);
    }

    private void ChangeVague()
    {
        currentVagueNumber += 1;
        
        currentVague = vagues[currentVagueNumber];
        timerVague = currentVague.duration;
    }
}
