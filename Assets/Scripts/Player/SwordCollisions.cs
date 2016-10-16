using Assets.Utility;
using UnityEngine;

namespace Assets.Player
{
    public class SwordCollisions : CustomMonoBehaviour
    {
        [SerializeField] private SwordController _sword;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                
            }
        }
    }
}