#region Namespaces
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace JumpToPosition
{
  [Transaction( TransactionMode.Manual )]
  public class CmdViewListedElements : IExternalCommand
  {
    /// <summary>
    /// JSON input file specifying element ids of 
    /// level and BIM elements to display on it.
    /// JSON format: { "level_id" : "...", 
    /// "ids_to_show" : ["id1", "id2"] }
    /// </summary>
    const string _input_file_path 
      = "C:/tmp/revit_view_creator_config.json";

    /// <summary>
    /// Read JSON input file and return success
    /// </summary>
    static bool ReadInput( 
      out ElementId id_level,
      out List<ElementId> ids_to_show )
    {
      bool rc = false;

      id_level = ElementId.InvalidElementId;
      ids_to_show = null;

      if( File.Exists( _input_file_path ) )
      {
        InputData d = (new JavaScriptSerializer())
          .Deserialize<InputData>( File
          .ReadAllText( _input_file_path ) );

        id_level = new ElementId( d.id_level );
        ids_to_show = new List<ElementId>();
        foreach(int i in d.ids_to_show)
        {
          ids_to_show.Add( new ElementId( i ) );
        }
      }
      return rc;
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
      Result rc = Result.Failed;
      ElementId id_level;
      List< ElementId > ids_to_show;

      if( ReadInput( out id_level, out ids_to_show ) )
      {

      }
      return rc;
    }
  }
}
