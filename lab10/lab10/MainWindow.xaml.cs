using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace lab10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var myCars = loadCars();
            BindingList<Car> myCarsBindingList = new BindingList<Car>(myCars);
            BindingSource carBindingSource = new BindingSource();
            carBindingSource.DataSource = myCarsBindingList;
            //Drag a DataGridView control from the Toolbox to the form.
            dataGridView1.ItemsSource = carBindingSource;


            //zad 1
            //pierwsze zapytanie //method based
            var queryExpresion1 = myCars.Where(x => x.model.Equals("A6"))
                                .Select(x => new
                                {
                                    engineType = String.Compare(x.motor.model, "TDI") == 0
                                                                   ? "diesel"
                                                                   : "petrol",
                                    avgHPPL = x.motor.horsePower / x.motor.displacement,
                                })
                                .GroupBy(x => x.engineType)
                                .Select(x => new
                                {
                                    avgHPPL = x.Average(s => s.avgHPPL).ToString(),
                                    //engineType = x.Select(s => s.engineType).ToString(),
                                    engineType = x.Key
                                }).OrderByDescending(x => x.avgHPPL) ;

            foreach (var e in queryExpresion1) Console.WriteLine(e.engineType + ": " + e.avgHPPL);


            //drugie zapytanie //query based

            var queryExpresion2 = from car in myCars

                                  select new { engineType = String.Compare(car.motor.model, "TDI") == 0
                                                                   ? "diesel"
                                                                   : "petrol",
                                             avgHPPL = car.motor.horsePower / car.motor.displacement
                                  };
            /*var queryExpresion3 = from car in queryExpresion2                                   //DO DOKOŃCZENIA
                                  group car by car.engineType into temp
                                  orderby temp descending
                                  select;
              */                    
                                  


            foreach (var e in queryExpresion2) Console.WriteLine(e.engineType + ": " + e.avgHPPL);
        }

        private List<Car> loadCars()
        {
            List<Car> myCars = new List<Car>(){
                new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
                new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
                new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
                new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
                new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
                new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
                new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
                };
            return myCars;
        }
    }
}
