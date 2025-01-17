using System;

namespace Lessons.Gameplay.Mech
{
    public interface IWorkComponent
    {
        event Action OnStartWork;

        event Action<float> OnProgress; 

        event Action OnFinishWork;
    
        bool IsWorking { get; }
        
        float Progress { get; }
    }
}