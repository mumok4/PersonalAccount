using System;
using System.Collections.Generic;

namespace PersonalAccount.Data.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<LinksUserCompany> LinksUserCompanies { get; set; } = new List<LinksUserCompany>();
}
