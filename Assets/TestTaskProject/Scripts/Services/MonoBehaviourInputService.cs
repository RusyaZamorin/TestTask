using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestTaskProject.Services
{
    public class MonoBehaviourInputService : MonoBehaviour, IInputService, IPointerDownHandler, IPointerUpHandler
    {
        private Camera mainCamera;

        public event Action<Vector2> MouseDownedScreenCoord;
        public event Action<Vector2> MouseUppedScreenCoord;
        public event Action<Vector3> MouseDownedWorldCoord;
        public event Action<Vector3> MouseUppedWorldCoord;

        private void Awake() =>
            mainCamera = Camera.main;

        public void OnPointerDown(PointerEventData eventData)
        {
            var mousePosition = eventData.position;
            
            MouseDownedScreenCoord?.Invoke(mousePosition);
            if (mainCamera)
                MouseDownedWorldCoord?.Invoke(mainCamera.ScreenToWorldPoint(mousePosition));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            var mousePosition = eventData.position;
            
            MouseUppedScreenCoord?.Invoke(mousePosition);

            if (Camera.main)
                MouseUppedWorldCoord?.Invoke(Camera.main.ScreenToWorldPoint(mousePosition));
        }
    }
}