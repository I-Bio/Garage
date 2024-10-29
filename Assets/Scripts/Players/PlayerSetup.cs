using UnityEngine;

namespace Players
{
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private PlayerStack _collision;
        [SerializeField] private Mover _mover;
        [SerializeField] private Transform _rotationPoint;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _stackPoint;
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _stackOffset;

        public void Init()
        {
            _collision.Init(_stackPoint, _stackOffset);
            _mover.Init(_rotationPoint, _transform.forward, _speed);
        }
    }
}