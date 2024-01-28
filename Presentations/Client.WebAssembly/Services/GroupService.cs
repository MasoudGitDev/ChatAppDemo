using Shared.DTOs.Group;
using Shared.Extensions;
using Shared.Models;

namespace Client.WebAssembly.Services;
public interface IGroupService {
    Task<Result<LinkedList<GroupResultDto>>> GetUserGroupsAsync(Guid userId);
}
public class GroupService(HttpClient _httpClient) : IGroupService {

    private readonly string baseUrl = "/api/Messaging/Groups/";

    public async Task<Result<LinkedList<GroupResultDto>>> GetUserGroupsAsync(Guid userId) {
        var response = await _httpClient.GetAsync(baseUrl + "GetUserGroups" + "?userId=" + userId);       
        if(response is null) {
            throw new ArgumentNullException();
        }
        if(!response.IsSuccessStatusCode) {
            throw new Exception("Failed - Code :" + response.StatusCode);
        }
        Console.WriteLine(response.Content);
        return (await response.Content.ReadAsStringAsync()).FromJsonTo<Result<LinkedList<GroupResultDto>>>();
    }
}
