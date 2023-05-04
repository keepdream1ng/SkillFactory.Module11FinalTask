using System.Collections.Concurrent;
using UtilityBot.Models;

namespace UtilityBot.Services
{
    public class MemoryStorage : IStorage
    {
        /// <summary>
        /// Session storage.
        /// </summary>
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            // Returning existing session. 
            if (_sessions.ContainsKey(chatId))
            {
                return _sessions[chatId];
            }

            // Creating new one otherwise.
            var newSession = new Session() { BotMode = "start" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}