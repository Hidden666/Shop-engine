//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Data.Interfaces;

namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer : IEntityBase
    {
        public Customer()
        {
            this.Rental = new HashSet<Rental>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentityCard { get; set; }
        public int UniqueKey { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
    
        public virtual ICollection<Rental> Rental { get; set; }
    }
}