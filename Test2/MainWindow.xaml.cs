using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
        
    /***************/
    /* Main method */
    /***************/
    public partial class MainWindow : Window
    {
        /********************************************************/
        /* definition of local function variables and constants */
        /********************************************************/
        private const string    endpoint    = "a1iehf79xi992z.iot.us-east-2.amazonaws.com";
        private const int       brokerPort  = MqttSettings.MQTT_BROKER_DEFAULT_SSL_PORT;
        private const string    topic       = "/test";

        public MainWindow()
        {
            /* Publish to the topic */
            publish();
            Console.WriteLine("***************");
            Console.WriteLine("End of Program!");
            Console.WriteLine("***************\n");
        }

        /*****************************************************************/
        /* Method to create a client ID connected to the client instance */
        /*****************************************************************/
        private void createClientId(MqttClient c)
        {
            /* Client ID */
            string clientId = "Client_Roberto";

            /* Connect client ID to client instance */
            c.Connect(clientId);
        }

        /********************************/
        /* Method to publish to a topic */
        /********************************/
        private void publish()
        {
            /* ca certificate */
            var caCert      = new X509Certificate(@"C:\Users\Roberto.Olivieri\Desktop\Stage\IoT\test\VeriSign-Class 3-Public-Primary-Certification-Authority-G5.pem.txt");
            /* client certificate */
            var clientCert  = new X509Certificate2(@"C:\Users\Roberto.Olivieri\Desktop\Stage\IoT\test\a09338111d-certificate.pem.pfx");
            /* client instance */
            var client      = new MqttClient(endpoint,
                                             brokerPort,
                                             true,
                                             caCert,
                                             clientCert,
                                             MqttSslProtocols.TLSv1_2);
            /* message to publish */
            var message     = "Message from VS!";

            /* Create client ID connected to client instance */
            createClientId(client);

            /* Subscribe to topic */
            Console.WriteLine("Subscribing to topic...\n");
            client.Subscribe(new string[] { topic },
                             new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            /* Publish to the topic */
            Console.WriteLine("Publishing...\n");
            client.Publish(topic,
                           Encoding.UTF8.GetBytes(message));
            if (client.IsConnected)
            {
                Console.WriteLine("Success!");
            }
            Console.ReadLine();
            /* Disconnection */
            Console.WriteLine("Disconnection...\n");
            client.Disconnect();
        }
    }
}
