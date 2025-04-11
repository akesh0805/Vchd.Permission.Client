namespace Vchd.Permission.Client.Services;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vchd.Permission.Client.Models; // Eto budet model dlya NewPermissionModel
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

public class PermissionService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authProvider;
    private readonly ILocalStorageService _localStorage;

    public PermissionService(HttpClient http, AuthenticationStateProvider authProvider, ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _http = http;
        _authProvider = authProvider;
    }

    private async Task AddAuthHeaderAsync(CancellationToken cancellationToken = default)
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated == true)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken"); // LocalStorage or SecureStorage
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<List<PermissionModel>?> GetAllPermissionsAsync()
    {
        await AddAuthHeaderAsync();
        return await _http.GetFromJsonAsync<List<PermissionModel>>("api/permissions");
    }

    public async Task<bool> SubmitPermissionAsync(NewPermissionModel model)
    {
        await AddAuthHeaderAsync();
        var response = await _http.PostAsJsonAsync("api/permissions", model);
        return response.IsSuccessStatusCode;
    }
}
