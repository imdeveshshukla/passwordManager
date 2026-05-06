using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _config;

    public ConfigController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var appName = _config["MySettings:AppName"];
        var version = _config["MySettings:Version"];

        return Ok(new { appName, version });
    }
}