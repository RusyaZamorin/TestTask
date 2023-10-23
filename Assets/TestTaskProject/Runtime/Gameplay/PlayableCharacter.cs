using System;
using UnityEngine;

namespace TestTaskProject.Runtime.Gameplay
{
    public class PlayableCharacter : MonoBehaviour
    {
        private float speed;

        public event Action OnSpeedChanged;

        public float Speed
        {
            get => speed;
            private set
            {
                speed = value;
                OnSpeedChanged?.Invoke();
            }
        }

        public void ChangeSpeed(float newSpeed)
        {
            Speed = newSpeed;
        }
    }
}