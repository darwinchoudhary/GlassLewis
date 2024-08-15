using System;
using System.ComponentModel.DataAnnotations;

namespace GlassLewisWebAPI.Models;

public class Company
{
    [Required]
    public int Id { get; set;}   
    [Required]
    public string Name { get; set;}    
    [Required]
    public string Exchange{ get; set;}
    [Required]
    public string Ticker{ get; set;}
    [Required]
    [RegularExpression("^[A-Za-z]{2}\\w{10}$", ErrorMessage = "ISIN must start with two letters and followed by 10 alphanumeric characters.")]
    public string Isin{ get; set;}
    public string Website{ get; set;} = "";
}
