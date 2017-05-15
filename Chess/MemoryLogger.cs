using System;
using System.Collections.Generic;

namespace Chess
{

    [Serializable()]
    public class MemoryLogger
    {

        internal List<LogEntry> logs = new List<LogEntry>();

        public int GetCount () { return this.logs.Count; }

        public void Add(LogEntry log)
        {
            //log.Representation = log.ToString();
            //log.TimeTaken = MainWindow.appInstance.GetActivePlayer().timer.display.Content.ToString();
            this.logs.Add(log);
        }

        public LogEntry Get(int index)
        {
            return logs[index];
        }

        public List<LogEntry> GetAll()
        {
            return this.logs;
        }

    }
}
