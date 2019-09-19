#region Namespaces
using Autodesk.Revit.DB;
using System.Windows.Forms;
#endregion

namespace JumpToPosition
{
  class JumpToPosition
  {
    /// <summary>
    /// Force a refresh of the current view.
    /// Calling Regenerate on its own has no effect.
    /// Setting a parameter does.
    /// Which parameter?
    /// Up to you.
    /// The Name would be fine, but cannot be reset to {3D}.
    /// Toggling the 'far bound active' on and off works.
    /// </summary>
    void RefreshView( View3D view )
    {
      Document doc = view.Document;

      // A call to Regenerate on its own has no effect.
      //doc.Regenerate();

      // Set a parameter to force a view refresh, cf.
      // Setting a Parameter to Regenerate the Model
      // https://thebuildingcoder.typepad.com/blog/2017/11/cloud-model-predicate-and-set-parameter-regenerates.html#3
      // If the original view name is "{3D}", we are
      // unable to reset it after changing it, because
      // it contains invalid characters.

      //Parameter p = view.get_Parameter(
      //  BuiltInParameter.VIEW_NAME );
      //string n = p.AsString();
      //p.Set( "JumpToPosition" );
      //doc.Regenerate();
      //p.Set( n );

      Parameter p = view.get_Parameter(
        BuiltInParameter.VIEWER_BOUND_ACTIVE_FAR );
      int b = p.AsInteger();
      p.Set( (0 == b) ? 1 : 0 );
      doc.Regenerate();
      p.Set( b );
    }

    /// <summary>
    /// Prompt the user for an eye position and a 
    /// view direction, the set the view orientation 
    /// accordingly.
    /// </summary>
    public JumpToPosition( 
      View3D view, 
      IWin32Window owner_window_handle )
    {
      Document doc = view.Document;

      JumpForm form = new JumpForm();

      DialogResult rslt = form.ShowDialog( 
        owner_window_handle );

      if( DialogResult.OK == rslt )
      {
        using( Transaction tx = new Transaction( doc ) )
        {
          tx.Start( "Jump to Position" );

          XYZ eye = form.Eye;
          XYZ forward = form.Viewdir;
          XYZ left = XYZ.BasisZ.CrossProduct( forward );
          XYZ up = forward.CrossProduct( left );

          // Setting ùp`to the Z axis, XYZ.BasisZ, throws
          // Autodesk.Revit.Exceptions.ArgumentsInconsistentException:
          // The vectors upDirection and forwardDirection 
          // are not perpendicular.

          ViewOrientation3D orientation
            = new ViewOrientation3D( eye, up, forward );

          view.SetOrientation( orientation );

          RefreshView( view );

          tx.Commit();
        }
      }
    }
  }
}
