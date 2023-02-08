namespace CityInfo.APi.Models
{
    /// <summary>
    /// A DTO for a city Without Points of interest
    /// </summary>
    public class CityWithoutPointsOfInterestDto
    {
        /// <summary>
        /// The Id of the City
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Name of the city of Interest
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The decription of the city of Interest.
        /// </summary>
        public string? Description { get; set; }
    }
}
