using System.ServiceModel;

namespace Chatik
{

    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChatik
    {
        [OperationContract] // Только те методы, которые помечены атрибутом OperationContract, доступны клиенту. 
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)] // указывает, что не надо ждать ответа от сервера
        void SentMessage(string msg, int id);
    }

    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
    }
}
