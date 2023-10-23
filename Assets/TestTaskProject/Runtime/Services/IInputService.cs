using System;
using UnityEngine;

namespace TestTaskProject.Runtime.Services
{
    public interface IInputService
    {
        event Action<Vector2> MouseDownedScreenCoord;
        
        event Action<Vector2> MouseUppedScreenCoord;
        
        event Action<Vector3> MouseDownedWorldCoord;
        
        event Action<Vector3> MouseUppedWorldCoord;
    }
}