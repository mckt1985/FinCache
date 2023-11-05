using EasyNetQ;
using EasyNetQ.SystemMessages;

namespace FinCache.WorkerService.Models.Amqp
{
    internal class AttributeNameSerializer : ITypeNameSerializer
    {
        private static readonly Dictionary<string, Type> MessageMap;
        private static readonly Dictionary<Type, string> TypeMap;

        static AttributeNameSerializer()
        {
            MessageMap = new Dictionary<string, Type>();
            TypeMap = new Dictionary<Type, string>();

            var allTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());
            var messageTypes = allTypes.Where(t => Attribute.IsDefined(t, typeof(MessageTypeAttribute)));

            foreach (var type in messageTypes)
            {
                var attribute = Attribute.GetCustomAttribute(type, typeof(MessageTypeAttribute));
                AddMapping(((MessageTypeAttribute)attribute).Name, type);
            }

            // Add the scheduled messages and error messages since they're built into EasyNetQ
            AddMapping<Error>("fincache.easynetq.error");
            AddMapping<string>("fincache.easynetq.log");
        }

        public static void AddMapping<T>(string name)
        {
            AddMapping(name, typeof(T));
        }

        public static void AddMapping(string name, Type type)
        {
            if (MessageMap.ContainsKey(name))
            {
                throw new ArgumentException(
                    string.Format("Two classes marked with the same MessageType ({0} and {1})",
                        type, MessageMap[name]));
            }

            MessageMap.Add(name, type);
            TypeMap.Add(type, name);
        }

        public string Serialize(Type type)
        {
            if (!TypeMap.ContainsKey(type))
                throw new ArgumentException("Couldn't find type in map", type.ToString());

            return TypeMap[type];
        }

        public Type DeSerialize(string typeName)
        {
            if (!MessageMap.ContainsKey(typeName))
                throw new ArgumentException("Couldn't find name in map", typeName);

            return MessageMap[typeName];
        }
    }
}
