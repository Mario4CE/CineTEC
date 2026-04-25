public class Sala
{
    public int Id { get; set; }
    public string Identificador { get; set; }
    public int SucursalId { get; set; }

    public Sucursal Sucursal { get; set; }
}