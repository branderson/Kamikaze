using System;
using Assets.Utility;
using Assets.Utility.Static;
using UnityEditor;
using UnityEngine;

namespace Assets.Player
{
    public class PlayerController : CustomMonoBehaviour
    {
        private enum MoveState
        {
            Walking,
            Flying,
        }

        private const float FlightSpinSpeed = 2000;

        [SerializeField] private CameraController _camera;
        [SerializeField] private SwordController _sword;
        [SerializeField] private float _baseMoveSpeed = 5f;
        [SerializeField] private float _baseThrust = .0001f;
        [SerializeField] private float _cameraBackoffDamping = 250f;
        private float _maxVelocity = 10f;

        private MoveState _moveState = MoveState.Walking;
        private float _spinSpeed = 0;
        private float _sqrVelocity = 0;

        private Rigidbody _rigidbody;

        public float SqrVelocity
        {
            get { return _sqrVelocity; }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _sword.Player = this;
        }

        private void Update()
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            Vector2 input = new Vector2(hor, ver);

            switch (_moveState)
            {
                case MoveState.Walking:
                    MoveWalking(input);
                    break;
                case MoveState.Flying:
                    MoveFlying(input);
                    break;
            }

            if (Input.GetButton("Spin"))
            {
                _sword.Spin();
            }
            _spinSpeed = _sword.SpinSpeed;
            _camera.FollowDistance = 5 + _spinSpeed/_cameraBackoffDamping;
        }

        private void MoveWalking(Vector2 input)
        {
            _rigidbody.useGravity = true;
            Vector3 speed = new Vector3(input.x*_baseMoveSpeed, 0, input.y*_baseMoveSpeed);
            if (speed.sqrMagnitude > _maxVelocity*_maxVelocity)
            {
                // Cap speed to max velocity
                speed = speed.normalized*_maxVelocity;
            }
            _sqrVelocity = speed.sqrMagnitude;
            speed = transform.TransformDirection(speed) * Time.deltaTime;
            
            _rigidbody.MovePosition(new Vector3(
                _rigidbody.position.x + speed.x, 
                _rigidbody.position.y, 
                _rigidbody.position.z + speed.z));

            if (_spinSpeed >= FlightSpinSpeed)
            {
                _moveState = MoveState.Flying;
            }
        }

        private void MoveFlying(Vector2 input)
        {
            _rigidbody.useGravity = false;
            Vector3 speed = new Vector3(input.x*_baseMoveSpeed, 0, input.y*_baseMoveSpeed);
            if (speed.sqrMagnitude > _maxVelocity*_maxVelocity)
            {
                // Cap speed to max velocity
                speed = speed.normalized*_maxVelocity;
            }
            speed.Set(speed.x, _spinSpeed * _baseThrust, speed.z); 
            _sqrVelocity = speed.sqrMagnitude;
            speed = transform.TransformDirection(speed) * Time.deltaTime;

            _rigidbody.MovePosition(new Vector3(
                _rigidbody.position.x + speed.x, 
                _rigidbody.position.y + speed.y, 
                _rigidbody.position.z + speed.z));

            if (_spinSpeed < FlightSpinSpeed)
            {
                _moveState = MoveState.Walking;
            }
        }

        public void LookTowards(Quaternion rotation)
        {
            switch (_moveState)
            {
                case MoveState.Walking:
                    transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
                    break;
                case MoveState.Flying:
                    transform.rotation = Quaternion.Euler(rotation.eulerAngles.x + 45, rotation.eulerAngles.y, 0);
                    break;
            }
        }

        public void EnemyKilled(float score)
        {
        }
    }
}