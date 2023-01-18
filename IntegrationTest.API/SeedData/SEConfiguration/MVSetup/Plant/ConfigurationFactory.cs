namespace IntegrationTest.API.SeedData.SEConfiguration.MVSetup.Plant
{
    public  class ConfigurationFactory
    {
        private ISetupCreator _setupCreator;

        public ConfigurationFactory(ISetupCreator setupCreator)
        {
            _setupCreator = setupCreator ?? throw new ArgumentNullException(nameof(setupCreator));
        }

        public void CreateSetup()
        {
            _setupCreator.CreateSetup();
        }
    }
}
