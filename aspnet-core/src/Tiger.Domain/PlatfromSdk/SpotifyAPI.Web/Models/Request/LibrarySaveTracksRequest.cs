using System.Collections.Generic;

namespace SpotifyAPI.Web
{
  public class LibrarySaveTracksRequest : RequestParams
  {
    /// <summary>
    ///
    /// </summary>
    /// <param name="ids">
    /// A comma-separated list of the Spotify IDs.
    /// For example: ids=4iV5W9uYEdYUVa79Axb7Rh,1301WleyT98MSxVHPZCA6M. Maximum: 50 IDs.
    /// </param>
    public LibrarySaveTracksRequest(IList<string> ids)
    {
      Ensure.ArgumentNotNullOrEmptyList(ids, nameof(ids));

      Ids = ids;
    }

    /// <summary>
    /// A comma-separated list of the Spotify IDs.
    /// For example: ids=4iV5W9uYEdYUVa79Axb7Rh,1301WleyT98MSxVHPZCA6M. Maximum: 50 IDs.
    /// </summary>
    /// <value></value>
    [QueryParam("ids")]
    public IList<string> Ids { get; }
  }
}

