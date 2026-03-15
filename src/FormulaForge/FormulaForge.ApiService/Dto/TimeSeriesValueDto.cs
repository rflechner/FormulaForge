using FormulaForge.Engine.Time;

namespace FormulaForge.ApiService.Dto;

public record TimeSeriesDecimalValueDto(Period Period, decimal Value);