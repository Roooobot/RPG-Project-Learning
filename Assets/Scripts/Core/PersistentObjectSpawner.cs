using System;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [Tooltip("���Ԥ�Ƽ�ֻ�ᱻ����һ�Σ����һ��ڲ�ͬ�ĳ���֮�䱣��")]
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