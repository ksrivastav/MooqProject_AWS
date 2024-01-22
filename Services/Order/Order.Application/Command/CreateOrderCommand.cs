using MediatR;
using Order.Application.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Command
{
    public class CreateOrderCommand: IRequest<OrderResponce>
    {
        public int? Id { get; set; }
        //Below Properties are Audit properties
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? UserName { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? AddressLine { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? CardName { get; set; }
        public string? CardNumber { get; set; }
        public string? Expiration { get; set; }
        public string? Cvv { get; set; }
        public int? PaymentMethod { get; set; }
        public int? Productid { get; set; }
        public string CorrelationId { get; set; }
    }
}
