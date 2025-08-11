using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace login_Form_with_session.Models;

public partial class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
