#region Namespaces
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace JumpToPosition
{
  [Transaction( TransactionMode.Manual )]
  public class CmdJumpToBrowserView : IExternalCommand
  {
    /// <summary>
    /// Return a single preselected element
    /// or prompt user to select one.
    /// </summary>
    Result GetSingleSelectedElement(
      UIDocument uidoc,
      ref string message,
      out Element e )
    {
      Document doc = uidoc.Document;
      Selection sel = uidoc.Selection;
      ICollection<ElementId> ids = sel.GetElementIds();
      int n = ids.Count;

      e = null;

      if( 1 == n )
      {
        foreach( ElementId id in ids )
        {
          e = doc.GetElement( id );
        }
      }
      else if( 0 == n )
      {
        try
        {
          Reference r = sel.PickObject(
            ObjectType.Element, "Please select element "
              + "to view in external browser" );

          e = doc.GetElement( r.ElementId );
        }
        catch( OperationCanceledException )
        {
          return Result.Cancelled;
        }
      }
      else
      {
        message = "Please launch this command with "
          + "at most one pre-selected element";

        return Result.Failed;
      }
      return Result.Succeeded;
    }

    /// <summary>
    /// Return information to jump to selected element 
    /// in borwser view, e.g., element identifier, 
    /// current level viewed, view name, username, 
    /// Revit version, model version, current view 
    /// direction (x,y,z, front,up...).
    /// </summary>
    string GetBrowserViewInfoFor( Element e )
    {
      #region Generate HTML file
#if GENERATE_HTML_FILE
      string path = "C:/tmp/CmdJumpToBrowserView.html";
      using( StreamWriter s = new StreamWriter( path ) )
      {
        s.WriteLine( string.Format(
          "<html>\n<body>\n<p>{0}</p>\n</body>\n<html>",
          Util.ElementDescription( e ) ) );

        s.Close();
      }
      return path;
#endif // GENERATE_HTML_FILE
      #endregion // Generate HTML file

      string s = Util.ElementDescription( e );

      return s;
    }

    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements )
    {
      UIApplication uiapp = commandData.Application;
      UIDocument uidoc = uiapp.ActiveUIDocument;
      Application app = uiapp.Application;
      Document doc = uidoc.Document;
      Element e;

      Result r = GetSingleSelectedElement(
        uidoc, ref message, out e );

      if( Result.Succeeded == r )
      {
        string path = GetBrowserViewInfoFor( e );
        Process.Start( path );
      }
      return r;
    }
  }
}
