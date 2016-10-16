using Assets.Enemy;
using Assets.Player;
using Assets.Utility;
using UnityEngine;

namespace Assets.Room
{
    public class RoomExteriorTrigger : CustomMonoBehaviour
    {
        [SerializeField] private float _levelRadius;
        [SerializeField] private GameObject _newRoom;
        [SerializeField] private GameObject _oldRoom;
        [SerializeField] private Light _newLight;
        [SerializeField] private Light _oldLight;

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<PlayerController>() == null) return;
            Debug.Log("In exterior trigger");

            // Enable new room
            _newRoom.SetActive(true);

            // Switch to new light
            _oldLight.gameObject.SetActive(false);
            _newLight.gameObject.SetActive(true);

            // Deactivate old spawners
            foreach (EnemySpawner spawner in _oldRoom.GetComponents<EnemySpawner>())
            {
                spawner.enabled = false;
            }

            // Set up new spawning positions
            GameManager.Instance.SetLevelBounds(_levelRadius);
            GameManager.Instance.DestroyEnemies();

            // Switch to new spawners
            foreach (EnemySpawner spawner in _newRoom.GetComponents<EnemySpawner>())
            {
                spawner.enabled = true;
            }

            // Get rid of this trigger
            Destroy(gameObject);
        }
    }
}