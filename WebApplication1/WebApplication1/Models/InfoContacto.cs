namespace WebApplication1.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InfoContacto")]
    public partial class InfoContacto
    {
        [Key]
        public int IdInfo { get; set; }

        public int IdPersona { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Celular { get; set; }

        public string Email { get; set; }

        public string Direccion { get; set; }

        [JsonIgnore]
        public virtual Personas Personas { get; set; }
    }
}
