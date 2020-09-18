using System;

namespace MedIdeaApp.DB.Models
{
    public class Treatment : BaseIdEntity
    {
        /// <summary>
        ///     Дата посещения
        /// </summary>
        public DateTime DateVisit { get; set; }

        /// <summary>
        ///     Тип посещения
        /// </summary>
        public string TypeVisit { get; set; }

        /// <summary>
        ///     Диагноз
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        ///     Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Пользователь
        /// </summary>
        public User User { get; set; }
    }
}