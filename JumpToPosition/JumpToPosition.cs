#region Namespaces
using Autodesk.Revit.DB;
using System.Windows.Forms;
#endregion

namespace JumpToPosition
{
  class JumpToPosition
  {
    public JumpToPosition( 
      View3D view, 
      IWin32Window h )
    {
      Document doc = view.Document;

      JumpForm form = new JumpForm();

      DialogResult rslt = form.ShowDialog( h );

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

          Parameter p = view.get_Parameter( 
            BuiltInParameter.VIEW_NAME );
          string n = p.AsString();
          p.Set( "Jumping to Position" );
          doc.Regenerate();
          p.Set( n );

          tx.Commit();
        }
      }
    }
  }
}
