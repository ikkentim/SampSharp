using System;
using System.Collections.Generic;

namespace CommunicationTest
{
    internal class MessageQueue
    {
        private readonly Queue<byte> _queue = new Queue<byte>(1000);

        private byte _command;
        private uint _commandLength;
        private bool _localFill;
        
        public (byte command, byte[] data) Get()
        {
            if (!CanGet())
                throw new Exception("Can't get");

            var data = new byte[_commandLength];
            for (var i = 0; i < _commandLength; i++)
                data[i] = _queue.Dequeue();

            _localFill = false;
            return (_command, data);
        }

        private bool TryFillLocal()
        {
            if (_localFill)
                return true;
            if (_queue.Count < 5)
                return false;

            _command = _queue.Dequeue();


//            _commandLength = (uint)(
//                (_queue.Dequeue() << 24) |
//                (_queue.Dequeue() << 16) |
//                (_queue.Dequeue() << 8) |
//                _queue.Dequeue());

            _commandLength = (uint)(
                (_queue.Dequeue() << 0) |
                (_queue.Dequeue() << 8) |
                (_queue.Dequeue() << 16) |
                (_queue.Dequeue() << 24));

            _localFill = true;
            
            return true;
        }

        public bool CanGet()
        {
            if (!TryFillLocal())
                return false;

            return _commandLength <= _queue.Count;
        }

        public void Add(byte value)
        {
            _queue.Enqueue(value);
        }
    }
}