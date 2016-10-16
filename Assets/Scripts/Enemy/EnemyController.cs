using Assets.Player;
using Assets.Utility;
using UnityEngine;

namespace Assets.Enemy
{
    public class EnemyController : CustomMonoBehaviour
    {
        [SerializeField] private GameObject _deathPrefab;
        [SerializeField] private float _score = 100;
        [SerializeField] private float _speed = 5f;

        private Transform _player;
        private Rigidbody _rigidbody;

        public float Score
        {
            get { return _score; }
        }

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 dirToPlayer = Direction(_player.position)*_speed*Time.deltaTime;
            
            _rigidbody.MovePosition(new Vector3(
                _rigidbody.position.x + dirToPlayer.x,
                _rigidbody.position.y + dirToPlayer.y,
                _rigidbody.position.z + dirToPlayer.z));
        }

        public GameObject Destroy()
        {
            GameObject go = Instantiate(_deathPrefab, transform.position, Quaternion.identity) as GameObject;
            
            Destroy(gameObject);
            return go;
        }
    }
}