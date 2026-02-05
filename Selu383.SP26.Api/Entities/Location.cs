namespace Selu383.SP26.Api.Entities;

public class Location
{
    public int Id { get; set; }
    public required String Name { get; set; }
    public required String Address { get; set; }
    public int TableCount { get; set; }
}

public class LocationGetDto
{
    public int Id { get; set; }
    public required String Name { get; set; }
    public required String Address { get; set; }
    public int TableCount { get; set; }
}

public class LocationCreateUpdateDto
{
    public required String Name { get; set; }
    public required String Address { get; set; }
    public int TableCount { get; set; }
}