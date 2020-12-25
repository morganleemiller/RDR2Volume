using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RDR2Volume
{
    public class Program
    {
        private static string _processName = "RDR2";
        private static float _appVolume = 100f;

        public static void Main(string[] args)
        {
            
            do
            {
                try
                {
                    var pID = 0;

                    foreach (var process in Process.GetProcesses())
                    {
                        if (process.ProcessName.Equals(_processName, StringComparison.OrdinalIgnoreCase) && !String.IsNullOrEmpty(process.MainWindowTitle))
                        {
                            pID = process.Id;
                            Console.WriteLine(string.Format("Found Process: {0} pID is: {1}", _processName, pID));

                            break;
                        }
                    }

                    
                    if (pID != 0)
                    {
                        var currentAppVolume = VolumeMixer.GetApplicationVolume(pID);
                        if (currentAppVolume < 100f)
                        {
                            Console.WriteLine(string.Format("{0} application volume: {1}, setting to {2}", _processName, currentAppVolume, _appVolume));
                            VolumeMixer.SetApplicationVolume(pID, _appVolume);
                        }
                    }
                    else
                    {
                        Console.WriteLine(string.Format("Did not find process: {0} will keep trying", _processName, pID));
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                Thread.Sleep(30000);

            } while (true);
        }
    }
}
