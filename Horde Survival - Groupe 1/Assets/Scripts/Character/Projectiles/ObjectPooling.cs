using System;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Projectiles
{
    public class ObjectPooling : MonoBehaviour
    {
        public static ObjectPooling Instance;
   
        [SerializeField] private List<PoolData> poolData;
        [SerializeField] private List<GameObject> uziAmmo;
        [SerializeField] private List<GameObject> miniGunAmmo;
        [SerializeField] private List<GameObject> ccCanonAmmo;
        [SerializeField] private List<GameObject> grenadeLauncherAmmo;

        private float _nextFireTime;

        private void Awake()
        {
            //Singleton
            if (Instance == null) Instance = this;

            InitialInstantiation();
        }
        
        void InitialInstantiation()
        {
            
            //UZI AMMO INSTANTIATION
            for (int i = 0; i < poolData[0].NumberOfProjectiles; i++)
            {
                GameObject obj = Instantiate(poolData[0].Prefab);
                obj.SetActive(false);
                uziAmmo.Add(obj);
            } 
            
            //CCCANON AMMO INSTANTIATION
            for (int i = 0; i < poolData[1].NumberOfProjectiles; i++)
            {
                GameObject obj = Instantiate(poolData[1].Prefab);
                obj.SetActive(false);
                ccCanonAmmo.Add(obj);
            } 
            
            //MINIGUN AMMO INSTANTIATION
            for (int i = 0; i < poolData[2].NumberOfProjectiles; i++)
            {
                GameObject obj = Instantiate(poolData[2].Prefab);
                obj.SetActive(false);
                miniGunAmmo.Add(obj);
            }
            
            //GRENADE LAUNCHER AMMO INSTANTIATION
            for (int i = 0; i < poolData[3].NumberOfProjectiles; i++)
            {
                GameObject obj = Instantiate(poolData[3].Prefab);
                obj.SetActive(false);
                grenadeLauncherAmmo.Add(obj);
            }
        }
        
        public GameObject ShootWithUzi()
        {
            for (int i = 0; i < uziAmmo.Count; i++)
            {
                if (!uziAmmo[i].activeInHierarchy)
                {
                    return uziAmmo[i];
                }
            }
            return null;
        }
        
        public GameObject ShootWithCcCanon()
        {
            for (int i = 0; i < ccCanonAmmo.Count; i++)
            {
                if (!ccCanonAmmo[i].activeInHierarchy)
                {
                    return ccCanonAmmo[i];
                }
            }
            return null;
        }
        public GameObject ShootWithMinigun()
        {
            for (int i = 0; i < miniGunAmmo.Count; i++)
            {
                if (!miniGunAmmo[i].activeInHierarchy)
                {
                    return miniGunAmmo[i];
                }
            }
            return null;
        }
        
        public GameObject ShootWithGrenadeLauncher()
        {
            for (int i = 0; i < grenadeLauncherAmmo.Count; i++)
            {
                if (!grenadeLauncherAmmo[i].activeInHierarchy)
                {
                    return grenadeLauncherAmmo[i];
                }
            }
            return null;
        }
    }



    //It allows me to have a custom list of elements in the inspector
    [Serializable]
    public class PoolData
    {
        [SerializeField]
        private GameObject prefab;
        [SerializeField]
        private int numberOfProjectiles;
        
        public GameObject Prefab => prefab; //Get without Set 
        public int NumberOfProjectiles => numberOfProjectiles; //Get without Set 

    }
    
}
