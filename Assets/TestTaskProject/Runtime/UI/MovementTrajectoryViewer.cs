using TestTaskProject.Runtime.Gameplay;
using UnityEngine;
using Zenject;

namespace TestTaskProject.Runtime.UI
{
    public class MovementTrajectoryViewer : MonoBehaviour, IInitializable
    {
        private MovementTrajectory movementTrajectory;

        [Inject]
        public void Construct(MovementTrajectory movementTrajectory)
        {
            this.movementTrajectory = movementTrajectory;
        }
        
        public void Initialize()
        {
            movementTrajectory.PointWasAdded += MovementTrajectoryOnPointWasAdded;    
        }

        private void MovementTrajectoryOnPointWasAdded(Vector3 obj)
        {
            
        }
    }
}