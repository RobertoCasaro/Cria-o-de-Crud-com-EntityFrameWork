using System;
using System.Collections.Generic;

namespace TesteVeranine.Models;

public partial class Medico
{
    public int Idmed { get; set; }

    public string Nome { get; set; } = null!;

    public string Crm { get; set; } = null!;

    public int Cpf { get; set; }

    public virtual ICollection<FichaMed> FichaMeds { get; set; } = new List<FichaMed>();
}
