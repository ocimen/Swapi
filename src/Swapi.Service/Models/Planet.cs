using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Swapi.Service.Models
{
    /// <summary>
    /// A planet.
    /// </summary>
    public class Planet : Entity
    {
        /// <summary>
        /// The name of this planet.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The number of standard hours it takes for this planet to complete a single rotation on it's axis.
        /// </summary>
        [JsonProperty("rotation_period")]
        public string RotationPeriod { get; set; }
        
        /// <summary>
        /// The number of standard days it takes for this planet to complete a single orbit of it's local star.
        /// </summary>
        [JsonProperty("orbital_period")]
        public string OrbitalPeriod { get; set; }
        
        /// <summary>
        /// The diameter of this planet in kilometers.
        /// </summary>
        public string Diameter { get; set; }
        
        /// <summary>
        /// The climate of this planet. Comma-seperated if diverse.
        /// </summary>
        public string Climate { get; set; }
        
        /// <summary>
        /// A number denoting the gravity of this planet. Where 1 is normal.
        /// </summary>
        public string Gravity { get; set; }
        
        /// <summary>
        /// the terrain of this planet. Comma-seperated if diverse.
        /// </summary>
        public string Terrain { get; set; }
        
        /// <summary>
        /// The percentage of the planet surface that is naturally occuring water or bodies of water.
        /// </summary>
        [JsonProperty("surface_water")]
        public string SurfaceWater { get; set; }
        
        /// <summary>
        /// The average population of sentient beings inhabiting this planet.
        /// </summary>
        public string Population { get; set; }
        
        /// <summary>
        /// An array of People URL Resources that live on this planet.
        /// </summary>
        public List<string> Residents { get; set; }

        /// <summary>
        /// An array of Film URL Resources that this planet has appeared in.
        /// </summary>
        public List<string> Films { get; set; }
    }
}