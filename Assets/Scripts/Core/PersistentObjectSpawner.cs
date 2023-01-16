using System;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [Tooltip("这个预制件只会被生成一次，并且会在不同的场景之间保留")]
        [SerializeField] GameObject persistentObjectPrefab = null;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;
            SpawnPersistentObjects();

            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }

}