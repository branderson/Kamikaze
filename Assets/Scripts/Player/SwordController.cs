using Assets.Utility;
using UnityEngine;

namespace Assets.Player
{
    public class SwordController : CustomMonoBehaviour
    {
        [SerializeField] private GameObject _lowVelocityCollisions;
        [SerializeField] private GameObject _highVelocityCollisions;
        [SerializeField] private float _baseSpinSpeed = 50f;
        [SerializeField] private float _spinDecay = 1f;
        [SerializeField] private float _highVelocity = 1000f;
        private float _maxSpinSpeed = 200f;

        [SerializeField] private float _spinSpeed = 0f;
        private Vector3 _initialPosition;

        public PlayerController Player;
        private Collider _playerCollider;
        private Collider _highVelocityCollider;
        private Collider _lowVelocityCollider;
        private Rigidbody _rigidbody;

        public float SpinSpeed
        {
            get { return _spinSpeed; }
        }

        public float MaxSpinSpeed
        {
            get { return _maxSpinSpeed; }
            set { _maxSpinSpeed = value; }
        }

        private void Awake()
        {
//            _rigidbody = GetComponent<Rigidbody>();
//            _rigidbody.centerOfMass = new Vector3(-2, 0, 0);
            _playerCollider = Player.GetComponent<Collider>();
            _highVelocityCollider = _highVelocityCollisions.GetComponent<Collider>();
            _lowVelocityCollider = _lowVelocityCollisions.GetComponent<Collider>();
        }

        private void Start()
        {
//            _initialPosition = _rigidbody.position - Player.transform.position;
        }

        private void LateUpdate()
        {
            if (_spinSpeed >= _highVelocity)
            {
                _highVelocityCollisions.gameObject.SetActive(true);
                _lowVelocityCollisions.gameObject.SetActive(false);
                Physics.IgnoreCollision(_highVelocityCollider, _playerCollider, true);
            }
            else
            {
                _highVelocityCollisions.gameObject.SetActive(false);
                _lowVelocityCollisions.gameObject.SetActive(true);
                Physics.IgnoreCollision(_lowVelocityCollider, _playerCollider, true);
            }

            transform.Rotate(Vector3.up, _spinSpeed*Time.deltaTime, Space.Self);
//            Quaternion dr = Quaternion.AngleAxis(_spinSpeed * Time.deltaTime, Vector3.up);
//            _rigidbody.MovePosition(Player.transform.position + _initialPosition);
//            _rigidbody.MoveRotation(_rigidbody.rotation * dr);

            _spinSpeed -= _spinDecay*Time.deltaTime;
            if (_spinSpeed < 0) _spinSpeed = 0;
        }

        public float Spin()
        {
            _spinSpeed += _baseSpinSpeed;
            if (_spinSpeed > _maxSpinSpeed) _spinSpeed = _maxSpinSpeed;
            return _spinSpeed;
        }

        public void EnemyKilled(float score)
        {
            _maxSpinSpeed += score;
            Player.EnemyKilled(score);
        }

        public Collider[] GetColliders()
        {
            return new [] {_highVelocityCollisions.GetComponent<Collider>(), _lowVelocityCollisions.GetComponent<Collider>()};
        }
    }
}