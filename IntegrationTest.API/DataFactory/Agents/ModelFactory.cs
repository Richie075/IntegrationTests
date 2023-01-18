
namespace IntegrationTest.API.DataFactory.Agents
{
    public class ModelFactory
    {
        public ModelFactory()
        {
            
        }

        public T CreateModel<T>(ModelCreator<T> creator, string name = "") where T : class, new()
        {
            return creator.CreateModel(name);
        }
    }
}
