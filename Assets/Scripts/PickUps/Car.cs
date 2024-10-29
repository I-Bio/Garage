using Cysharp.Threading.Tasks;
using Players;
using UnityEngine;

namespace PickUps
{
    public class Car : Interactable
    {
        [SerializeField] private Transform _putPoint;

        public override void StartInteraction(IPlayerStack stack)
        {
            Interact(stack).Forget();
        }

        private async UniTaskVoid Interact(IPlayerStack stack)
        {
            while (Cancellation.IsCancellationRequested == false)
            {
                if (stack.HaveItems() == false)
                    break;

                await stack.Put(_putPoint.position);
            }
        }
    }
}