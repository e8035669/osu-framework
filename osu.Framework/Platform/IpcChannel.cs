﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using System.Threading.Tasks;

namespace osu.Framework.Platform
{
    public class IpcChannel<T> : IDisposable
    {
        private BasicGameHost host;
        public event Action<T> MessageReceived;
    
        public IpcChannel(BasicGameHost host)
        {
            this.host = host;
            this.host.MessageReceived += handleMessage;
        }

        {
            var msg = new IpcMessage
            {
                Type = typeof(T).AssemblyQualifiedName,
                Value = message,
            };
            await host.SendMessage(msg);
        }

        {
            if (message.Type != typeof(T).AssemblyQualifiedName)
                return;
            MessageReceived?.Invoke((T)message.Value);
        }

        public void Dispose()
        {
            host.MessageReceived -= handleMessage;
        }
    }
}