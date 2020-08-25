using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Domain.DTOs
{
    public class AnuncioDTO : Notifiable
    {
        public int AnuncioId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Versão { get; set; }
        public int Ano { get; set; }
        public int Quilometragem { get; set; }
        public string Observacao { get; set; }

        public void Validate()
        {

            if (Ano <= 0)
                AddNotification(new Notification("Ano", "Ano inválido"));

            if (Quilometragem <= 0)
                AddNotification(new Notification("Quilometragem", "Quilometragem inválido"));


            AddNotifications(
               new ValidationContract()
                    .Requires()
                    .IsNotNullOrEmpty(Marca, "Marca", "Marca é obrigatório")
                    .IsNotNullOrEmpty(Modelo, "Modelo", "Modelo é obrigatório")
                    .IsNotNullOrEmpty(Versão, "Versão", "Versão é obrigatório")
                    .IsNotNullOrEmpty(Observacao, "Observacao", "Observacao é obrigatório")
            );
        }
    }
}
