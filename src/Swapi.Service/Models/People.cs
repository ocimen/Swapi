using System.Collections.Generic;
using Newtonsoft.Json;

namespace Swapi.Service.Models

{
    /// <summary>
    /// A person within the Star Wars universe
    /// </summary>
    public class People : Entity
    {
        /// <summary>
        /// The name of this person.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The height of this person in meters.
        /// </summary>
        public string Height { get; set; }
        
        /// <summary>
        /// The mass of this person in kilograms.
        /// </summary>
        public string Mass { get; set; }

        /// <summary>
        /// The hair color of this person.
        /// </summary>
        [JsonProperty("hair_color")]
        public string HairColor { get; set; }

        /// <summary>
        /// The skin color of this person.
        /// </summary>
        [JsonProperty("skin_color")]
        public string SkinColor { get; set; }

        /// <summary>
        /// The eye color of this person.
        /// </summary>
        [JsonProperty("eye_color")]
        public string EyeColor { get; set; }

        /// <summary>
        /// The birth year of this person. BBY (Before the Battle of Yavin) or ABY (After the Battle of Yavin).
        /// </summary>
        [JsonProperty("birth_year")]
        public string BirthYear { get; set; }
        
        /// <summary>
        /// The gender of this person (if known).
        /// </summary>
        public string Gender { get; set; }
        
        /// <summary>
        /// The url of the planet resource that this person was born on.
        /// </summary>
        public string Homeworld { get; set; }
        
        /// <summary>
        /// An array of urls of film resources that this person has been in.
        /// </summary>
        public List<string> Films { get; set; }
        
        /// <summary>
        /// The url of the species resource that this person is.
        /// </summary>
        public List<string> Species { get; set; }
        
        /// <summary>
        /// An array of vehicle resources that this person has piloted
        /// </summary>
        public List<string> Vehicles { get; set; }
        
        /// <summary>
        /// An array of starship resources that this person has piloted
        /// </summary>
        public List<string> Starships { get; set; }
    }
}
