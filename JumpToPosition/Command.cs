#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace JumpToPosition
{
  [Transaction( TransactionMode.Manual )]
  public class Command : IExternalCommand
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

      View3D view = (null == doc) 
        ? null 
        : doc.ActiveView as View3D;

      if( null == view )
      {
        message = "Please run this command in a 3D view";
        return Result.Failed;
      }

      JtWindowHandle h = new JtWindowHandle( 
        uiapp.MainWindowHandle );

      JumpToPosition jumper = new JumpToPosition( 
        view, h );

      return Result.Succeeded;
    }
  }
}
