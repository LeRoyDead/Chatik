﻿using System.ServiceModel;
namespace Chatik
{
    class ServerUsers
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public OperationContext operationContext { get; set; }
    }
}
