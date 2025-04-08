using System;
using System.Collections.Generic;

namespace Quiq_Application.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Gender { get; set; }

    public int UserAddressId { get; set; }

    public int UserTypeId { get; set; }

    public bool UserValidation { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

    public virtual UserAddress UserAddress { get; set; } = null!;

    public virtual UserType UserType { get; set; } = null!;
}
