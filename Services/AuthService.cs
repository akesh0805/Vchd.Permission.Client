namespace Vchd.Permission.Client.Services;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Vchd.Permission.Client.Models;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private readonly JwtAuthenticationStateProvider _authProvider;

    public AuthService(HttpClient http, ILocalStorageService localStorage, AuthenticationStateProvider authProvider)
    {
        _http = http;
        _localStorage = localStorage;
        _authProvider = (JwtAuthenticationStateProvider)authProvider;
    }

    public async Task<bool> LoginAsync(LoginModel model)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", model);
        if (!response.IsSuccessStatusCode) return false;

        var result = await response.Content.ReadFromJsonAsync<LoginResult>();
        await _localStorage.SetItemAsync("authToken", result!.Token);
        _authProvider.NotifyUserAuthentication(result.Token);

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
        return true;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _authProvider.NotifyUserLogout();
        _http.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<bool> RegisterAsync(RegisterModel model)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", model);
        return response.IsSuccessStatusCode;
    }
}
