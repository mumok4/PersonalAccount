using System;
using System.Collections.Generic;

namespace PersonalAccount.Data.Models;

public partial class LinksUserCompany
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid? CompanyId { get; set; }

    public virtual Company? Company { get; set; }

    public virtual User? User { get; set; }
}
