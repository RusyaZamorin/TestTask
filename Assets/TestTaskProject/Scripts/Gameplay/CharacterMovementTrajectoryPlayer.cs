using System;
using Cysharp.Threading.Tasks;
using TestTaskProject.Services;
using UnityEngine;
using Zenject;

namespace TestTaskProject.Gameplay
{
    public class CharacterMovementTrajectoryPlayer : IInitializable
    {
        private CharacterMover characterMover;
        private IInputService inputService;
        private MovementTrajectory movementTrajectory;

        private bool movementIsPlaying;

        public event Action<Vector3> ArrivedOnPoint; 

        [Inject]
        public CharacterMovementTrajectoryPlayer(
            CharacterMover characterMover,
            IInputService inputService,
            MovementTrajectory movementTrajectory)
        {
            this.movementTrajectory = movementTrajectory;
            this.inputService = inputService;
            this.characterMover = characterMover;
        }

        public void Initialize() =>
            inputService.MouseDownedWorldCoord += AddTrajectoryPoint;

        private async UniTask PlayMovement()
        {
            movementIsPlaying = true;

            for (var index = 0; index < movementTrajectory.Points.Count; index++)
            {
                var point = movementTrajectory.Points[index];
                await characterMover.MoveTo(point);
                ArrivedOnPoint?.Invoke(point);
            }

            movementTrajectory.Clear();
            movementIsPlaying = false;
        }

        private void AddTrajectoryPoint(Vector3 point)
        {
            movementTrajectory.Add(point);

            if (!movementIsPlaying)
                PlayMovement();
        }
    }
}