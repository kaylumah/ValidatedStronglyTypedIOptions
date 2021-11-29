// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using Kaylumah.ValidatedStronglyTypedIOptions.ApiClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaylumah.ValidatedStronglyTypedIOptions.ApiClient.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public Profile CreateProfile(Profile profile)
    {
        _logger.LogInformation("profile created!");
        return profile;
    }
}
