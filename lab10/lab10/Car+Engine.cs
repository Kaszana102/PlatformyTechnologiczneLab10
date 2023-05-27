using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab10
{
    [XmlType("car")]
    public class Car
    {
        public string model { get; set; }
        public int year { get; set; }
        [XmlElement(ElementName = "engine")]
        public Engine motor { get; set; }

        public Car() { }
        public Car(string model, Engine motor, int year)
        {
            this.model = model;
            this.year = year;
            this.motor = motor;
        }
    }

    [XmlRoot(ElementName = "engine")]
    public class Engine : IComparable<Engine>
    {
        public double displacement { get; set; }
        public double horsePower { get; set; }
        [XmlAttribute]
        public string model { get; set; }

        public Engine() { }
        public Engine(double displacement, double horsePower, string model)
        {
            this.displacement = displacement;
            this.horsePower = horsePower;
            this.model = model;
        }

        public override string ToString()
        {
            return model + ' ' + displacement + " (" + horsePower+" hp)";
        }

        public int CompareTo(Engine? other)
        {
            return (int)(horsePower - other.horsePower);
        }
    }
}
