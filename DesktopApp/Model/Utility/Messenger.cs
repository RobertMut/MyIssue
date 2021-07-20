using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.DesktopApp.Model.Utility
{
    /// Part of code used from Pluralsight's course Practical MVVM
    public class Messenger
    {
        private static readonly object locker = new object();
        private static Messenger _messenger;
        private static readonly ConcurrentDictionary<MessengerKey, object> Dictionary = new ConcurrentDictionary<MessengerKey, object>();
        public static Messenger Default
        {
            get
            {
                if (_messenger is null)
                {
                    lock(locker)
                    {
                        if(_messenger is null)
                        {
                            _messenger = new Messenger();
                        }
                    }
                }
                return _messenger;
            }
        }
        public void Register<T>(object recipient, Action<T> action)
        {
            Register(recipient, action);
        }
        public void Register<T>(object recipient, Action<T> action, object context)
        {
            var key = new MessengerKey(recipient, context);
            Dictionary.TryAdd(key, action);
        }
        public void Unregister(object recipient)
        {
            Unregister(recipient);
        }
        public void Unregister(object recipient, object context)
        {
            object action;
            var key = new MessengerKey(recipient, context);
            Dictionary.TryRemove(key, out action);
        }
        public void Send<T>(T message)
        {
            Send(message);
        }
        public void Send<T>(T message, object context)
        {
            IEnumerable<KeyValuePair<MessengerKey, object>> result;
            if (context is null)
                result = from r in Dictionary where r.Key.Context is null select r;
            else
                result = from r in Dictionary where !(r.Key.Context is null) && r.Key.Context.Equals(context) select r;
            foreach (var a in result.Select(x => x.Value).OfType<Action<T>>())
            {
                a(message);
            }
        }
        protected class MessengerKey
        {
            public object Recipient { get; private set; }
            public object Context { get; private set; }
            public MessengerKey(object recipient, object context)
            {
                Recipient = recipient;
                Context = context;
            }
            protected bool Equals(MessengerKey key)
            {
                return Equals(Recipient, key.Recipient) && Equals(Context, key.Context);
            }
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((MessengerKey)obj);
            }
            //public override int GetHashCode()
            //{
            //    unchecked
            //    {
            //
            //    }
        }
    }
}
