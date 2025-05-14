namespace Instagram.API.Repositorio
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}