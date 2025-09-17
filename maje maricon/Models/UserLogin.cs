using System.ComponentModel.DataAnnotations.Schema;

public class UserLogin
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    // Relación uno a uno con GoogleUserData (opcional) (estudiar)
    public string? GoogleUserSub { get; set; }
    [ForeignKey("GoogleUserSub")]
    public GoogleUserData? GoogleUserData { get; set; }
}