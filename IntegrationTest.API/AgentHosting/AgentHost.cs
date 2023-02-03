namespace IntegrationTest.API.AgentHosting
{
    [Serializable]
    public class AgentHost : MarshalByRefObject
    {
        private IAgentService _agentService;


        public void InjectAgentService(IAgentService agentService)
        {
            _agentService = agentService ?? throw new ArgumentNullException(nameof(agentService));
        }
        public void Start()
        {
            _agentService.Start();
        }
    }
}
