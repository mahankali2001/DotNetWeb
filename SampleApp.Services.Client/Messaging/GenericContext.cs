using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SampleApp.Services.Core
{
    [DataContract]
    public class GenericContext<T>
    {
        internal static string TypeName;
        internal static string TypeNamespace;

        [DataMember] public readonly T Value;

        static GenericContext()
        {
            //Verify [DataContract] or [Serializable] on T
            Debug.Assert(IsDataContract(typeof (T)) || typeof (T).IsSerializable);

            TypeNamespace = "net.clr:" + typeof (T).FullName;
            TypeName = "GenericContext";
        }

        public GenericContext(T value)
        {
            Value = value;
        }

        public GenericContext()
            : this(default(T))
        {
        }

        public static GenericContext<T> Current
        {
            get
            {
                OperationContext context = OperationContext.Current;
                if (context == null)
                {
                    return null;
                }
                try
                {
                    return context.IncomingMessageHeaders.GetHeader<GenericContext<T>>(TypeName, TypeNamespace);
                }
                catch (Exception exception)
                {
                    Debug.Assert(exception is MessageHeaderException &&
                                 exception.Message ==
                                 "There is not a header with name " + TypeName + " and namespace " + TypeNamespace +
                                 " in the message.");
                    return null;
                }
            }
            set
            {
                OperationContext context = OperationContext.Current;
                Debug.Assert(context != null);

                //Having multiple GenericContext<T> headers is an error
                bool headerExists = false;
                try
                {
                    context.OutgoingMessageHeaders.GetHeader<GenericContext<T>>(TypeName, TypeNamespace);
                    headerExists = true;
                }
                catch (MessageHeaderException exception)
                {
                    Debug.Assert(exception.Message ==
                                 "There is not a header with name " + TypeName + " and namespace " + TypeNamespace +
                                 " in the message.");
                }
                if (headerExists)
                {
                    throw new InvalidOperationException("A header with name " + TypeName + " and namespace " +
                                                        TypeNamespace + " already exists in the message.");
                }
                var genericHeader = new MessageHeader<GenericContext<T>>(value);
                context.OutgoingMessageHeaders.Add(genericHeader.GetUntypedHeader(TypeName, TypeNamespace));
            }
        }

        private static bool IsDataContract(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof (DataContractAttribute), false);
            return attributes.Length == 1;
        }
    }
}