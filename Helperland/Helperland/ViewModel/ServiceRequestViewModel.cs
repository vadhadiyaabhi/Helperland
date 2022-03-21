using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ServiceRequestViewModel : ZipCodeViewModel
    {

        //[Column(TypeName = "datetime")]
        //public DateTime ServiceStartDate { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public double ServiceHours { get; set; }

        public double? ExtraHours { get; set; }

        
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

        public bool[] ExtraServices { get; set; }

        [Required]
        public int AddressId { get; set; }

        public int FavouriteProsId { get; set; }


    }
}
