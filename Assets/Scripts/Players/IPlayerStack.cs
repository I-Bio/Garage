using Cysharp.Threading.Tasks;
using PickUps;
using UnityEngine;

namespace Players
{
    public interface IPlayerStack
    {
        public bool HaveItems();

        public UniTask Take(IItem item);

        public UniTask Put(Vector3 target);
    }
}