using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EReceipt.Utils;
using Microsoft.AspNetCore.Http;

namespace EReceipt.ViewModels
{
    public class MedicamentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [AllowedExtensions(new[] { ".jpeg", ".bmp", ".png", ".jpg" })]
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }

        public byte[] PictureByte { get; set; }

        public string ContentType { get; set; }

        public int CategoryId { get; set; }

        public int ManufacturerId { get; set; }

        public string Instruction { get; set; }

        public string Description { get; set; }

        public string ManufacturerName { get; set; }

        public string ManufacturerLicense { get; set; }

        public SelectViewModel MedicamentCategory { get; set; }

        public ICollection<SelectViewModel> Pharmacies { get; set; }
    }
}
