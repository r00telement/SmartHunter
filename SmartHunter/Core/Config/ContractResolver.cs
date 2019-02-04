using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SmartHunter.Core.Config
{
    public class ContractResolver : DefaultContractResolver
    {
        public static ContractResolver Instance { get; private set; }

        static ContractResolver() { Instance = new ContractResolver(); }

        protected ContractResolver() : base() { }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);
            if (jsonProperty.ObjectCreationHandling == null && GetDictionaryKeyType(jsonProperty.PropertyType) != null && member.GetCustomAttribute<MaintainCollectionIntegrity>() == null)
            {
                jsonProperty.ObjectCreationHandling = ObjectCreationHandling.Replace;
            }

            return jsonProperty;
        }

        Type GetDictionaryKeyType(Type type)
        {
            while (type != null)
            {
                if (type.IsGenericType)
                {
                    var genType = type.GetGenericTypeDefinition();
                    if (genType == typeof(Dictionary<,>))
                    {
                        return type.GetGenericArguments()[0];
                    }
                }

                type = type.BaseType;
            }

            return null;
        }
    }
}
