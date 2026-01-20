namespace KlantenDienstData.Models;

public partial class PersoneelslidAccount
{
    public int PersoneelslidAccountId { get; set; }

    public string Emailadres { get; set; } = null!;

    public string Paswoord { get; set; } = null!;

    public bool Disabled { get; set; }

    public virtual ICollection<Chatgespreklijn> Chatgespreklijnen { get; set; } = new List<Chatgespreklijn>();

    public virtual ICollection<Personeelslid> Personeelsleden { get; set; } = new List<Personeelslid>();
}
