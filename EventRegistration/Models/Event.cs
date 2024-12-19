using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventRegistration.Models;
public class Event
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }
    public string Location { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    public ICollection<Registration> Registrations { get; set; }=  new List<Registration>();
}