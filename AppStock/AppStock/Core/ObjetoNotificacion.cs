using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppStock.Core
{
    public abstract class ObjetoNotificacion : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //CallerMemberName nos devuelve el nombre de la propiedad que fue modificada
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            /*Este código que comprueba si un valor es nulo se puede hacer
            * con una sola linea con la forma ?.Invoke
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }*/
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
