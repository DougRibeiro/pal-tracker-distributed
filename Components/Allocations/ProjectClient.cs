using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Allocations
{
    public class ProjectClient : IProjectClient
    {
        private readonly HttpClient _client;

        public ProjectClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<ProjectInfo> Get(long projectId)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            var streamTask = _client.GetStreamAsync($"?projectId={projectId}");

            var serializer = new DataContractJsonSerializer(typeof(ProjectInfo));
            return await NewMethod(streamTask, serializer);
        }

        private static async Task<ProjectInfo> NewMethod(Task<System.IO.Stream> streamTask, DataContractJsonSerializer serializer)
        {
            return serializer.ReadObject(await streamTask) as ProjectInfo;
        }
    }
}