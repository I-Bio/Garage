using System.Threading;
using Cysharp.Threading.Tasks;
using Input;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover : MonoBehaviour, IReader
    {
        private Rigidbody _rigidbody;
        private Transform _rotationPoint;
        private Vector2 _input;
        private Vector3 _forward;
        private float _speed;
        private CancellationToken _token;

        public void Init(Transform rotationPoint, Vector3 forward, float speed)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _forward = forward;
            _rotationPoint = rotationPoint;
            _speed = speed;
            _token = destroyCancellationToken;

            StartMove().Forget();
        }

        public void ReadInput(Vector2 input)
        {
            _input = input;
        }

        private async UniTaskVoid StartMove()
        {
            while (_token.IsCancellationRequested == false)
            {
                Move();
                RotateAlongMove();
                await UniTask.NextFrame(PlayerLoopTiming.FixedUpdate, _token);
            }
        }

        private void Move()
        {
            if (_rigidbody.velocity == Vector3.zero && _input == Vector2.zero)
                return;

            _rigidbody.velocity = new Vector3(_input.x, 0f, _input.y) * _speed;
        }

        private void RotateAlongMove()
        {
            if (_input == Vector2.zero)
                return;

            Vector3 direction = new Vector3(_input.x, 0, _input.y);
            _rotationPoint.eulerAngles = new Vector3(0, Vector3.SignedAngle(_forward, direction, Vector3.up), 0);
        }
    }
}