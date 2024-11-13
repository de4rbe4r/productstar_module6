using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Configuration
{
    /// <summary>
    /// Настойки Kafka.
    /// </summary>
    public static class KafkaConfig
    {
        public static string BootstrapServers = "localhost:9092";
    }
}
