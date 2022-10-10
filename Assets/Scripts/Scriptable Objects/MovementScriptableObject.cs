using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeIdle.Movement
{
    [CreateAssetMenu(menuName = "Movement/MovementSetting",fileName ="MovementSettings")]
    public class MovementScriptableObject : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;

        public float MoveSpeed { get { return _moveSpeed; } }


    }
}