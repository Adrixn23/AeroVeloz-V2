using System;
using System.Collections.Generic;
namespace AeroVeloz.Domain.Entities.Flights;

public partial class ViewFlightHistoryChronological
{
    public DateTime? ChangeDate { get; set; }

    public string FlightNumber { get; set; } = null!;

    public string? OldState { get; set; }

    public string? NewState { get; set; }
}
