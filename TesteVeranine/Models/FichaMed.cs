using System;
using System.Collections.Generic;

namespace TesteVeranine.Models;

public partial class FichaMed
{
    public int Idfic { get; set; }

    public int? Idmed { get; set; }

    public int? Idpac { get; set; }

    public virtual Medico? IdmedNavigation { get; set; }

    public virtual Paciente? IdpacNavigation { get; set; }
}
