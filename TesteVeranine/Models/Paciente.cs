using System;
using System.Collections.Generic;

namespace TesteVeranine.Models;

public partial class Paciente
{
    public int Idpac { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Cpf { get; set; }

    public int Telefone { get; set; }

    public virtual ICollection<FichaMed> FichaMeds { get; set; } = new List<FichaMed>();
}
