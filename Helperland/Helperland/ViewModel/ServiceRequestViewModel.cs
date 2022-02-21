using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ServiceRequestViewModel : UserAddressViewModel
    {

        [Column(TypeName = "datetime")]
        public DateTime ServiceStartDate { get; set; }

        [Column(TypeName = "date")]
        public string Date { get; set; }

        public string Time { get; set; }

        public double ServiceHours { get; set; }

        public double? ExtraHours { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal TotalCost { get; set; }

        [Display(Prompt = "Add comment")]
        [StringLength(500)]
        public string Comments { get; set; }

        public bool HasPets { get; set; }
        public int? Status { get; set; }

        public bool ExtraService1 { get; set; }

        public bool ExtraService2 { get; set; }

        public bool ExtraService3 { get; set; }

        public bool ExtraService4 { get; set; }

        public bool ExtraService5 { get; set; }


    }
}
