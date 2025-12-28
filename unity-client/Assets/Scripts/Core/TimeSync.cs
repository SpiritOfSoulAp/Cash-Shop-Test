using System;
using UnityEngine;

namespace Core
{
    public class TimeSync
    {
        private TimeSpan _offset = TimeSpan.Zero;

        // Call this every time server_time is received
        public void UpdateServerTime(string serverTimeUtc)
        {
            if (string.IsNullOrEmpty(serverTimeUtc))
            {
                Debug.LogError("[TimeSync] serverTimeUtc is null or empty");
                return;
            }

            if (!DateTime.TryParse(serverTimeUtc, out var serverUtc))
            {
                Debug.LogError($"[TimeSync] Invalid server time format: {serverTimeUtc}");
                return;
            }

            _offset = serverUtc.ToUniversalTime() - DateTime.UtcNow;
        }

        // Always use this instead of DateTime.Now / UtcNow
        public DateTime NowServerUtc()
        {
            return DateTime.UtcNow + _offset;
        }
    }
}


