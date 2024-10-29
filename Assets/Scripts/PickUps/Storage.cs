using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Players;
using UnityEngine;

namespace PickUps
{
    public class Storage : Interactable
    {
        private const int MinValue = 0;

        [SerializeField] private Transform _spawnPoints;
        [SerializeField] private Item _template;
        [SerializeField] private List<GameObject> _thingTemplates;

        private Queue<IItem> _items;

        public void Init()
        {
            _items = new Queue<IItem>();

            for (int i = 0; i < _spawnPoints.childCount; i++)
            {
                Transform spawnPoint = _spawnPoints.GetChild(i);
                GameObject created = Instantiate(
                    _thingTemplates[Random.Range(MinValue, _thingTemplates.Count)],
                    spawnPoint);
                _items.Enqueue(Instantiate(_template, spawnPoint).Init(() => created.SetActive(false)));
            }
        }

        public override void StartInteraction(IPlayerStack stack)
        {
            Interact(stack).Forget();
        }

        private async UniTaskVoid Interact(IPlayerStack stack)
        {
            while (Cancellation.IsCancellationRequested == false)
            {
                if (_items.Count == MinValue)
                    break;

                await stack.Take(_items.Dequeue());
            }
        }
    }
}