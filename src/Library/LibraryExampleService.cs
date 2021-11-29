// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Kaylumah.ValidatedStronglyTypedIOptions.Library;

public class LibraryExampleService : ILibraryExampleService
{
    private readonly HttpClient _httpClient;

    public LibraryExampleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> Retrieve()
    {
        var request = new HttpRequestMessage() {
             Method = HttpMethod.Get
        };
        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return result;
    }
}
