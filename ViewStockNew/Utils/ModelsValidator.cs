using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewStockNew.Utils
{
    public class ModelsValidator
    {
        public void Validate(object modelo)
        {
            string mensajeError = "";
            List<ValidationResult> resultados = new List<ValidationResult>();
            ValidationContext contexto = new ValidationContext(modelo);
            bool esValido = Validator.TryValidateObject(modelo, contexto, resultados, true);
            if (!esValido)
            {
                foreach (var item in resultados)
                    mensajeError += "- " + item.ErrorMessage + "\n";
                throw new Exception(mensajeError);
            }
        }
    }
}
