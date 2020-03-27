using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace Adapter
{
    class Program
    {

        Dictionary<string, string> countries = new Dictionary<string, string>(3);

        public Program()
        {
            countries.Add("UA", "Ukraine");
            countries.Add("RU", "Russia");
            countries.Add("CA", "Canada");
        }

                    
        public class IncomeDataAdapter: Customer, Contact
        {
            private IncomeData iData;
            
            public IncomeDataAdapter(IncomeData iData)
            {
                this.iData = iData;
            }

            public string getCompanyName()
            {
                return iData.getCompany();
            }

            public string getCountryName()
            {
                Program pr = new Program();
                
                return pr.countries[iData.getCountryCode()]; 
            }

            public string getName()
            {
                return iData.getContactLastName() +", " + iData.getContactFirstName();
            }

            public string getPhoneNumber()
            {

                String temp = "";
                String z = (iData.getPhoneNumber()).ToString();
                int count = z.Length;
                for (int i = 0; i < 10 - count; i++)
                {
                    temp = temp + 0;
                }
                temp = temp + z;

                return "+" + iData.getCountryPhoneCode() + "(" + temp.Substring(0, 3) + ")" + temp.Substring(3,3) + "-" + temp.Substring(6,2) + "-" + temp.Substring(8);

            }

        }
        
        public interface IncomeData
        {
            string getCountryCode();
            string getCompany();
            string getContactFirstName();
            string getContactLastName();
            int getCountryPhoneCode();
            int getPhoneNumber();
        }

        public class ConcreteIncomeData: IncomeData
        {
            public string getCountryCode()
            {
                Program pr = new Program();
                Random rand = new Random();
                return pr.countries.ElementAt(rand.Next(0, pr.countries.Count)).Key;
            }
            public string getCompany()
            {
                return "JavaRush Ltd.";
            }
            public string getContactFirstName()
            {
                return "Ivan_ID";
            }
            public string getContactLastName()
            {
                return "Ivanov_ID";
            }
            public int getCountryPhoneCode()
            {
                return 38;
            }
            public int getPhoneNumber()
            {
                return 501234567;
            }
        }

        public interface Customer
        {
            string getCompanyName();
            string getCountryName();
        }

        public class ConcreteCustomer: Customer
        {
            public string getCompanyName()
            {
                return "JavaRush Ltd. (из Customer)";
            }

            public string getCountryName()
            {
                Program pr = new Program();
                Random rand = new Random();
                return pr.countries.ElementAt(rand.Next(0, pr.countries.Count)).Value + " (из Customer)";
                
            }
        }

        public interface Contact
        {
            string getName();
            string getPhoneNumber();
        }

        public class ConcreteContact: Contact
        {
            public string getName()
            {
                return "Ivanov, Ivan (из Contact)";
            }
            public string getPhoneNumber()
            {
                return "+38(050)123-45-67  (из Contact)";
            }
        }

        static void Main(string[] args)
        {
            ConcreteCustomer cust = new ConcreteCustomer();
            ConcreteContact cont = new ConcreteContact();
            ConcreteIncomeData inData = new ConcreteIncomeData();

            Console.WriteLine("Вызов методов Customer:");
            Console.WriteLine(cust.getCompanyName());
            Console.WriteLine(cust.getCountryName());
            Console.WriteLine();

            Console.WriteLine("Вызов методов Contact:");
            Console.WriteLine(cont.getName());
            Console.WriteLine(cont.getPhoneNumber());
            Console.WriteLine();

            IncomeDataAdapter adapter = new IncomeDataAdapter(inData);

            Console.WriteLine("Вызов методов через адаптер:");
            Console.WriteLine(adapter.getCompanyName());
            Console.WriteLine(adapter.getCountryName());
            Console.WriteLine(adapter.getName());
            Console.WriteLine(adapter.getPhoneNumber());

            Console.ReadKey();
        }
 
    }
}
