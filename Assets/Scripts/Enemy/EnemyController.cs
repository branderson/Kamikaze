using Assets.Player;
using Assets.Utility;
using UnityEngine;

namespace Assets.Enemy
{
    public class EnemyController : CustomMonoBehaviour
    {
        [SerializeField] private float _score = 100;

        private Transform _player;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 dirToPlayer = Direction(_player.position)*Time.deltaTime;
            
            _rigidbody.MovePosition(new Vector3(
                _rigidbody.position.x + dirToPlayer.x,
                _rigidbody.position.y + dirToPlayer.y,
                _rigidbody.position.z + dirToPlayer.z));
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Sword")
            {
                SwordController sword = other.gameObject.GetComponent<SwordController>();
                sword.EnemyKilled(_score);
            }
        }
    }
}