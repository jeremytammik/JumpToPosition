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

### Setting the View Orientation

### Refreshing the View

### Setting the View Name to Force Refresh

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
This works:

VIEW_PARTS_VISIBILITY
VIEWER_BOUND_ACTIVE_FAR
