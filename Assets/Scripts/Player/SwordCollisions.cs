using Assets.Enemy;
using Assets.Utility;
using UnityEngine;

namespace Assets.Player
{
    public class SwordCollisions : CustomMonoBehaviour
    {
        [SerializeField] private SwordController _sword;
        private float _baseKnockback = .5f;
        private float _impactFactor = .0005f;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rigidbody.position = _sword.transform.position;
        }

        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag != "Enemy") return;

            if (_sword.SpinSpeed < 100) return;

            EnemyController enemy = col.gameObject.GetComponent<EnemyController>();
            _sword.EnemyKilled(enemy.Score);
            GameObject enemyDeath = enemy.Destroy();

            foreach (Rigidbody cube in enemyDeath.GetComponentsInChildren<Rigidbody>())
            {
                float swordSpeed = _sword.SpinSpeed;
                Vector3 force = col.impulse.normalized*(_baseKnockback + swordSpeed*_impactFactor);
                cube.AddForce(force , ForceMode.Impulse);;
            }
        }
    }
}