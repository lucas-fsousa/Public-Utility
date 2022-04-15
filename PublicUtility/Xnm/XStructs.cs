using System.Drawing;
using System.Text;

namespace PublicUtility.Xnm {
  public struct Box {
    public Size Size { get; set; }
    public Point Location { get; set; }
    
    public Box(int w, int h, int x, int y) {
      Size = new Size(w, h);
      Location = new Point(x, y);
    }

    public bool IsFilled() {
      if(Location.IsEmpty || Size.IsEmpty) 
        return false;
      return true;
    }

    public Point GetCenterBox() {
      Point xy = new();
      xy.X = Location.X + (Size.Width / 2);
      xy.Y = Location.Y + (Size.Height / 2);
      return xy;
    }

    public Point GetEndBox() {
      return new Point(Location.X + Size.Width, Location.Y + Size.Height);
    }

    public Point GetStartBox() {
      return new Point(Location.X, Location.Y);
    }
  }

}
