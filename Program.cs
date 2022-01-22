//final revised program; a great competition;
//A Simple Calculator only support single operation;



using ConsoleApp1;

//creat a object list(dynamic array);
   var  objectArray = new List<SimpleCalculator>();
   // access the seven function from delegate;
    //access the delegate using Lambda expression;
    objectArray.AddRange(new []{
        new SimpleCalculator("+", (x, y) => x + y),   
        new SimpleCalculator("-", (x, y) => x - y),
        new SimpleCalculator("*", (x, y) => x * y),
        new SimpleCalculator("/", (x, y) => x / y),
        new SimpleCalculator("?", (x, y) =>Math.Floor(x /y)),   //floor function (get the integer);
        new SimpleCalculator("%", (x, y) => Convert.ToDouble(x % y)),
        new SimpleCalculator("!", (x, y) => { 
            double multi = 1.0;
            for ( double k = x; k > 1; k--)
            {
                multi = multi * k;
            }
            return multi;
        }),
        new SimpleCalculator("^", (x, y) =>
        {
            double multi = 1.0;
            for (int k = 0; k < y; k++)
            {
                multi = multi * x;
            }
            return multi;
        })
    });
    // main function!!
while (true)
{
    Console.WriteLine("please enter the  calculation expression(only support the single operator calculation):");
    string inputstring = Console.ReadLine();
    Calculation.FinIndexAndCal(inputstring, objectArray);
}
//user_defined_type
class Calculation
{
    //final calculation access the substring !!-----first function;
    private static void MidCal(string inputstring, int index, Func<double, double, double> fun)
    {
        string numstr1 = "";
        string numstr2 = "";
        double num1 = 0.0;
        double num2 = 0.0;
        // make two judgements about string whether contained the '!' and "()",make some different operation on it ;
        if (!IsInString(inputstring, '!'))
        {
            if (!IsInString(inputstring, '('))
            {
                AccessString(inputstring, ref numstr1, ref numstr2, ref num1, ref num2, 0,
                    index, index + 1, inputstring.Length - index - 1);
            }
            else
            {
                if (CharCount(inputstring, '(') == 2)
                {
                    AccessString(inputstring, ref numstr1, ref numstr2, ref num1, ref num2, 1,
                        index - 2, index + 2, inputstring.Length - index - 3);
                }
                else
                {
                    if (inputstring.IndexOf('(') < index)
                    {
                        AccessString(inputstring, ref numstr1, ref numstr2, ref num1, ref num2, 1,
                            index - 2, index + 1, inputstring.Length - index - 1);
                    }
                    else
                    {
                        AccessString(inputstring, ref numstr1, ref numstr2, ref num1, ref num2, 0,
                            index, index + 2, inputstring.Length - index - 3);
                    }
                }
            }
        }
        else
        {
            numstr1 = inputstring.Substring(0, index);
            num1 = Convert.ToDouble(numstr1);
        }

        double result = fun(num1, num2);
        Console.WriteLine($"the result is {inputstring}  ={result}");
    }
    // find the index of operator and  calculation!----second function;     

    public static void FinIndexAndCal(string inputstring, List<SimpleCalculator> list)
    {
        var index = 0;
        var ii = 0;
        // make a judgement for string whether containing the "()"  !!
        if (IsInString(inputstring, '('))
        {
            for (int jj = 0; jj < list.Count; jj++)
            {
                if (CharCount(inputstring, '(') == 2)
                {
                    index = IndexSearch(inputstring, Convert.ToChar(list[jj].sign),
                        FindStringIndex(inputstring, 1, ')'),
                        FindStringIndex(inputstring, 2, '('));
                    if (index != -1)
                    {
                        ii = jj;
                        break;
                    }
                }
                else
                {
                    if (inputstring.IndexOf('(') == 0)
                    {
                        index = IndexSearch(inputstring, Convert.ToChar(list[jj].sign), FindStringIndex(inputstring, 1, ')'),
                            inputstring.Length);
                    }
                    else
                    {
                        index = IndexSearch(inputstring, Convert.ToChar(list[jj].sign), 0,
                            inputstring.IndexOf('('));
                    }

                    if (index != -1)
                    {
                        ii = jj;
                        break;
                    }
                }
            }
        }
        else{
            for (int j = 0; j < list.Count; j++)
            {
                index = inputstring.IndexOf(list[j].sign);
                if (index != -1)
                {
                    ii = j;
                    break;
                }
            }
        }
        if (index != -1)
        {
            MidCal(inputstring, index, list[ii].Calfun); //  if finding  the relevant method,calculate now.
        }
        else
        {
            Console.WriteLine("only support the six operation!!");
        }
    }

    //  make a judgement ,with it we  can catch some exception non_effect------third function;
    private  static bool IsInString(string inputstring, char ch)
    {
        bool serchforString = false;
        foreach (char che in inputstring)
        {
            if (che == ch)
            {
                serchforString = true;
            }
        }

        return serchforString;
    }

    // make a consideration with input-string may containing the'(' and ')' ;-----fourth function;
    //overwritten for the parameter;
   private static void AccessString(string inputstring, ref string num1str, ref string num2str, ref double num1
        , ref double num2, int firstindex1, int substrlen1, int firstindex2, int substrlen2)
    {
        num1str = inputstring.Substring(firstindex1, substrlen1);
        num1 = double.Parse(num1str);
        num2str = inputstring.Substring(firstindex2, substrlen2);
        num2 = double.Parse(num2str);
    }
    //get the index of repeated element---fifth function
    private  static int FindStringIndex(string inputstring, int order, char kk)
    {
        int count = 0; // make a counter;
        int ii = 0;
        foreach (var ch in inputstring)
        {
            if (ch == kk)
            {
                count++;
            }

            if (order == count)
            {
                return ii;
            }

            ii++;
        }

        return -1;
    }
// get the count char of stringArray --sixth function;
    private  static int CharCount(string inputstring, char ch)
    {
        int count = 0;
        foreach (var chc in inputstring)
        {
            if (chc == ch)
            {
                count++;
            }
        }

        return count;
    }

    //adapt the range of searching the String Array-seventh function
    private  static int IndexSearch(string inputstring, char ch, int startindex, int endindex)
    {
        for (int i = startindex; i < endindex; i++)
        {
            if (inputstring[i] == ch)
            {
                return i; //return index  of the first occur char;
            }
        }

        return -1;
    }
}  


        



    




