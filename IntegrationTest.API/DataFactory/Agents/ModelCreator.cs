namespace IntegrationTest.API.DataFactory.Agents
{
    public abstract class ModelCreator<T> where T : class, new()
    {

        protected ModelCreator()
        {
            
        }

        public virtual T CreateModel(string prefix = "")
        {
            var model = ConstructModel(prefix);
            return model;
        }

        internal abstract T ConstructModel(string prefix = "");

    }
}
