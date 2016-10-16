using Assets.Utility;
using UnityEngine;

namespace Assets.Enemy
{
    public class EnemySpawner : CustomMonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _spawnRate = .5f;

        private float _spawnFrames = 0;

        private void Update()
        {
            _spawnFrames++;

            while (_spawnFrames > 60f/_spawnRate)
            {
                _spawnFrames -= 60f/_spawnRate;
                GameObject spawned = GameManager.Instance.InstantiateEnemy(_enemyPrefab);
            }
        }
    }
}