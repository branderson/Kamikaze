using System.Collections.Generic;
using Assets.Enemy;
using Assets.Player;
using Assets.Utility;
using UnityEngine;

namespace Assets.Room
{
    public class RoomInteriorTrigger : CustomMonoBehaviour
    {
        [SerializeField] private float _exitVelocity;
        [SerializeField] private GameObject _room;

        private void OnTriggerStay(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player == null) return;

//            Debug.Log(player.SqrVelocity + " " + _exitVelocity * _exitVelocity);
            if (player.SqrVelocity < _exitVelocity*_exitVelocity) return;

            // Player can exit this level
            // Disable colliders
            foreach (Collider col in _room.GetComponentsInChildren<Collider>())
            {
                if (col.isTrigger) continue;
                col.enabled = false;
            }

            // Get rid of this trigger
            Destroy(gameObject);
        }
    }
}