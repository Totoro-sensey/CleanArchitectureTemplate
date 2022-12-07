using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity.Entities
{
    /// <summary>
    /// Пользователь системы
    /// </summary>
    public class ApplicationUser : IdentityUser, IEntityWithId<string>
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Признак удаления пользователя
        /// </summary>
        public bool IsDeleted { get; set; }


        // Навигационные свойства
        
        /// <summary>
        /// Навигационное свойство - Сессия для обновления JWT
        /// </summary>
        public List<RefreshSession> RefreshSessions { get; } = new();
    }
}
