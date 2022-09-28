using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Projectiles
{
    public class ObjectPooling : MonoBehaviour
    {
        public static ObjectPooling Instance;

        private Dictionary<string, List<GameObject>> _poolDictionary = new Dictionary<string, List<GameObject>>();

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
            
            _poolDictionary.Add(poolData[0].Prefab.name, new List<GameObject>());
            _poolDictionary.Add(poolData[1].Prefab.name, new List<GameObject>());
            _poolDictionary.Add(poolData[2].Prefab.name, new List<GameObject>());
            _poolDictionary.Add(poolData[3].Prefab.name, new List<GameObject>());
            // //UZI AMMO INSTANTIATION
            // for (int i = 0; i < poolData[0].NumberOfProjectiles; i++)
            // {
            //     GameObject obj = Instantiate(poolData[0].Prefab);
            //     obj.SetActive(false);
            //     _poolDictionary[poolData[0].Prefab.name].Add(obj);
            // }

            foreach (var entry in poolData)
            {
                for (int i = 0; i < poolData[i].NumberOfProjectiles; i++)
                {
                    GameObject obj = Instantiate(poolData[i].Prefab);
                    obj.SetActive(false);
                    _poolDictionary[poolData[i].Prefab.name].Add(obj);
                    Debug.Log(3);
                }
            }
            
            foreach (KeyValuePair<string, List<GameObject>> entry in _poolDictionary)
            {
                
            }
            
            // int i;
            // for (i = 0; i < _poolDictionary; i++)
            //     {
            //         for (i = 0; i < poolData[i].NumberOfProjectiles; i++)
            //         {
            //             GameObject obj = Instantiate(poolData[i].Prefab);
            //             obj.SetActive(false);
            //             _poolDictionary[poolData[i].Prefab.name].Add(obj);
            //             Debug.Log(3);
            //         }
            //     }
                
                

            // //CCCANON AMMO INSTANTIATION
            // for (int i = 0; i < poolData[1].NumberOfProjectiles; i++)
            // {
            //     GameObject obj = Instantiate(poolData[1].Prefab);
            //     obj.SetActive(false);
            //     ccCanonAmmo.Add(obj);
            // } 
            //
            // //MINIGUN AMMO INSTANTIATION
            // for (int i = 0; i < poolData[2].NumberOfProjectiles; i++)
            // {
            //     GameObject obj = Instantiate(poolData[2].Prefab);
            //     obj.SetActive(false);
            //     miniGunAmmo.Add(obj);
            // }
            //
            // //GRENADE LAUNCHER AMMO INSTANTIATION
            // for (int i = 0; i < poolData[3].NumberOfProjectiles; i++)
            // {
            //     GameObject obj = Instantiate(poolData[3].Prefab);
            //     obj.SetActive(false);
            //     grenadeLauncherAmmo.Add(obj);
            // }
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
