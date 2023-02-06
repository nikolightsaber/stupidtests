using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private enum SafeDistanceRecoveryState
        {
            Init,
            Stopping,
            CatchupMargin,
            WaitQuality,

            WaitSafetyRoutine,
            // Recover Safety (SafeDistance < 0)
            StartSearchingGpsArea,
            SearchingGpsArea,
            // Recover Brain (OutOfGpsZone) (SafeDistance + GpsCrossMargin < 0)
            StartSearchingGpsZone,
            SearchingGpsZone,

            StartPrepareForEnd,
            WaitPrepareForEnd,
            Completed,
        }

        static void Main(string[] args)
        {
            SafeDistanceRecoveryState state = SafeDistanceRecoveryState.StartSearchingGpsZone;
            if (state < SafeDistanceRecoveryState.SearchingGpsArea)
                Console.WriteLine("Hello World!");
        }
    }
}
