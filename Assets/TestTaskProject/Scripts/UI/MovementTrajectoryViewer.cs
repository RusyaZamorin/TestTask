using System.Collections.Generic;
using System.Linq;
using TestTaskProject.Gameplay;
using UnityEngine;
using Zenject;

namespace TestTaskProject.UI
{
    public class MovementTrajectoryViewer : MonoBehaviour, IInitializable
    {
        public LineRenderer LineRenderer;

        public float LineZPosition = -1;

        private MovementTrajectory movementTrajectory;
        private PlayableCharacter character;
        private Vector3 startPosition;

        [Inject]
        public void Construct(
            MovementTrajectory movementTrajectory,
            PlayableCharacter playableCharacter)
        {
            character = playableCharacter;
            this.movementTrajectory = movementTrajectory;
        }

        public void Initialize()
        {
            movementTrajectory.PointWasAdded += _ => UpdateLine();
            movementTrajectory.Cleared += ClearLineRenderer;
        }

        private void UpdateLine()
        {
            if (movementTrajectory.Points.Count == 1)
                startPosition = ConvertToLinePosition(character.transform.position);

            var positions = new List<Vector3> { startPosition };

            var notCompletedPositions = movementTrajectory.Points
                .Select(ConvertToLinePosition)
                .ToList();

            positions.AddRange(notCompletedPositions);

            LineRenderer.positionCount = positions.Count;
            LineRenderer.SetPositions(positions.ToArray());
        }

        private Vector3 ConvertToLinePosition(Vector3 point) =>
            new(point.x, point.y, LineZPosition);

        private void ClearLineRenderer() =>
            LineRenderer.positionCount = 0;
    }
}