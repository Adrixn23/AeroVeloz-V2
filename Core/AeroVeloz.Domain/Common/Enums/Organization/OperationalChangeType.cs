namespace AeroVeloz.Domain.Common.Enums.Organization
{
    public enum OperationalChangeType  // enum que gestiona los elementos operacionales que puede realizar 
        //el equipo de operaciones del aeropuerto en caso de que se requieran mas acciones se pueden colocar directamente
        //en este elemento, de momento solo coloque las 4 basica 
    {
        EstimatedTimeUpdated =1,
        GateChanged =2,
        Cancelled =3,
        TerminalOrRoomReassigned =4
    }
}
