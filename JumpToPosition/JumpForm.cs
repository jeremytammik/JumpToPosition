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

    public XYZ Eye
    {
      get
      {
        return Util.ParseXyz( textBoxEye.Text );
      }
    }

    public XYZ Viewdir
    {
      get
      {
        return Util.ParseXyz( textBoxViewdir.Text );
      }
    }

    private void textBoxEye_Validating( object sender, CancelEventArgs e )
    {
      string s = textBoxEye.Text;
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
        textBoxEye.Select( 0, textBoxEye.Text.Length );
        // Report error.
        this.errorProvider1.SetError( textBoxEye,
          "Invalid eye position: " + s );
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

    private void textBoxEye_Validated( object sender, System.EventArgs e )
    {
      errorProvider1.SetError( textBoxEye, "" );
    }

    private void textBoxViewdir_Validated( object sender, System.EventArgs e )
    {
      errorProvider1.SetError( textBoxViewdir, "" );
    }
  }
}
