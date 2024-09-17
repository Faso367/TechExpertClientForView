using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            //Получение сырого XML - ответа
            string rawXml = reply.ToString();
            Console.WriteLine("Сырой XML-ответ:");
            Console.WriteLine(rawXml);
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            // Здесь можно логировать исходящее сообщение (запрос)
            string rawXmlRequest = request.ToString();
            Console.WriteLine("Исходящий SOAP-запрос:");
            Console.WriteLine(rawXmlRequest);

            return null;
        }
    }

    public class CustomBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new MessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }

        public void Validate(ServiceEndpoint endpoint) { }
    }
}
