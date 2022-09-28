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
            for (int j = 0; j < poolData.Count; j++)
            {
                _poolDictionary.Add(poolData[j].Prefab.name, new List<GameObject>());
                
                for (int i = 0; i < poolData[j].NumberOfProjectiles; i++)
                {
                    GameObject obj = Instantiate(poolData[j].Prefab);
                    obj.SetActive(false);
                    _poolDictionary[poolData[j].Prefab.name].Add(obj);
                    GetObject(obj.name);
                }
            }
        }

        public GameObject GetObject(string objectName)
        {
            if (_poolDictionary.ContainsKey(objectName))
            {
                for (int i = 0; i < _poolDictionary[objectName].Count; i++)
                {
                    if (!_poolDictionary[objectName][i].activeInHierarchy)
                    {
                        return _poolDictionary[objectName][i];
                    }
                }
            }
            return null;
        }

        // public GameObject ShootWithUzi()
        // {
        //     for (int i = 0; i < _poolDictionary[objectna]; i++)
        //     {
        //         if (!uziAmmo[i].activeInHierarchy)
        //         {
        //             return uziAmmo[i];
        //         }
        //     }
        //     return null;
        // }
        
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
