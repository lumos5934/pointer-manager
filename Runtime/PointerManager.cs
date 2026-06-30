using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace LLib
{
    public class PointerManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference _clickReference;
        [SerializeField] private InputActionReference _positionReference;

        private Vector2 _prevPosition;
        
        public static PointerManager Instance { get; private set; }

        public Vector2 Position { get; private set; }
        public Vector2 Delta { get; private set; }
        public bool IsDown { get; private set; }
        public bool IsHold { get; private set; }
        public bool IsUp { get; private set; }
        public float HoldTime { get; private set; }


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _positionReference?.action.Enable();
            _clickReference?.action.Enable();

            InputSystem.onAfterUpdate += OnInputUpdate;
        }

        private void OnDestroy()
        {
            InputSystem.onAfterUpdate -= OnInputUpdate;
        }

        private void Update()
        {
            if (IsHold)
            {
                HoldTime += Time.deltaTime;
                return;
            }
            
            HoldTime = 0f;
        }
        
        public bool IsOverEventSystem()
        {
            if (EventSystem.current == null)
                return false;

            return EventSystem.current.IsPointerOverGameObject();
        }

        private void OnInputUpdate()
        {
            if (_positionReference != null)
            {
                Position = _positionReference.action.ReadValue<Vector2>();
                Delta = Position - _prevPosition;
                _prevPosition = Position;
            }

            if (_clickReference != null)
            {
                IsDown = _clickReference.action.WasPressedThisFrame();
                IsHold = _clickReference.action.IsPressed();
                IsUp = _clickReference.action.WasReleasedThisFrame();
            }
        }
    }
}