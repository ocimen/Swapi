namespace Swapi.Service.Models
{
    public abstract class Entity
    {
        /// <summary>
        /// The ISO 8601 date format of the time that this resource was created.
        /// </summary>
        public string Created { get; set; }
        
        /// <summary>
        /// the ISO 8601 date format of the time that this resource was edited.
        /// </summary>
        public string Edited { get; set; }
        
        /// <summary>
        /// The hypermedia URL of this resource.
        /// </summary>
        public string Url { get; set; }
    }
}