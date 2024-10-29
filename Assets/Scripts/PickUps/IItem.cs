using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PickUps
{
    public interface IItem
    {
        public UniTask Take(Vector3 target, Transform parent, Action<IItem> onComplete);

        public UniTask Put(Vector3 target);

        public void SetPosition(Vector3 position);
    }
}