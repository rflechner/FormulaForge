using System.Net.Http.Json;
using FormulaForge.Domain.Entities;
using FormulaForge.Engine.Time;

namespace FormulaForge.Domain.Services;

public interface IDataSourcesProvider
{
    Task<TimeSeries<decimal>?> GetDecimalTimeSeriesAsync(DataSourceSpec dataSource, CancellationToken cancellationToken = default);
}

public class RestApiDataSourcesProvider(IHttpClientFactory httpFactory) : IDataSourcesProvider
{
    public async Task<TimeSeries<decimal>?> GetDecimalTimeSeriesAsync(DataSourceSpec dataSource, CancellationToken cancellationToken = default)
    {
        var httpClient = httpFactory.CreateClient(dataSource.Name);

        var result = new TimeSeries<decimal>();
        var start = new DateTimeOffset(new DateOnly(2026, 01, 01), TimeOnly.MinValue, TimeSpan.Zero);
        var end = new DateTimeOffset(new DateOnly(2026, 02, 01), TimeOnly.MinValue, TimeSpan.Zero);
        result.Add(new Period(start, end), 100m, (o, n) => n.Value + o.Value);
        
        
        throw new NotImplementedException();
    }
}
