using System;
using System.Diagnostics;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

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

    #region Formatting
    /// <summary>
    /// Return an English plural suffix for the given
    /// number of items, i.e. 's' for zero or more
    /// than one, and nothing for exactly one.
    /// </summary>
    public static string PluralSuffix( int n )
    {
      return 1 == n ? "" : "s";
    }

    /// <summary>
    /// Return an English plural suffix 'ies' or
    /// 'y' for the given number of items.
    /// </summary>
    public static string PluralSuffixY( int n )
    {
      return 1 == n ? "y" : "ies";
    }

    /// <summary>
    /// Return a dot (full stop) for zero
    /// or a colon for more than zero.
    /// </summary>
    public static string DotOrColon( int n )
    {
      return 0 < n ? ":" : ".";
    }

    /// <summary>
    /// Return a string for a real number
    /// formatted to two decimal places.
    /// </summary>
    public static string RealString( double a )
    {
      return a.ToString( "0.##" );
    }

    /// <summary>
    /// Return a string for a UV point
    /// or vector with its coordinates
    /// formatted to two decimal places.
    /// </summary>
    public static string PointString(
      UV p,
      bool onlySpaceSeparator = false )
    {
      string format_string = onlySpaceSeparator
        ? "{0} {1}"
        : "({0},{1})";

      return string.Format( format_string,
        RealString( p.U ),
        RealString( p.V ) );
    }

    /// <summary>
    /// Return a string for an XYZ point
    /// or vector with its coordinates
    /// formatted to two decimal places.
    /// </summary>
    public static string PointString(
      XYZ p,
      bool onlySpaceSeparator = false )
    {
      string format_string = onlySpaceSeparator
        ? "{0} {1} {2}"
        : "({0},{1},{2})";

      return string.Format( format_string,
        RealString( p.X ),
        RealString( p.Y ),
        RealString( p.Z ) );
    }

    /// <summary>
    /// Return a string for this bounding box
    /// with its coordinates formatted to two
    /// decimal places.
    /// </summary>
    public static string BoundingBoxString(
      BoundingBoxUV bb,
      bool onlySpaceSeparator = false )
    {
      string format_string = onlySpaceSeparator
        ? "{0} {1}"
        : "({0},{1})";

      return string.Format( format_string,
        PointString( bb.Min, onlySpaceSeparator ),
        PointString( bb.Max, onlySpaceSeparator ) );
    }

    /// <summary>
    /// Return a string for this bounding box
    /// with its coordinates formatted to two
    /// decimal places.
    /// </summary>
    public static string BoundingBoxString(
      BoundingBoxXYZ bb,
      bool onlySpaceSeparator = false )
    {
      string format_string = onlySpaceSeparator
        ? "{0} {1}"
        : "({0},{1})";

      return string.Format( format_string,
        PointString( bb.Min, onlySpaceSeparator ),
        PointString( bb.Max, onlySpaceSeparator ) );
    }
    #endregion // Formatting

    #region Display a message
    const string _caption = "The Building Coder";

    public static void ErrorMsg(
      string msg )
    {
      Debug.WriteLine( msg );
      TaskDialog.Show( _caption, msg );
    }

    public static void InfoMsg(
      string instruction,
      string content )
    {
      Debug.WriteLine( instruction + "\r\n" + content );
      TaskDialog d = new TaskDialog( _caption );
      d.MainInstruction = instruction;
      d.MainContent = content;
      d.Show();
    }

    /// <summary>
    /// Return a string describing the given element:
    /// .NET type name,
    /// category name,
    /// family and symbol name for a family instance,
    /// element id and element name.
    /// </summary>
    public static string ElementDescription(
      Element e )
    {
      if( null == e )
      {
        return "<null>";
      }

      // For a wall, the element name equals the
      // wall type name, which is equivalent to the
      // family name ...

      FamilyInstance fi = e as FamilyInstance;

      string typeName = e.GetType().Name;

      string categoryName = (null == e.Category)
        ? string.Empty
        : e.Category.Name + " ";

      string familyName = (null == fi)
        ? string.Empty
        : fi.Symbol.Family.Name + " ";

      string symbolName = (null == fi
        || e.Name.Equals( fi.Symbol.Name ))
          ? string.Empty
          : fi.Symbol.Name + " ";

      return string.Format( "{0} {1}{2}{3}<{4} {5}>",
        typeName, categoryName, familyName,
        symbolName, e.Id.IntegerValue, e.Name );
    }

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
    #endregion // Display a message
  }
}
