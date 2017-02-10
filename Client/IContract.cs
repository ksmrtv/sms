using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Client
{
    [ServiceContract]
    public interface IContract
    {
        [OperationContract]
        string RegisterUser(string login, string pass);

        [OperationContract]
        string Enter(string login, string pass);

        [OperationContract]
        double GetBalance(string login);

        [OperationContract]
        bool AddBalance(string login, double sum);

        [OperationContract]
        bool SendMessage(string login, string phone, string message);

        [OperationContract]
        string[] GetLastPhoneNumbers(int countNumbers, string login);
    }
}
