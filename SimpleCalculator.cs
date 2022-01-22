namespace ConsoleApp1;

public class SimpleCalculator
{ 
    public string sign = "";
   public Func<double, double,double> Calfun; //default to null;
   public SimpleCalculator(string sign, Func<double,double,double>calfun)
   {
       this.sign = sign;
       this.Calfun = calfun;
   }
}
