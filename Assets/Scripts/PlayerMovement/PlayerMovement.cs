using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeIdle.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private FloatingJoystick _fixedJoystick;
        [SerializeField] private MovementScriptableObject _movementSettings;
        [SerializeField] private PlayerAnimation _playerAnimation;
        #endregion

        #region Private Fields
        private Rigidbody _rb;
        private Vector3 _direction;
        private bool _isRun;
        #endregion

        #region MonoBehaviour Methods
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            Movement();
            CheckRun();
        }
        #endregion


        #region Movement and Rotation Methods
        private void Movement()
        {
            _direction = new Vector3(_fixedJoystick.Horizontal, 0, _fixedJoystick.Vertical);
            if (!_isRun)
                return;
            _rb.velocity = _direction * Time.fixedDeltaTime * 100 * _movementSettings.MoveSpeed;
            RotatePlayer();
        }

        private void CheckRun()
        {
            _isRun = _direction.magnitude > 0;
            _playerAnimation.AnimateRun(_isRun);
        }

        private void RotatePlayer()
        {
            if (_direction != Vector3.zero)
            {
                Quaternion directRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, directRotation,
                10 * Time.deltaTime);
            }

        }
        #endregion
    }
}