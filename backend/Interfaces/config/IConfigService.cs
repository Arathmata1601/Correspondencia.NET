using backend.Models.config;

namespace backend.Interfaces.config;
public interface IConfigService
{
    IEnumerable<Config> GetConfig();
}