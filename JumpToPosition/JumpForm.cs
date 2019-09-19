using System.ComponentModel;
using System.Windows.Forms;
using XYZ = Autodesk.Revit.DB.XYZ;

namespace JumpToPosition
{
  public partial class JumpForm : Form
  {
    public JumpForm()
    {
      InitializeComponent();
    }

    public XYZ Target
    {
      get
      {
        return Util.ParseXyz( textBoxTarget.Text );
      }
    }

    public XYZ Viewdir
    {
      get
      {
        return Util.ParseXyz( textBoxViewdir.Text );
      }
    }

    private void textBoxTarget_Validating( object sender, CancelEventArgs e )
    {
      string s = textBoxTarget.Text;
      XYZ p;
      try
      {
        p = Util.ParseXyz( s );
      }
      catch( System.FormatException )
      {
        // Cancel the event.
        e.Cancel = true;
        // Select the text to be corrected by the user.
        textBoxTarget.Select( 0, textBoxTarget.Text.Length );
        // Report error.
        this.errorProvider1.SetError( textBoxTarget,
          "Invalid target point: " + s );
      }
    }

    private void textBoxViewdir_Validating( object sender, CancelEventArgs e )
    {
      string s = textBoxViewdir.Text;
      XYZ p;
      try
      {
        p = Util.ParseXyz( s );
      }
      catch( System.FormatException )
      {
        // Cancel the event.
        e.Cancel = true;
        // Select the text to be corrected by the user.
        textBoxViewdir.Select( 0, textBoxViewdir.Text.Length );
        // Report error.
        this.errorProvider1.SetError( textBoxViewdir,
          "Invalid view direction vector: " + s );
      }
    }

    private void textBoxTarget_Validated( object sender, System.EventArgs e )
    {
      errorProvider1.SetError( textBoxTarget, "" );
    }

    private void textBoxViewdir_Validated( object sender, System.EventArgs e )
    {
      errorProvider1.SetError( textBoxViewdir, "" );
    }
  }
}
