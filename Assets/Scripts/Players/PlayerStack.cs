using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PickUps;
using UnityEngine;

namespace Players
{
    public class PlayerStack : MonoBehaviour, IPlayerStack
    {
        private const int MinValue = 0;

        private Transform _stack;
        private Vector3 _offset;
        private Vector3 _currentOffset;
        private Queue<IItem> _items;
        
        public void Init(Transform stack, Vector3 offset)
        {
            _stack = stack;
            _offset = offset;
            _currentOffset = Vector3.zero;
            _items = new Queue<IItem>();
        }

        public bool HaveItems()
        {
            return _items.Count > MinValue;
        }

        public async UniTask Take(IItem item)
        {
            await item.Take(_stack.position, _stack, OnCompleteTake);
            _currentOffset += _offset;
        }

        public async UniTask Put(Vector3 target)
        {
            await _items.Dequeue().Put(target);
            _currentOffset -= _offset;
        }

        private void OnCompleteTake(IItem item)
        {
            item.SetPosition(_currentOffset);
            _items.Enqueue(item);
        }
    }
}