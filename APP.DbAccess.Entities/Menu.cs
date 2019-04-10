using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using APP.Framework.Util;

namespace APP.DbAccess.Entities
{
    public class Menu
    {
        [Key]
        [MaxLength(40)]
        public string Id { get; set; } = Guid.NewGuid().ToString(10);

        [MaxLength(40)]
        public string ParentId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Icon { get; set; }
        [MaxLength(500)]
        public string Path { get; set; }
        public int Order { get; set; }
        public int State { get; set; }
    }
}
