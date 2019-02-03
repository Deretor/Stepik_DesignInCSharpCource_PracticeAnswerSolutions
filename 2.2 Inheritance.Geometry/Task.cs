using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.Geometry
{
    

	public abstract class Body: IVisitorNode
    {
        public abstract void Accept(IVisitor visitor);
        public abstract double GetVolume();		
	}

	public class Ball : Body
	{
		public double Radius { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override double GetVolume()
        {
            return 4.0 * Math.PI * Math.Pow(this.Radius, 3) / 3;
        }
    }

	public class Cube : Body
	{
		public double Size { get; set; }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override double GetVolume()
        {
            return Math.Pow(this.Size, 3);
        }
    }

	public class Cyllinder : Body
	{
		public double Height { get; set; }
		public double Radius { get; set; }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override double GetVolume()
        {   
            return Math.PI * Math.Pow(this.Radius, 2) * this.Height;
        }
    }

    // Заготовка класса для задачи на Visitor

    public interface IVisitor {
       void Visit(Ball ball);
       void Visit(Cube cube);
       void Visit(Cyllinder cyllinder);
    }

    public interface IVisitorNode {
        void Accept(IVisitor visitor);
    }

    public class SurfaceAreaVisitor: IVisitor
	{
        public double SurfaceArea { get; private set; }

        public void Visit(Ball ball)
        {
            this.SurfaceArea  = 4 * Math.PI * Math.Pow(ball.Radius, 2);
        }

        public void Visit(Cube cube)
        {
            this.SurfaceArea = 6 * Math.Pow(cube.Size,2);
        }

        public void Visit(Cyllinder cyllinder)
        {
            this.SurfaceArea = 2 * Math.PI * cyllinder.Radius * (cyllinder.Radius + cyllinder.Height);
        }
    }
	public class DimensionsVisitor: IVisitor
	{
		public Dimensions Dimensions { get; private set; }

        public void Visit(Ball ball)
        {
            this.Dimensions = new Dimensions(ball.Radius * 2, ball.Radius * 2);
        }

        public void Visit(Cube cube)
        {
            this.Dimensions = new Dimensions(cube.Size,cube.Size);
        }

        public void Visit(Cyllinder cyllinder)
        {
            this.Dimensions = new Dimensions(cyllinder.Radius*2, cyllinder.Height);
        }
    }
}
