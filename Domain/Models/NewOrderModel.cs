using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class NewOrderModel
    {
        public int? CustId { get; set; } = null;

        [Required(ErrorMessage = "El ID del empleado es obligatorio.")]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "El ID del transportista es obligatorio.")]
        public int ShipperId { get; set; }

        [Required(ErrorMessage = "El nombre del destinatario es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del destinatario no puede superar los 100 caracteres.")]
        public string ShipName { get; set; } = null!;

        [Required(ErrorMessage = "La dirección de envío es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección de envío no puede superar los 200 caracteres.")]
        public string ShipAddress { get; set; } = null!;

        [Required(ErrorMessage = "La ciudad de envío es obligatoria.")]
        [StringLength(50, ErrorMessage = "La ciudad de envío no puede superar los 50 caracteres.")]
        public string ShipCity { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de la orden es obligatoria.")]
        public DateTime OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El costo de envío debe ser mayor o igual a 0.")]
        public decimal Freight { get; set; }

        [Required(ErrorMessage = "El país de envío es obligatorio.")]
        [StringLength(50, ErrorMessage = "El país de envío no puede superar los 50 caracteres.")]
        public string ShipCountry { get; set; } = null!;

        [Required(ErrorMessage = "Debe agregar al menos un producto en la orden.")]
        public List<OrderDetailModel> OrderDetails { get; set; } = new();
    }

    public class OrderDetailModel
    {
        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor a 0.")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Qty { get; set; }

        public float Discount { get; set; } = 0;
    }
}
