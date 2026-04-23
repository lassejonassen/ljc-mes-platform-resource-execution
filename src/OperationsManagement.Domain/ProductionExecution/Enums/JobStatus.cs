namespace ResourceExecution.Domain.ProductionExecution.Enums;

public enum JobStatus
{
    Pending,    // The Job exists within a released order, but hasn't been sent to the equipment.
    Dispatched, // The Dispatcher has published the JSON payload to the .../Cmd/Parameters MQTT topic.
    Accepted,   // (Optional Handshake) Th ePLC has recevied the parameters and sent an "ACK" back to the UNS.
    Running,    // The machine has started the process. Your system detects this via a state change on the equipment's status topic.
    Completed,  // The specific unit (e.g., the filler) has finished its part of the work.
    Failed      // The machine encourted a fault, or the dispatcher couldn't reach the MQTT broker.
}
