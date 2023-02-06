using System;

namespace Dragons.Api.Models
{
    public class Dragon
    {
        // The dragons unique id. Guid.
        public Guid DragonId { get; set; }

        // The dragon's name
        public string Name { get; set; }

        // The type of the dragon
        public string Type { get; set; }

        // The size of the dragon
        public string Size { get; set; }
    }
}
