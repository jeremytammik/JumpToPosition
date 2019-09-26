#region Namespaces
using System.Collections.Generic;
using System.Diagnostics;
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
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements )
    {
      UIApplication uiapp = commandData.Application;
      UIDocument uidoc = uiapp.ActiveUIDocument;
      Application app = uiapp.Application;
      Document doc = uidoc.Document;
      Selection sel = uidoc.Selection;
      ICollection<ElementId> ids = sel.GetElementIds();
      int n = ids.Count;
      Element e = null;

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

      Parameter p = e.get_Parameter( BuiltInParameter.ALL_MODEL_MARK );

      if( null != p )
      {
        string s = p.AsString();
        if( null != s && s.ToLower().StartsWith( "html" ) )
        {
          Process.Start( s );
        }
      }

      return Result.Succeeded;
    }
  }
}
