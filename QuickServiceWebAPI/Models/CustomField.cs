namespace QuickServiceWebAPI.Models;

public partial class CustomField
{
    public string CustomFieldId { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string FieldCode { get; set; } = null!;

    public string FieldName { get; set; } = null!;

    public string? FieldDescription { get; set; }

    public string FieldType { get; set; } = null!;

    public string ValType { get; set; } = null!;

    public int? MinVal { get; set; }

    public int? MaxVal { get; set; }

    public int? MinLength { get; set; }

    public int? MaxLength { get; set; }

    public string? DefaultValue { get; set; }

    public string? ListOfValue { get; set; }

    public string? ListOfValueDisplay { get; set; }

    public virtual ICollection<RequestTicketExt> RequestTicketExts { get; set; } = new List<RequestTicketExt>();

    public virtual ICollection<ServiceItemCustomField> ServiceItemCustomFields { get; set; } = new List<ServiceItemCustomField>();
}
