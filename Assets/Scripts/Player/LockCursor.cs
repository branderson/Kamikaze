using UnityEngine;

namespace Assets.Player
{
    public class LockCursor : MonoBehaviour
    {
        private bool _lockCursor = true;

        private void Update()
        {
            if (_lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
            }

            if (Input.GetButtonDown("Cancel"))
            {
                _lockCursor = !_lockCursor;
            }
        }
    }
}