using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Server.Models;
public class Order
{
    [Required]
    public string Customer { get; set; }
    [Required]
    public string Product { get; set; }
    [Required]
    public Int64 Count { get; set; }

    public Order(string customer, string product, Int64 count)
    {
        this.Customer = customer;
        this.Product = product;
        this.Count = count;
    }
}
