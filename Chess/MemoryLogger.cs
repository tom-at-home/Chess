using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class MemoryLogger
    {

        private List<LogEntry> logs = new List<LogEntry>();

        public int GetCount () { return this.logs.Count; }

        public void Add(LogEntry log)
        {
            this.logs.Add(log);
        }

        public LogEntry Get(int index)
        {
            return logs[index];
        }

    }
}
