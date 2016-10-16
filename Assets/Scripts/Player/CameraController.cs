using Assets.Utility;
using UnityEngine;

namespace Assets.Player
{
    public class CameraController : CustomMonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _followDistance = 10f;

        public float _xSpeed = 120.0f;
        public float _ySpeed = 120.0f;
     
        public float _yMinLimit = -20f;
        public float _yMaxLimit = 80f;
     
        public float _distanceMin = .5f;
        public float _distanceMax = 15f;

        private PlayerController _playerController;
        private Rigidbody _rigidbody;
     
        float _x = 0.0f;
        float _y = 0.0f;

        /// <summary>
        /// The distance that the camera should be from the player
        /// </summary>
        public float FollowDistance
        {
            set { _followDistance = value; }
        }

        private void Awake()
        {
            _playerController = _player.GetComponent<PlayerController>();
        }
 
        private void Start () 
        {
            Vector3 angles = transform.eulerAngles;
            _x = angles.y;
            _y = angles.x;
     
            _rigidbody = GetComponent<Rigidbody>();
     
            // Make the rigid body not change rotation
            if (_rigidbody != null)
            {
                _rigidbody.freezeRotation = true;
            }
        }

        private void LateUpdate()
        {
            if (_player) 
            {
                _x += Input.GetAxis("Mouse X") * _xSpeed * _followDistance * 0.02f;
                _y -= Input.GetAxis("Mouse Y") * _ySpeed * 0.02f;
     
                _y = ClampAngle(_y, _yMinLimit, _yMaxLimit);
     
                Quaternion rotation = Quaternion.Euler(_y, _x, 0);
     
                RaycastHit hit;
//                if (Physics.Linecast (_player.position, transform.position, out hit)) 
//                {
//                    _followDistance -=  hit.distance;
//                }
                Vector3 negDistance = new Vector3(0.0f, 0.0f, -_followDistance);
                Vector3 position = rotation * negDistance + _player.position;
     
                transform.rotation = rotation;
                transform.position = position;
                _playerController.LookTowards(rotation);
            }
        }
     
        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}