using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Chatik
{
    
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] //ВАЖНО: при подключении юзера создается единая сессия для всех
    public class ServiceChatik : IServiceChatik 
    {
        List<ServerUsers> serverUsers = new List<ServerUsers>(); // список для хранения юзеров

        int nextId = 1;

        public int Connect(string name)
        {
            ServerUsers user = new ServerUsers()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++; // после приcваения юзеру ID, увеличиваем значение ID на 1

            var message = $"{user.Name} подключился к Чатику ";

            SentMessage(message, 0); // сообщаем о подключении нового юзера в Чатик, id=0 - чтобы не передавать в чат ID юзера

            serverUsers.Add(user); 
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = serverUsers.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                serverUsers.Remove(user);
                SentMessage(user.Name + " покинул Чатик", 0);
            }
        }

        public void SentMessage(string msg, int id)
        {
            var user = serverUsers.FirstOrDefault(i => i.ID == id);

            if (user == null) { return; }

            foreach (var item in serverUsers)
            {
               string answer = $"{user.Name} ({DateTime.Now.ToShortTimeString()}): {msg}";
  
                item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);
            }
        }
    }
}
