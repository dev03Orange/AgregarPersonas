namespace WebApplication1.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Personas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personas()
        {
            InfoContacto = new HashSet<InfoContacto>();
        }

        [Key]
        public int IdPersona { get; set; }

        [Required]
        [StringLength(50)]
        public string Documento { get; set; }

        [Required]
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime FechaNacimiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<InfoContacto> InfoContacto { get; set; }
    }
}
