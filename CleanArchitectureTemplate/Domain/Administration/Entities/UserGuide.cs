using Domain.Common;
using Domain.Identity.Entities;

namespace SystemOfWidget.Domain.Administration.Entities
{
    /// <summary>
    /// Руководство пользователя
    /// </summary>
    public class UserGuide : BaseEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата последнего обновления
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Ссылка на документ
        /// </summary>
        public long? DocumentId { get; set; }

        /// <summary>
        /// Роли
        /// </summary>
        public List<ApplicationRole> Roles { get; set; } = new();
    }
}
