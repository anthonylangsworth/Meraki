using System;
using System.Collections.Generic;
using System.Text;

namespace MerakiDashboard.Test
{
    /// <summary>
    /// Compare <see cref="Organization"/>s.
    /// </summary>
    public class OrganizationEqualityComparer: IEqualityComparer<Organization>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">
        /// The first object of type T to compare.
        /// </param>
        /// <param name="y">
        /// The second object of type T to compare.
        /// </param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(Organization x, Organization y)
        {
            return x?.Name == y?.Name
                   && x?.Id == y?.Id;
        }

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <param name="obj">The <see cref="T:System.Object"></see> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj">obj</paramref> is a reference type and <paramref name="obj">obj</paramref> is null.</exception>
        public int GetHashCode(Organization obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Encourage a singleton pattern.
        /// </summary>
        public static OrganizationEqualityComparer Instance { get; } = new OrganizationEqualityComparer();
    }
}
