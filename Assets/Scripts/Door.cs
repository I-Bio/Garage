using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 _rightTargetRotation;
    [SerializeField] private float _animationDuration;
    [SerializeField] private Transform _right;
    [SerializeField] private Transform _left;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        _right.DORotate(_rightTargetRotation, _animationDuration);
        _left.DORotate(-_rightTargetRotation, _animationDuration);
    }
}