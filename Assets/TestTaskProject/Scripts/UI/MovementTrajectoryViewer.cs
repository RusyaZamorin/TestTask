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
        public void Construct(MovementTrajectory movementTrajectory, PlayableCharacter playableCharacter)
        {
            this.character = playableCharacter;
            this.movementTrajectory = movementTrajectory;
        }

        public void Initialize()
        {
            movementTrajectory.PointWasAdded += MovementTrajectoryOnPointWasAdded;
            movementTrajectory.Cleared += ClearLineRenderer;
        }

        private void MovementTrajectoryOnPointWasAdded(Vector3 obj)
        {
            if (movementTrajectory.Points.Count == 1)
                startPosition = ConvertToLinePosition(character.transform.position);

            var points = new List<Vector3> { startPosition };
            points.AddRange(movementTrajectory.Points.Select(ConvertToLinePosition));

            LineRenderer.positionCount = points.Count;
            LineRenderer.SetPositions(points.ToArray());
        }

        private Vector3 ConvertToLinePosition(Vector3 point) =>
            new(point.x, point.y, LineZPosition);

        private void ClearLineRenderer() =>
            LineRenderer.positionCount = 0;
    }
}