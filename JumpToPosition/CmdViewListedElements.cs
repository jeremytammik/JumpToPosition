#region Namespaces
using System;
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
        foreach( int i in d.ids_to_show )
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
      List<ElementId> ids_to_show;

      if( ReadInput( out id_level, out ids_to_show ) )
      {
        Level level = doc.GetElement( id_level ) as Level;

        if( null == level )
        {
          Util.ErrorMsg( string.Format(
            "Ïnvalid level id {0}.", id_level ) );
        }
        else
        {
          ElementId id_view = level.FindAssociatedPlanViewId();
          View view = doc.GetElement( id_level ) as View;

          if( null == view )
          {
            Util.ErrorMsg( string.Format(
              "No associated plan view found for level {0}.",
              level.Name ) );
          }
          else
          {
            string date_iso = DateTime.Now.ToString( "yyyy-MM-dd" );
            string view_name = string.Format( "{0}_showing_{1}_elements_{2}",
              view.Name, ids_to_show.Count, date_iso );

            id_view = view.Duplicate( ViewDuplicateOption.AsDependent );
            View view2 = doc.GetElement( id_level ) as View;
            view2.Name = view_name;

            List<ElementId> ids_to_unhide = new List<ElementId>();

            foreach( ElementId id in ids_to_show )
            {
              Element e = doc.GetElement( id );
              if( e.IsHidden( view2 ) )
              {
                ids_to_unhide.Add( id );
              }
            }
            view2.UnhideElements( ids_to_unhide );

            FilteredElementCollector els
              = new FilteredElementCollector( doc, view2.Id );

            List<ElementId> ids_to_hide = new List<ElementId>();
            List<ElementId> ids_cannot_hide = new List<ElementId>();

            foreach( Element e in els )
            {
              if( !ids_to_show.Contains( e.Id ) )
              {
                if( !e.CanBeHidden( view2 ) )
                {
                  ids_cannot_hide.Add( e.Id );
                }
                else
                {
                  ids_to_hide.Add( e.Id );
                }
              }
            }
            view2.HideElements( ids_to_hide );
          }
        }
      }
      return rc;
    }
  }
}
