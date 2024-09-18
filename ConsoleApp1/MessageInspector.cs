using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;

namespace ConsoleApp1
{
    public class MessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// Получение сырого XML - ответа
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            string rawXml = reply.ToString();
            Console.WriteLine("Сырой XML-ответ:");
            Console.WriteLine(rawXml);
            Console.WriteLine();
        }

        /// <summary>
        /// Здесь можно логировать исходящее сообщение (запрос)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            string rawXmlRequest = request.ToString();
            Console.WriteLine("Исходящий SOAP-запрос:");
            Console.WriteLine(rawXmlRequest);
            Console.WriteLine();

            return null;
        }
    }
}
