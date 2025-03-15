using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeiraApi.Domain.Model
{
    [Table("user")]
    public class User
    {
        public int id { get; private set; }
        public string nome { get; private set; }
        public string age { get; private set; }
        public string? photo { get; private set; }
        public User() { }
        public User(string name, string age, string? photo)
        {
            nome = name ?? throw new Exception("O nome precissa estar completo");
            this.age = age;
            this.photo = photo;
        }



    }
}
