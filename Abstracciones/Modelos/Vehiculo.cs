﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Abstracciones.Modelos
{
    public class VehiculoBase
    {
        [Required(ErrorMessage = "La propiedad placa es requerida")]
        [RegularExpression(@"[A-Za-z]{3}-[0-9]{3}", ErrorMessage = "El formato de la placa debe ser ABC-###")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "La propiedad color es requerida")]
        [StringLength(40, ErrorMessage = "La propiedad color debe ser mayor a 4 caracteres y menor a 40", MinimumLength = 4)]
        public string Color { get; set; }

        [Required(ErrorMessage = "La propiedad año es requerida")]
        [RegularExpression(@"19|20\d\d", ErrorMessage = "El formato del año no es válido")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "La propiedad precio es requerida")]
        public decimal Precio { get; set; }


        [Required(ErrorMessage = "La propiedad correo es requerida")]
        [EmailAddress]
        public string CorreoPropietario { get; set; }

        [Required(ErrorMessage = "La propiedad teléfono es requerida")]
        [Phone]
        public int TelfonoPropietario { get; set; }

    }
    public class VehiculoRequest : VehiculoBase
    {
        public Guid IdModelo { get; set; }
    }

    public class VehiculoResponse : VehiculoBase
    {
        public Guid IdVehiculo { get; set; }

        public string Modelo { get; set; }

        public string Marca { get; set; }
    }

    //SOLID DE LA O ABIERTOS A LA EXTENSION Y CERRADOS AL CAMBIO
    //CTRL K CTRL D FORMATEAR
    public class VehiculoDetalle : VehiculoResponse

    {
        public bool RevisionValida { get; set; }

        public bool RegistroValido { get; set; }

    }
}
