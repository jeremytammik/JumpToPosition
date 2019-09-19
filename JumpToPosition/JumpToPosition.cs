#region Namespaces
using Autodesk.Revit.DB;
using System.Windows.Forms;
#endregion

namespace JumpToPosition
{
  class JumpToPosition
  {
    public JumpToPosition( 
      Autodesk.Revit.DB.View view, 
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
          XYZ up = XYZ.BasisZ;

          ViewOrientation3D viewOrientation3D
            = new ViewOrientation3D( eye, up, forward );

          tx.Commit();
        }
      }
    }
  }
}
