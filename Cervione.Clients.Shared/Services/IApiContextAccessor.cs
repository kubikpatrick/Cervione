using Cervione.Clients.Shared.Services.Api;

namespace Cervione.Clients.Shared.Services;

public interface IApiContextAccessor : IServerUrlAccessor, ITokenAccessor;