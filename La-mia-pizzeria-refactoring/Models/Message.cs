using NuGet.Protocol.Core.Types;

namespace La_mia_pizzeria_refactoring.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Titolo { get; set; }
        public string Text { get; set; }
    }
}
