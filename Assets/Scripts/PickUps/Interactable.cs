using System.Threading;
using Players;
using UnityEngine;

namespace PickUps
{
    public abstract class Interactable : MonoBehaviour
    {
        public CancellationTokenSource Cancellation { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPlayerStack stack) == false)
                return;

            Cancellation = new CancellationTokenSource();
            StartInteraction(stack);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IPlayerStack _) == false)
                return;

            ResetInteraction();
        }

        private void OnDestroy()
        {
            ResetInteraction();
        }

        private void ResetInteraction()
        {
            if (Cancellation == null)
                return;

            if (Cancellation.IsCancellationRequested == false)
                Cancellation.Cancel();

            Cancellation.Dispose();
        }

        public abstract void StartInteraction(IPlayerStack stack);
    }
}