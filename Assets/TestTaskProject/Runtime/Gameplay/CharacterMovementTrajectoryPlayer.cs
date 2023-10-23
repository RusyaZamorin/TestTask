using Cysharp.Threading.Tasks;
using TestTaskProject.Runtime.Services;
using UnityEngine;
using Zenject;

namespace TestTaskProject.Runtime.Gameplay
{
    public class CharacterMovementTrajectoryPlayer : IInitializable
    {
        private CharacterMover characterMover;
        private IInputService inputService;
        private MovementTrajectory movementTrajectory;

        private bool movementIsPlaying;

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