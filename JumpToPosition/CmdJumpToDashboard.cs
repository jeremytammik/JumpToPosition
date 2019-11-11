#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Diagnostics;
#endregion

namespace JumpToPosition
{
  [Transaction( TransactionMode.ReadOnly )]
  public class CmdJumpToDashboard : IExternalCommand
  {
    const string _url_base = "https://duckduckgo.com/?q=";

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

      Result r = CmdJumpToBrowserView
        .GetSingleSelectedElement( uidoc, 
          ref message, out e );

      if( Result.Succeeded == r )
      {
        string url = _url_base + e.UniqueId;

        Process.Start( url );
      }
      return r;
    }
  }
}

