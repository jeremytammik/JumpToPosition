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
  [Transaction( TransactionMode.ReadOnly )]
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
    /// Return the level currently viewed.
    /// If that is not well-defined, return the level
    /// of the given element, else "<nil>".
    /// </summary>
    string GetLevelFor(
      Element e,
      View view )
    {
      Document doc = e.Document;
      Element level = null;
      ElementId id = e.LevelId;
      if( ElementId.InvalidElementId != id )
      {
        level = doc.GetElement( id );
      }
      if( null == level )
      {
        level = view.GenLevel;
      }
      return null == level ? "<nil>" : level.Name;
    }

    /// <summary>
    /// Return the current user name. Presumably,
    /// the best source is the Windows login name.
    /// </summary>
    string GetUsername()
    {
      return "Jeremy";
    }

    /// <summary>
    /// Return the Revit application version string.
    /// </summary>
    string GetAppVersionString( Application app )
    {
      return string.Format(
        "{0} {1}.{2}.{3}", app.VersionName,
        app.VersionNumber, app.SubVersionNumber,
        app.VersionBuild );
    }

    /// <summary>
    /// Return information to jump to selected element 
    /// in borwser view, e.g., element identifier, 
    /// current level viewed, view name, username, 
    /// Revit version, model version, current view 
    /// direction (x,y,z, front, up, ...).
    /// </summary>
    string GetBrowserViewInfoFor(
      Element e,
      View view )
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

      Document doc = e.Document;
      Application app = doc.Application;

      string s = string.Format(
        "Element: {0}\r\n"
        + "Level: {1}\r\n"
        + "View: {2}\r\n"
        + "User: {3}\r\n"
        + "Revit: {4}\r\n"
        + "Model: {5}{6}\r\n"
        + "Eye location: {6}\r\n"
        + "View direction: {7}\r\n"
        + "Up direction: {8}\r\n",
        Util.ElementDescription( e ),
        GetLevelFor( e, view ),
        view.Name,
        GetUsername(),
        GetAppVersionString( app ),
        Document.GetDocumentVersion( doc ) );

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
      View view = doc.ActiveView;
      Element e;

      Result r = GetSingleSelectedElement(
        uidoc, ref message, out e );

      if( Result.Succeeded == r )
      {
        string browser_view_info 
          = GetBrowserViewInfoFor( e, view );
        
        //Process.Start( path );

        TaskDialog.Show( "Jump to Browser View",
          "Generate HTTP request or make web socket "
          + "call with:\r\n\r\n"
          + browser_view_info );
      }
      return r;
    }
  }
}
