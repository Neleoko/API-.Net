using System;
using System.Collections.Generic;

namespace API_.Net.Entities;

public partial class Anime
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Acronyme { get; set; } = null!;
}
