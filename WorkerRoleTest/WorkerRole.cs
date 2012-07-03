using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Threading;
using Microsoft.WindowsAzure;
using ReactiveAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace WorkerRoleTest
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("WorkerRoleTest entry point called", "Information");
            var cloudStorageAccount = CloudStorageAccount.FromConfigurationSetting("");
            
            var cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();

            var testQueue = cloudQueueClient.GetQueueReference("testqueue");

            testQueue.CreateIfNotExist();
            
            var observable = QueueObserver<TestMessage>.Factory.CreateObservable(TimeSpan.FromSeconds(5), testQueue);
            using (var resetEvent = new ManualResetEventSlim(false))
            {
                observable.Subscribe(Observer.Create<TypedMessage<TestMessage>>(HandleIncomingMessage, _ => resetEvent.Set(), resetEvent.Set));
                resetEvent.Wait();
            }
        }

        public void HandleIncomingMessage(TypedMessage<TestMessage> message)
        {
            var contents = message.Value;
            Trace.WriteLine(contents.MessageIdentifier);
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
