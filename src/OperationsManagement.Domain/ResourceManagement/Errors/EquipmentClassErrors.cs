using ResourceExecution.Domain.ResourceManagement.Aggregates;
using ResourceExecution.SharedKernel;

namespace ResourceExecution.Domain.ResourceManagement.Errors;

public static class EquipmentClassErrors
{
    private const string Prefix = nameof(EquipmentClass);

    public static readonly Error NotFound
        = new($"{Prefix}.NotFound", "The Equipment Class with the specified ID was not found", ErrorType.NotFound);

    public static readonly Error AlreadyExists
        = new($"{Prefix}.AlreadyExists", "The Equipment Class with the specified name already exists", ErrorType.Failure);

    public static readonly Error NameRequired
        = new($"{Prefix}.NameRequired", "Name is required", ErrorType.Failure);

    public static readonly Error NameTooLong
        = new($"{Prefix}.NameTooLong", "The name is too long", ErrorType.Failure);

    public static readonly Error DescriptionTooLong
        = new($"{Prefix}.DescriptionTooLong", "The description is too long", ErrorType.Failure);

    public static readonly Error CapabilityExists
        = new($"{Prefix}.CapabilityExists", "The Equipment Capability with the specified name already exists", ErrorType.Failure);

    public static readonly Error CapabilityNotFound
        = new($"{Prefix}.CapabilityNotFound", "The Equipment Capability was not found", ErrorType.NotFound);
}
