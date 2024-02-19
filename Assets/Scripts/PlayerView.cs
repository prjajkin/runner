using Core.Platforms.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// View (monoBeh) layer for Player Entity.
    /// </summary>
    public class PlayerView : MonoBehaviour
    {
        private static readonly Vector3 LeftVector = Vector3.left * 7;
        private static readonly Vector3 RightVector = Vector3.right * 7;
        private static readonly Vector3 JumpHeight = Vector3.up * 120;

        private const float MaxHeight = 12;
        private const float MinHeight = -5;
        private const float VelocityThreshold = 0.05f;

        public float Speed = 15;
        [SerializeField] private Rigidbody _rb;
        
        public void SetGravity(bool val)=> _rb.useGravity = val;
        public PlayerController Controller { get; private set; }

        public event Action PlayerDied;

        private bool _stopped, _leftMoving, _rightMoving = false;
        private List<Effect> _effects = new List<Effect>();

        private void Awake()
        {
            SetGravity(false);
        }

        public void Init(PlayerController player)
        {
            Controller = player;
        }

        public void StartMove()
        {
            _stopped = false;
            SetGravity(true);
        }

        public void StartLeft() => _leftMoving = true;
        public void EndLeft() => _leftMoving = false;

        public void StartRight() => _rightMoving = true;
        public void EndRight() => _rightMoving = false;

        public void Move()
        {
            var v = _rb.velocity; 
            _rb.velocity = new Vector3(0, v.y, Speed);
            //_rb.AddForce(FrwVector * Time.fixedDeltaTime, ForceMode.VelocityChange );
            //transform.position += FrwVector * Time.deltaTime; 
        }

        private void Left() => transform.position += LeftVector * Time.deltaTime;
        private void Right() => transform.position += RightVector * Time.deltaTime;

        public void Jump()
        {
            if(IsJumpProcess())
                return; 
            _rb.AddForce(JumpHeight,  ForceMode.Impulse); 
        }

        private bool IsJumpProcess() => transform.position.y>1.3 ||  Mathf.Abs(_rb.velocity.y) > VelocityThreshold;


        private void Update()
        {
            if(_stopped) return;

            CheckYBorders();

            if (_leftMoving)
            {
                Left();
            }

            if (_rightMoving)
            {
                Right();
            }

            for (var i = _effects.Count - 1; i >= 0; i--)
            {
                var effect = _effects[i];
                effect.Time -= Time.deltaTime;
                if (effect.Time < 0)
                {
                    _effects.Remove(effect);
                    effect.OnEnd?.Invoke(this);
                }
            }
        }

        private void CheckYBorders()
        {
            if (transform.position.y < MinHeight)
            {
                Stop();
                PlayerDied?.Invoke();
            }
            else if (transform.position.y > MaxHeight)
            {
                _rb.velocity = Vector3.zero;
                transform.position += Vector3.down * 0.05f;
            }
        }

        public void AddEffect(Effect effect)
        {
            _effects.Add(effect);
            effect.OnStart?.Invoke(this);
        }

        private void FixedUpdate()
        {
            if(_stopped) return;

            Move();
        }

        public void Stop()
        {
            _stopped = true;
            SetGravity(false);
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }
}
