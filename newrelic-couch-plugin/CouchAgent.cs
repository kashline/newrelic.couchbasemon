using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using NewRelic.Platform.Sdk;
using NewRelic.Platform.Sdk.Processors;
using NewRelic.Platform.Sdk.Utils;
using Newtonsoft.Json.Linq;


namespace newrelic_couch_plugin
{
    public class CouchAgent : Agent
    {
        private static readonly Logger Logger = Logger.GetLogger("NeuronEsbLogger");
        private readonly Version _version = Assembly.GetExecutingAssembly().GetName().Version;
        private readonly string _name;
        private static string _host;
        private static int _port;
        private static string _instance;

        private readonly IDictionary<string, IDictionary<string, EpochProcessor>> _queueProcessors;
        private readonly IProcessor _heartbeats;
        private readonly IProcessor _errors;
        private readonly IProcessor _warnings;
        private readonly IProcessor _messagesProcessed;

        public CouchAgent(string name, string host, int port, string instance)
        {
            //if (string.IsNullOrEmpty(name))
            //    throw new ArgumentNullException(nameof(name), "Name must be specified for the agent to initialize");
            //if (string.IsNullOrEmpty(host))
            //    throw new ArgumentNullException(nameof(host), "Host must be specified for the agent to initialize");
            //if (string.IsNullOrEmpty(instance))
            //    throw new ArgumentNullException(nameof(instance), "Instance must be specified for the agent to initialize");

            _name = name;
            _host = host;
            _port = port;
            _instance = instance;

            _queueProcessors = new Dictionary<string, IDictionary<string, EpochProcessor>>();
            _heartbeats = new EpochProcessor();
            _errors = new EpochProcessor();
            _warnings = new EpochProcessor();
            _messagesProcessed = new EpochProcessor();
        }

        public override string Guid
            {
	            get { "newrelic.couchmon"; }
            }
        public override string Version = 

        public override string GetAgentName()
        {
            return _name;
        }

        public override void PollCycle()
        {
            var couchUri = "http://{_host}:{_port}";
            var poolsEndpoint = "/pools";

            var uri = couchUri + poolsEndpoint;
            Logger.Debug("Endpoint URI: " + uri);
            var client = new WebClient();
            client.Headers.Add("content-type", "application/json");

            Logger.Debug("Getting pool information...");

            var json = JArray.Parse(client.DownloadString(uri));
            
            Logger.Debug("Response received:\n" + json);

            Logger.Debug("Sending Summary Metrics to New Relic");

            

            //ReportMetric("Summary/Heartbeats", "Messages/Second", _heartbeats.Process(json.Sum(j => j["heartbeats"].Value<float>())));
            //ReportMetric("Summary/Errors", "Messages/Second", _errors.Process(json.Sum(j => j["errors"].Value<float>())));
            //ReportMetric("Summary/Warnings", "Messages/Second", _warnings.Process(json.Sum(j => j["warnings"].Value<float>())));
            //ReportMetric("Summary/MessagesProcessed", "Messages/Second", _messagesProcessed.Process(json.Sum(j => j["messagesProcessed"].Value<float>())));
        }

    }
}
