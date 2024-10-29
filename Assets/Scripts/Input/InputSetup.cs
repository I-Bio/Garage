using Players;
using UnityEngine;

namespace Input
{
    public class InputSetup : MonoBehaviour
    {
        [SerializeField] private Mover _player;

        private InputSystem _model;
        private InputPresenter _presenter;

        public void Init()
        {
            _model = new InputSystem();
            _presenter = new InputPresenter(_model, _player);

            _presenter.Enable();
        }

        private void OnDestroy()
        {
            _presenter?.Disable();
        }
    }
}