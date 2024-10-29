using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace PickUps
{
    public class Item : MonoBehaviour, IItem
    {
        private const float AnimationDuration = 0.2f;

        private Transform _transform;
        private GameObject _gameObject;
        private Action _onTake;

        public Item Init(Action onTake)
        {
            _transform = transform;
            _gameObject = gameObject;
            _onTake = onTake;
            _gameObject.SetActive(false);
            return this;
        }

        public async UniTask Take(Vector3 target, Transform parent, Action<IItem> onComplete)
        {
            _gameObject.SetActive(true);
            _onTake.Invoke();
            await Move(target);
            _transform.SetParent(parent);
            onComplete.Invoke(this);
            await UniTask.CompletedTask;
        }

        public async UniTask Put(Vector3 target)
        {
            await Move(target);
            Destroy(gameObject);
        }

        public void SetPosition(Vector3 position)
        {
            _transform.localPosition = position;
            _transform.localEulerAngles = Vector3.zero;
        }

        private async UniTask Move(Vector3 target)
        {
            await _transform.DOMove(target, AnimationDuration).ToUniTask(cancellationToken: destroyCancellationToken);
        }
    }
}