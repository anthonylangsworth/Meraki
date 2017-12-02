using System.Runtime.Serialization;

namespace Meraki
{
    /// <summary>
    /// A Meraki Organization.
    /// </summary>
    public class Organization
    {
        [DataMember(Name="id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}