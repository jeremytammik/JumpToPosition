# Jump to Position in Revit

.NET C# Revit add-in setting view target and view direction to specified values.

## Task

I have a model open, and a view that I see in my external model navigator.

I want to be able to jump to this exact position within Revit, looking exactly in the same direction, i.e.:

- Go to level
- Target coordinates x=.. y=.. z=..
- Looking along this vector...

## Solution

Here is a tiny plugin to do that.

- In Revit, launch the command in a 3D view
- Enter the target viewpoint and view direction vector X, Y and Z cooredinates
- Click `Jump`

The view settings jump to that position.

## To Do

- Rotation
- Other viewing parameters, e.g., field of view

