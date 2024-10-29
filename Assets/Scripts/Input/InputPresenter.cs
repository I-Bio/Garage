using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputPresenter
    {
        private readonly InputSystem _model;
        private readonly IReader _player;

        public InputPresenter(InputSystem model, IReader player)
        {
            _model = model;
            _player = player;
        }

        public void Enable()
        {
            _model.Player.Move.performed += OnMoved;
            _model.Player.Move.canceled += OnMoveEnded;

            _model.Enable();
        }

        public void Disable()
        {
            _model.Player.Move.performed -= OnMoved;
            _model.Player.Move.canceled -= OnMoveEnded;

            _model.Disable();
        }

        private void OnMoved(InputAction.CallbackContext context)
        {
            _player.ReadInput(context.ReadValue<Vector2>());
        }

        private void OnMoveEnded(InputAction.CallbackContext context)
        {
            _player.ReadInput(Vector2.zero);
        }
    }
}