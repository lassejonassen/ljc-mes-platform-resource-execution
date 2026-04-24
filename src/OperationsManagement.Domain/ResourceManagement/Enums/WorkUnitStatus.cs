namespace ResourceExecution.Domain.ResourceManagement.Enums;

/// <summary>
/// Represents the current physical and operational state of a Work Unit.
/// These states are used by the Dispatcher to determine machine availability.
/// </summary>
public enum WorkUnitStatus
{
    /// <summary>
    /// The unit is healthy, powered on, and waiting for a job.
    /// </summary>
    Idle,

    /// <summary>
    /// The unit is currently executing a Production Job.
    /// </summary>
    Running,

    /// <summary>
    /// The unit is stopped due to an unplanned event (breakdown, emergency stop).
    /// </summary>
    Down,

    /// <summary>
    /// The unit is undergoing planned maintenance or cleaning (CIP).
    /// </summary>
    Maintenance,

    /// <summary>
    /// The unit is functional but blocked by a downstream process (e.g., full conveyor).
    /// </summary>
    Blocked,

    /// <summary>
    /// The unit is waiting for upstream materials or an operator.
    /// </summary>
    Starved,

    /// <summary>
    /// The unit is being configured for a new product type (Changeover).
    /// </summary>
    Setup
}