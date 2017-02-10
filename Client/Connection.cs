using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Configuration;

namespace Client
{ 
    public static class Connection
    {
        static IContract channel;

        public static IContract Channel { get { return channel; } }

        static Connection()
        {
            Uri address = new Uri(ConfigurationManager.AppSettings.Get("Address"));
            EndpointAddress endPoint = new EndpointAddress(address);
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IContract> factory = new ChannelFactory<IContract>(binding, endPoint);
            channel = factory.CreateChannel();
        }
    }
}
