using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestTaskProject.Gameplay
{
    public class MovementTrajectory
    {
        private List<Vector3> points = new();
        
        public event Action<Vector3> PointWasAdded;
        public event Action Cleared;
        
        public List<Vector3> Points => points;
        
        public void Add(Vector3 point)
        {
            points.Add(point);
            PointWasAdded?.Invoke(point);
        }

        public void Clear()
        {
            points.Clear();
            Cleared?.Invoke();
        }
    }
}