using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace LLib
{
    public class PointerManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference _clickReference;
        [SerializeField] private InputActionReference _positionReference;

        private Vector2 _prevPosition;
        
        public Vector2 Position { get; private set; }
        public Vector2 Delta { get; private set; }
        public bool IsDown { get; private set; }
        public bool IsHold { get; private set; }
        public bool IsUp { get; private set; }


        private void Awake()
        {
            _positionReference?.action.Enable();
            _clickReference?.action.Enable();

            InputSystem.onAfterUpdate += OnInputUpdate;
        }

        private void OnDestroy()
        {
            InputSystem.onAfterUpdate -= OnInputUpdate;
        }
        
        public bool IsOverUI()
        {
            if (EventSystem.current == null)
                return false;

            return EventSystem.current.IsPointerOverGameObject();
        }

        public List<RaycastResult> HitUI()
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Position,
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            return results;
        }
        
        public RaycastHit2D Hit2D(Camera cam, LayerMask mask = default)
        {
            Vector2 world = cam.ScreenToWorldPoint(Position);

            return Physics2D.Raycast(world, Vector2.zero, Mathf.Infinity, mask);
        }
        
        public RaycastHit Hit3D(Camera cam, LayerMask mask = default)
        {
            var ray =  cam.ScreenPointToRay(Position);
            
            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask);
            return hit;
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