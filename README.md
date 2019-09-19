# Jump to Position in Revit

.NET C# Revit add-in setting view target and view direction to specified values.

## Task

I have a model open, and a view that I see in my external model navigator.

I want to be able to jump to this exact position within Revit, looking exactly in the same direction, i.e.:

- Go to level
- Target coordinates x=.. y=.. z=..
- Looking along this vector...

## Analysis

This is quite simple.

I believe there is no need to do anything about the level.

Here is an article
on [setting up your ViewOrientation3D](http://thebuildingcoder.typepad.com/blog/2013/04/setting-up-your-vieworientation3d.html).

If you are happy with a perspective view, I cleaned up an existing method `CreatePerspectiveViewMatchingCamera`
in [The Building Codes samples](https://github.com/jeremytammik/the_building_coder_samples) to
make it ready and suitable for use,
in [CmdNewSpotElevation.cs lines 110-174](https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdNewSpotElevation.cs#L110-L174).

## Solution

Here is a tiny plugin to do that.

- In Revit, launch the command in a 3D view
- Enter the target viewpoint and view direction vector X, Y and Z cooredinates
- Click `Jump`

The view settings jump to that position.

## To Do

- Rotation
- Other viewing parameters, e.g., field of view

## Implementation

### Windows Forms TextBox XYZ Validation

The eye position and view direction vector data is entered in a text box in a Windows form.

To ensure that valid coordintates are entered that can be parsed into a Revit `XYZ` object, validation is added to the form using the `TextBox` `Validating` and `Validated` events.

They require a predicate to test whether the current data can be successfully parsed into a 3D `XYZ` object.

Such a predicate is supplied by the new `ParseXyz` method:

```
    /// <summary>
    /// Parse an XYZ point or vector from a string
    /// </summary>
    public static XYZ ParseXyz( string s )
    {
      char[] delimiters = new[] { ',', ';', ' ' };

      string[] coords = s.Split( delimiters, 
        StringSplitOptions.RemoveEmptyEntries );

      double x = 0, y = 0, z = 0;
      int j = 0;

      foreach( string coord in coords )
      {
        switch( j )
        {
          case 0:
            x = Double.Parse( coord );
            break;
          case 1:
            y = Double.Parse( coord );
            break;
          case 2:
            z = Double.Parse( coord );
            break;
          default:
            break;
        }
        j++;
      }
      if( 3 != j )
      {
        throw new System.FormatException(
          "Unable to parse X, Y and Z coordinates" );
      }
      return new XYZ( x, y, z );
    }
```

### Setting the View Orientation

The view orientation can easily be set as described in the article
on [setting up your ViewOrientation3D](http://thebuildingcoder.typepad.com/blog/2013/04/setting-up-your-vieworientation3d.html).

Its constructor takes three arguments, `eye`, `up` and `forward`.

The `up` vector must indeed be perpendicular to `forward`; otherwise, Revit will throw 
*Autodesk.Revit.Exceptions.ArgumentsInconsistentException: The vectors upDirection and forwardDirection are not perpendicular*.

In the initial version, we simply calculate `up` from `eye` and `forward` like this, also taking a vertical view direction into account:

```
    XYZ eye = form.Eye;
    XYZ forward = form.Viewdir;
  
    XYZ left = Util.IsVertical( forward )
      ? -XYZ.BasisX
      : XYZ.BasisZ.CrossProduct( forward );
  
    XYZ up = forward.CrossProduct( left );
```

Please refer to [JumpToPosition.cs](JumpToPosition/JumpToPosition.cs) for the complete implementation.

### Refreshing the View

The view does not refresh by itself after setting the `ViewOrientation3D`.

Calling `doc.Regenerate` on its own has no effect either.

We can modify a parameter value to force a view refresh, as described in the note 
on [Setting a Parameter to Regenerate the Model](https://thebuildingcoder.typepad.com/blog/2017/11/cloud-model-predicate-and-set-parameter-regenerates.html#3).

I searched for a parameter that changes as little as possible in the model to avoid performance costs.

I initially tried to use the view `Name`. That works and does indeed refresh the view.

Unfortunately, if the original view name is "{3D}", we are unable to reset it after changing it, because it contains invalid characters.

Next, I tried toggling the 'far bound active' on and off, and that works as well.

Please refer to [JumpToPosition.cs](JumpToPosition/JumpToPosition.cs) for the complete implementation.
