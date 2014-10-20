using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
namespace iTrak.Importer.Common
{
    public class ThreadHelper
    {
        public const int NUMBER_OF_THREADS = 20;
        public const int NUMBER_OF_THREADS_MEDIA = 20;
        public static void WaitAll(WaitHandle[] waitHandlers)
        {
            foreach (WaitHandle eventHandler in waitHandlers)
            {
                if (eventHandler != null)

                    eventHandler.WaitOne();
            }
        }
    }
}
