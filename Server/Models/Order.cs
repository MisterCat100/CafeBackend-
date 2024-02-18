using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Server.Models;
public class Order
{
    [Required]
    public int ID { get; set; }
    [Required]
    public string Customer { get; set; }
    [Required]
    public string Product { get; set; }
    [Required]
    public int Count { get; set; }

    public Order(int id, string customer, string product, int count)
    {
        this.ID = id;
        this.Customer = customer;
        this.Product = product;
        this.Count = count;
    }
}
