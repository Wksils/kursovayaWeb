namespace kursovayaWeb.Models
{
    public class ProductionBatch
    {
        public int BatchId { get; set; }
        public string BatchNumber { get; set; } = null!;
        public int ProductId { get; set; }
        public int RecipeId { get; set; }
        public int CardId { get; set; }
        public decimal PlannedQty { get; set; }
        public decimal? ActualQty { get; set; }
        public int UomId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? StartedBy { get; set; }
        public int? CompletedBy { get; set; }
        public string? QaDecision { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class Product
    {
        public int ProductId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public string ReleaseForm { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
    public class BatchStepExecution
    {
        public int ExecutionId { get; set; }
        public int BatchId { get; set; }
        public int StepId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? StartedBy { get; set; }
        public int? CompletedBy { get; set; }
        public string? ActualParams { get; set; }
        public string? Notes { get; set; }
    }
    public class TechStep
    {
        public int StepId { get; set; }
        public int CardId { get; set; }
        public int StepNumber { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? EquipmentId { get; set; }
        public int? DurationMin { get; set; }
        public bool IsCritical { get; set; }
        public string? ParamsNote { get; set; }
    }
    public class ExtruderEvent
    {
        public int EventId { get; set; }
        public int ExecutionId { get; set; }
        public string Zone { get; set; } = null!;
        public string EventType { get; set; } = null!;
        public string? ParameterName { get; set; }
        public decimal? ActualValue { get; set; }
        public decimal? TargetValue { get; set; }
        public string? Description { get; set; }
        public int? RecordedBy { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
