using System.Collections.Generic;
using Assets.Utility;
using UnityEngine;

namespace Assets
{
    public class GameManager : Singleton<GameManager>
    {
        protected GameManager() { }

        private const float EdgeOffset = 10f;

        private List<GameObject> _enemies = new List<GameObject>();
        private float _levelBounds = 0;
        private float _innerBounds = 0;

        public void Start()
        {
            SetLevelBounds(160);
        }

        public void SetLevelBounds(float radius)
        {
            _innerBounds = _levelBounds;
            _levelBounds = radius;
        }

        public GameObject InstantiateEnemy(GameObject enemy)
        {
            // Select a random position in quadrant 1
            Vector3 quadrantPosition = new Vector3(
                Random.Range(EdgeOffset, _levelBounds - _innerBounds - EdgeOffset), 
                Random.Range(EdgeOffset, _levelBounds - _innerBounds - EdgeOffset),
                Random.Range(EdgeOffset, _levelBounds - _innerBounds - EdgeOffset));
            float invertX = Random.value > .5f ? 1 : -1;
            float invertY = Random.value > .5f ? 1 : -1;
            float invertZ = Random.value > .5f ? 1 : -1;
            Vector3 spawnPosition = new Vector3(
                quadrantPosition.x * invertX,
                quadrantPosition.y * invertY,
                quadrantPosition.z * invertZ);
            GameObject spawned = Instantiate(enemy, spawnPosition, Quaternion.identity) as GameObject;
            _enemies.Add(spawned);
            return spawned;
        }

        public void DestroyEnemies()
        {
            foreach (GameObject enemy in _enemies)
            {
                Destroy(enemy);
            }
        }
    }
}