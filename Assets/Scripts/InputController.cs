using System;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Handlers for user input.
    /// </summary>
    public class InputController: IDisposable
    {
        private readonly UserInput _userInput;
        private readonly PlayerController _player;
        private bool _disposedValue, _touchStarted;
        private float _lastTouchTime;

        public InputController(UserInput userInput, PlayerController playerController)
        {
            _player = playerController;
            _userInput = userInput;
            _userInput.Player.Left.started += Left_started;
            _userInput.Player.Left.canceled += Left_canceled;
            _userInput.Player.Right.started += Right_started;
            _userInput.Player.Right.canceled += Right_canceled;
            _userInput.Player.Jump.canceled += Jump_canceled;

            _userInput.Player.FirstTouch.started += FirstTouch_started;
            _userInput.Player.FirstTouch.canceled += FirstTouch_canceled;
            _userInput.Player.TouchPos.performed += TouchPos_performed; ; 
        }

        private void TouchPos_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if(_touchStarted==false)
                return;

            var pos = obj.ReadValue<Vector2>() ;
            if (pos.x > Screen.width / 2f)
            {
                _player.StartRight();
                _player.EndLeft();
            }
            else
            {
                _player.StartLeft();
                _player.EndRight();
            }
            
        }

        private void FirstTouch_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _touchStarted = false;
            _player.EndRight();
            _player.EndLeft();

        }

        private void FirstTouch_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (Time.time - _lastTouchTime < 0.2f)
            {
                _player.Jump();
            }
            _lastTouchTime = Time.time;

            _touchStarted = true;
        }

        private void Left_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _player.EndLeft();
        }

        private void Left_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _player.StartLeft();
        }

        private void Right_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _player.EndRight();
        }

        private void Right_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _player.StartRight();
        }

        private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _player.Jump();
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                _userInput.Player.Left.started -= Left_started;
                _userInput.Player.Left.canceled -= Left_canceled;
                _userInput.Player.Right.started -= Left_started;
                _userInput.Player.Right.canceled -= Left_canceled;
                _userInput.Player.Jump.canceled -= Left_canceled;

                _disposedValue = true;
            }
        }

        ~InputController()
        { 
            Dispose(disposing: false);
        }

        public void Dispose()
        { 
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    
        #endregion
    }
}