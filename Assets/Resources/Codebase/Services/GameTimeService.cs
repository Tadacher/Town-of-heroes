using UnityEngine;

namespace Services
{
    public class GameTimeService
    {
        public void StopTime() => 
            Time.timeScale = 0f;
        public void StartTime() =>
            Time.timeScale = 1f;
    }
}