using System;
using Autodesk.Revit.DB;


namespace JumpToPosition
{
  class Util
  {
    #region Geometrical Comparison
    public const double _eps = 1.0e-9;

    public static bool IsZero(
      double a,
      double tolerance = _eps )
    {
      return tolerance > Math.Abs( a );
    }

    public static bool IsVertical( XYZ v )
    {
      return IsZero( v.X ) && IsZero( v.Y );
    }
    #endregion // Geometrical Comparison

    /// <summary>
    /// Parse an XYZ point or vector from a string
    /// </summary>
    public static XYZ ParseXyz( string s )
    {
      char[] delimiters = new[] { ',', ';', ' ' };

      string[] coords = s.Split( delimiters, 
        StringSplitOptions.RemoveEmptyEntries );

      double x = 0, y = 0, z = 0;
      int j = 0;

      foreach( string coord in coords )
      {
        switch( j )
        {
          case 0:
            x = Double.Parse( coord );
            break;
          case 1:
            y = Double.Parse( coord );
            break;
          case 2:
            z = Double.Parse( coord );
            break;
          default:
            break;
        }
        j++;
      }
      if( 3 != j )
      {
        throw new System.FormatException(
          "Unable to parse X, Y and Z coordinates" );
      }
      return new XYZ( x, y, z );
    }
  }
}
