using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grimoire.Interfaces;

namespace Grimoire.Processors
{
    public class Schedule
    {
        private int _time;
        private readonly SortedDictionary<int, List<IAmScheduleable>> _scheduleables;

        public Schedule()
        {
            _time = 0;
            _scheduleables = new SortedDictionary<int, List<IAmScheduleable>>();
        }

        public void Add(IAmScheduleable scheduleable)
        {
            int key = _time + scheduleable.Time;
            if (!_scheduleables.ContainsKey(key))
            {
                _scheduleables.Add(key, new List<IAmScheduleable>());
            }

            _scheduleables[key].Add(scheduleable);
        }
        /// <summary>
        /// remove specific object from schedule
        /// when an enemy is killed to remove it before it's action running again
        /// </summary>
        /// <param name="scheduleable"></param>
        public void Remove(IAmScheduleable scheduleable)
        {
            KeyValuePair<int, List<IAmScheduleable>> scheduleablesListFound = new KeyValuePair<int, List<IAmScheduleable>>(-1,null);
            foreach (var schedulablesList in _scheduleables)
            {
                if (schedulablesList.Value.Contains(scheduleable))
                {
                    scheduleablesListFound = schedulablesList;
                    break;
                }
            }
            if (scheduleablesListFound.Value != null)
            {
                scheduleablesListFound.Value.Remove(scheduleable);
                if (scheduleablesListFound.Value.Count <= 0)
                {
                    _scheduleables.Remove(scheduleablesListFound.Key);
                }
            }
        }

        public IAmScheduleable Get()
        {
            var firstScheduleableGroup = _scheduleables.First();
            var firstSchedulable = firstScheduleableGroup.Value.First();
            Remove(firstSchedulable);
            _time = firstScheduleableGroup.Key;
            return firstSchedulable;
        }

        public int GetTime()
        {
            return _time;
        }

        public void Clear()
        {
            _time = 0;
            _scheduleables.Clear();
            
        }
    }
}
