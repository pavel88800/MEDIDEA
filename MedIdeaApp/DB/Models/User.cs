using System;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace MedIdeaApp.DB.Models
{
    public class User : BaseIdNameEntity
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public User()
        {
            Treatments = new List<Treatment>();
        }

        /// <summary>
        /// Пол
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime Birthday { get; set; }
        
        /// <summary>
        ///  Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        public List<Treatment> Treatments { get; set; }
    }
}