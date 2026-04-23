namespace OperationsManagement.Domain.Assets.Enums;

public enum UnitStatus
{
    Idle,           // The unit is functional and available, but no process is currently assigned.
    Inactive,       // The unit is intentionally taken out of the production flow (e.g., scheduled shutdown).
    Allocated,      // The unit has been reserved for a specific production job but has not been started yet.
    Running,        // The unit is actively executing a process or "Job".
    Held,           // The process is termporarily stopped due to an external command or internal logic.
    Faulted,        // The unit has encountered a hardware or software error and cannot proceed.
    Maintenance,    // The unit is being serviced and is unavailable for production.
}
